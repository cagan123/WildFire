using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using JetBrains.Annotations;
[RequireComponent(typeof(Seeker))]
public class PathFinder : MonoBehaviour
{
    public Transform playerPosition;
    [HideInInspector] Transform targetPos;
    private Seeker seeker;
    public Path path;
    public float nextWaypointDistance = 0.5f;
    private int currentWaypoint = 0;
    public bool reachedEndOfPath;
    public Vector2 velocity;
    public Vector2 direction;
    public void Start () {
        playerPosition = PlayerManager.instance.player.transform;
        seeker = GetComponent<Seeker>();
        StartCoroutine("PathFindingRoutine");
        targetPos = DestinationSetter(playerPosition);
    }
    public Transform DestinationSetter(Transform _newTarget){
        targetPos = _newTarget;
        return targetPos;
    }

    public void OnPathComplete (Path p) {
       // Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void Update () {      
        CalculatePath();    
    }
    public IEnumerator PathFindingRoutine(){      
        float delay = .5f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            seeker.StartPath(transform.position, targetPos.position, OnPathComplete);
        }

    }

    public void CalculatePath(){
        if (path == null) {
            // We have no path to follow yet, so don't do anything
            return;
        }
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true) {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }
        direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
    }
}

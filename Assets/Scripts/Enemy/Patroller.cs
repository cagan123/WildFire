using UnityEngine;
[RequireComponent(typeof(Waypoint))]

public class Patroller : MonoBehaviour
{
	Enemy enemy;
	public Vector3 patrolDirection{get; private set;}
	public bool playerSeen = false;
	Waypoint waypoint;
	private int pointIndex;
	private Vector3 nextPos;
	private void Awake(){
		waypoint = GetComponent<Waypoint>();
		enemy = GetComponent<Enemy>();
	}
	private	void Update(){		
		GetDirectionOfPath();
	}
	private void GetDirectionOfPath(){
		patrolDirection = (GetCurrnetPosition() - transform.position).normalized;
		if(Vector3.Distance(transform.position, GetCurrnetPosition()) < 0.1f){
			UpdateNextPosition();
		}

	}
	private void UpdateNextPosition(){
		pointIndex ++;
		if(pointIndex > waypoint.Points.Length - 1){
			pointIndex = 0;
		}
	}
	private Vector3 GetCurrnetPosition(){
		return waypoint.GetPosition(pointIndex);
	}
}

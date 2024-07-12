using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    Enemy enemy;
    Transform player;
    [SerializeField] protected LayerMask PlayerLayer;
    [SerializeField] protected LayerMask ObstacleLayer;

    [Header("Vision Info")]
    [Range(0f, 360f)]
    public float angleOfVision;
    public float lenghtOfVision;
    [HideInInspector] public bool playerSeen;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        player = PlayerManager.instance.player.transform;
        StartCoroutine(FieldOfViewRoutine());
    }

    #region Field of View
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees) // math stuff
    {
        angleInDegrees -= eulerY;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private IEnumerator FieldOfViewRoutine()
    {
        float delay = .2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, lenghtOfVision, PlayerLayer);

        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector2 directionToPlayer = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToPlayer) < angleOfVision / 2)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position); ;

                if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, ObstacleLayer))
                    playerSeen = true;
                else
                    playerSeen = false;
            }
            else
            {
                playerSeen = false;
            }
        }
        else if (playerSeen)
        {
            playerSeen = false;
        }
    }
    #endregion

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, lenghtOfVision);

        Vector3 angle01 = DirectionFromAngle(transform.eulerAngles.z, -angleOfVision / 2);
        Vector3 angle02 = DirectionFromAngle(transform.eulerAngles.z, angleOfVision / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * lenghtOfVision);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * lenghtOfVision);
        if (playerSeen)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}

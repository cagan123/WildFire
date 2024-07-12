using UnityEngine;
[RequireComponent(typeof(Waypoint))]

public class Assasin : MonoBehaviour
{
    Waypoint waypoint;
    Enemy enemy;
    public float assasinRadius;
    public bool assasinTriggered;
    private int pointIndex;
    private void Start(){
        waypoint = GetComponent<Waypoint>();
        enemy =GetComponent<Enemy>();
    }
    private void Update(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GetCurrnetPosition(), assasinRadius);
        foreach (var hit in colliders)
        {
            if(hit.GetComponentInParent<Player>() != null){
                assasinTriggered = true;
            }            
        }
    }
	public Vector3 GetCurrnetPosition(){
		return waypoint.GetPosition(pointIndex);
	}
}

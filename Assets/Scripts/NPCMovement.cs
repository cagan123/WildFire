using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [HideInInspector]public int facingDirection { get; private set; } = 1;
    [HideInInspector]protected bool facingLeft = true;
    [SerializeField] public bool startFlipped;
    Patroller NPCpatrol;
    Rigidbody2D rb;
    public float speed = 0.5f;
    private void Awake(){

        NPCpatrol = GetComponent<Patroller>();
        rb = GetComponent<Rigidbody2D>();
        if(startFlipped){
            Flip();
        }
    }
    private void Update(){
        rb.velocity = NPCpatrol.patrolDirection.normalized * speed;
        ControlFlipbyVelocity();
    }
    public void ControlFlipbyVelocity(){
        if(rb.velocity.x> 0 && facingLeft){
            Flip();
        }
        if(rb.velocity.x<0 && !facingLeft){
            Flip();
        }
    }
    public virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }


}

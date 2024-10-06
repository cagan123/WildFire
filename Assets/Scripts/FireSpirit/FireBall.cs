using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Vector2 direction;
    public float speed;
    public void Awake(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void Start(){
        StartCoroutine("DestroyRoutine");
    }
    public void FixedUpdate(){
        rb.velocity = direction * speed;
    }
    public void PassDirection(Vector2 _direction){
        direction = _direction;
    }
    public IEnumerator DestroyRoutine(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThrownRockPrefabScript : MagicAttack
{

    public GameObject spriteRenderer;
    public Rigidbody2D rb;
    public float movementSpeed;
    public float lifeTime;
    private Vector3 direction;
    void Start(){
        direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;
    }
    public override void  Update()
    {
        rb.velocity = direction * movementSpeed;
        Destroy(this, lifeTime);
    }
}

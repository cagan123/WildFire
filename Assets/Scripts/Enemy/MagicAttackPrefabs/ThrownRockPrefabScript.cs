using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThrownRockPrefabScript : MagicAttack
{

    public GameObject spriteRenderer;
    public Rigidbody2D rb;
    public float rotationSpeed;
    public float movementSpeed;
    public float lifeTime;
    private Vector3 direction;
    private MagicDamageSource damageSource => GetComponentInChildren<MagicDamageSource>();
    void Start(){
        direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;
    }
    void Update()
    {
        rb.velocity = direction * movementSpeed;
        spriteRenderer.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        Destroy(this, lifeTime);
        if(damageSource.damageDone){
            Destroy(this.gameObject);
        }
    }
}

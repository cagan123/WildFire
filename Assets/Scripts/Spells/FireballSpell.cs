using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : SpellPrefabMaster
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float lifeTime;
    private Vector3 direction;
    private SpellDamageSource damageSource => GetComponentInChildren<SpellDamageSource>();
    void Start(){
        direction = FireSpiritManager.instance.fireSpirit.FireToMouseDirection().normalized;
    }
    void Update()
    {
        rb.velocity = direction * movementSpeed;
        Destroy(this.gameObject, lifeTime);
        if(damageSource.damageDone){
            Destroy(this.gameObject);
        }
    }
}

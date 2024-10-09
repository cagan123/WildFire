using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    ParticleSystem fire => GetComponentInChildren<ParticleSystem>();
    public bool isBurning = false;
    public float burnTime;
    public bool destroyable;
    public float damageFrequency = 1f;
    public float damageRadius;
    private void Start(){
        
    }
    public virtual void Burn(){
        fire.Play();
        isBurning = true;
        if(destroyable){
            StartCoroutine("DestroyRoutine");
        }
    }
    public IEnumerator DestroyRoutine(){
        yield return new WaitForSeconds(burnTime);
        isBurning = false;
        Destroy(gameObject);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);      
    }
    public void Update(){ // preferably add a timer
        if(isBurning){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            foreach (var hit in colliders){
                if (hit.GetComponent<Enemy>() != null){
                    EnemyStats _target = hit.GetComponent<EnemyStats>();
                    hit.GetComponent<Enemy>().stats.DoDamage(_target);
                }
                    
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSource : MonoBehaviour
{
    FireSpirit fireSpirit => GetComponentInParent<FireSpirit>();
    private void OnTriggerEnter2D(Collider2D hit){
        if (hit.gameObject.GetComponent<Enemy>() != null)
        {
            DoDamageToEnemy(hit);
        }
        if(hit.gameObject.GetComponent<Burnable>() != null){
            hit.gameObject.GetComponent<Burnable>().Burn();
        }
    }
    public void DoDamageToEnemy(Collider2D _hit){
            EnemyStats _target = _hit.gameObject.GetComponent<EnemyStats>();
            fireSpirit.stats.DoDamage(_target);
        }
}

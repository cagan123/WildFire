using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageSource : MonoBehaviour
{
    public bool damageDone = false;
    private void OnTriggerEnter2D(Collider2D hit){
        if (hit.gameObject.GetComponent<Enemy>() != null)
        {
            DoDamageToEnemy(hit);
            damageDone = true;
        }
        if(hit.gameObject.GetComponent<Burnable>() != null){
            hit.gameObject.GetComponent<Burnable>().Burn();
            damageDone = true;
        }
    }
    public void DoDamageToEnemy(Collider2D _hit){
            EnemyStats _target = _hit.gameObject.GetComponent<EnemyStats>();
            FireSpiritManager.instance.fireSpirit.stats.DoDamage(_target);
        }
}

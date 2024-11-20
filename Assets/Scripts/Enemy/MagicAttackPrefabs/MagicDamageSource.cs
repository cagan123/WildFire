using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamageSource : MonoBehaviour
{
    Enemy enemy => GetComponentInParent<MagicAttack>().enemy;
    public bool damageDone = false;
    private void OnTriggerEnter2D(Collider2D hit){
        if (hit.gameObject.GetComponent<Player>() != null)
        {
            DoDamageToPlayer(hit);
            damageDone = true;
        }
    }
    public void DoDamageToPlayer(Collider2D _hit){
            PlayerStats _target = _hit.GetComponent<PlayerStats>();
            _hit.GetComponent<Player>().closestEnemyPos = enemy.transform.position;
            enemy.stats.DoDamage(_target);
        }
}

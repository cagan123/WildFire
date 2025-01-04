using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class enemyDamageSource : MonoBehaviour
{
    Enemy enemy => GetComponentInParent<Enemy>();
    private void OnTriggerEnter2D(Collider2D hit){
        if (hit.gameObject.GetComponent<Player>() != null)
        {
            DoDamageToPlayer(hit);
        }
    }
    public void DoDamageToPlayer(Collider2D _hit){
            PlayerStats _target = _hit.GetComponent<PlayerStats>();
            _hit.GetComponent<Player>().closestEnemyPos = enemy.transform.position;
            enemy.stats.DoDamage(_target);
        }
}

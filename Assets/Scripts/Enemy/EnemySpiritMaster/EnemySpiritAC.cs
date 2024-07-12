using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiritAC : MonoBehaviour
{
   private EnemySpirit enemy => GetComponentInParent<EnemySpirit>();
        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }
        private void Attack1Trigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck[0].position, enemy.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    PlayerStats _target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(_target);
                }
            }
        }
        private void Attack2Trigger()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck[1].position, enemy.attack2CheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    PlayerStats _target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(_target);
                }
            }
        }
}
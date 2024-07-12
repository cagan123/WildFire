using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dirt_Anim_Controller : MonoBehaviour
{
private Enemy_Dirt enemy => GetComponentInParent<Enemy_Dirt>();
        private void AnimationTrigger()
        {
            enemy.AnimationFinishTrigger();
        }
        private void AttackTrigger()
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
}

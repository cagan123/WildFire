using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy_Bug_namespace
{
    public class Bug_Anim_Controller : MonoBehaviour
    {
        private Enemy_Bug enemy => GetComponentInParent<Enemy_Bug>();
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

}

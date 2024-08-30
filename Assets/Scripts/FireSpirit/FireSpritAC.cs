using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpritAC : MonoBehaviour
{
    private FireSpirit fireSpirit => GetComponentInParent<FireSpirit>();

    private void AnimationTrigger()
    {
        fireSpirit.AnimationFinishTrigger();
    }
    private void Attack1Trigger()
    {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(fireSpirit.attackCheck.position, fireSpirit.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    DoDamageToEnemy(hit);
                }
            }
    }
    public void DoDamageToEnemy(Collider2D _hit){
            EnemyStats _target = _hit.GetComponent<EnemyStats>();
            fireSpirit.stats.DoDamage(_target);
        }
}

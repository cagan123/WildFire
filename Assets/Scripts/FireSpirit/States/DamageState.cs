using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : FireSpiritState
{
    public DamageState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public bool damageDone;
    public override void Enter()
    {
        base.Enter();
        damageDone = false;
    }
    public override void Exit()
    {
        base.Exit();
        damageDone = false;
    }
    public override void Update()
    {
        base.Update();
        if(!damageDone){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(fireSpirit.attackCheck.position, fireSpirit.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    DoDamageToEnemy(hit);
                }
            }
            damageDone = true;
        }
        
    }
    public void DoDamageToEnemy(Collider2D _hit){
            EnemyStats _target = _hit.GetComponent<EnemyStats>();
            fireSpirit.stats.DoDamage(_target);
        }

}

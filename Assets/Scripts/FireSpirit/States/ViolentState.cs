using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolentState : FireSpiritState
{
    public ViolentState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine) : base(_fireSpirit, _stateMachine){

    }
    

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    public override void Update()
    {
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(fireSpirit.attackCheck.position, fireSpirit.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponentInParent<Enemy>() != null){
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                hit.GetComponentInParent<Enemy>().stats.DoDamage(_target);
                if(stateMachine.currentState != fireSpirit.swordState){
                    stateMachine.ChangeState(fireSpirit.returnState);            
                }   
                
            }
            else if(hit.GetComponentInParent<Burnable>() != null){
                hit.GetComponentInParent<Burnable>().Burn();
                stateMachine.ChangeState(fireSpirit.returnState);
            }
        }
    }
}

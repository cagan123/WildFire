using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritReco3State : EnemyState
{
EnemySpirit enemy;
    public SpiritReco3State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.moveState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!enemy.noPrepDash){
            enemy.PassDashVelocity(-enemy.EnemyToPlayerDirection() * enemy.enemyAttacks[2].attackDashSpeed/2);
        }
        else{
            enemy.PassVelocity(Vector2.zero);
        }
    }
}

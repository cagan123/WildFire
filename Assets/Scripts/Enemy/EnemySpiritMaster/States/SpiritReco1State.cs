using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritReco1State : EnemyState
{
EnemySpirit enemy;
    public SpiritReco1State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        enemy.PassDashVelocity(-enemy.EnemyToPlayerDirection() * enemy.Attack1DashSpeed/2);
    }
}

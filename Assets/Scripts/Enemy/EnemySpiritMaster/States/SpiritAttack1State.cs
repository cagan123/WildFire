using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpiritAttack1State : EnemyState
{
    EnemySpirit enemy;
    public SpiritAttack1State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public Vector2 slashDirection;
    public override void Enter()
    {
        base.Enter();
        slashDirection = enemy.EnemyToPlayerDirection();      
    }
    public override void Exit()
    {
        base.Exit();
        enemy.attackCooldownTimer = enemy.attackCooldown;
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.Recover1State);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate(); 
        rb.velocity = slashDirection * enemy.dashSpeed*enemy.Attack1DashSpeed;
    }
}
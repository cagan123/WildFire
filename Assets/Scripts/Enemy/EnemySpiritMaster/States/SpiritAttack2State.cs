using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpiritAttack2State : EnemyState
{
EnemySpirit enemy;
    public SpiritAttack2State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public Vector2 thrustDirection;
    public override void Enter()
    {
        base.Enter();
        thrustDirection = enemy.EnemyToPlayerDirection();

        enemy.enemyAttacks[1].damageCollider.gameObject.SetActive(true);  
    }
    public override void Exit()
    {
        base.Exit();
        enemy.attackCooldownTimer = enemy.attackCooldown;
    
        enemy.enemyAttacks[1].damageCollider.gameObject.SetActive(false);    
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.Recover2State);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();    
        rb.velocity = thrustDirection * enemy.dashSpeed*enemy.enemyAttacks[1].attackDashSpeed;
    }
}

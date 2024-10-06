using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritIdleState : EnemyState
{
    EnemySpirit enemy;
    public SpiritIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.enemyDamageSource1.gameObject.SetActive(false);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.PassVelocity(Vector2.zero);
        base.FixedUpdate();
        if (enemy.canSeePlayer() || enemy.PlayerInAssasinArea() || enemy.EnemytoPlayerDistance() < enemy.agroDistance)
            {
                stateMachine.ChangeState(enemy.moveState);
            }
        else if(enemy.Patrolling()){
                stateMachine.ChangeState(enemy.moveState);
            }
    }
}

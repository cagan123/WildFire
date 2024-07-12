using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dirt_IdleState : EnemyState
{
    Enemy_Dirt enemy;
    public Dirt_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dirt _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.collider2d.enabled = false;
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

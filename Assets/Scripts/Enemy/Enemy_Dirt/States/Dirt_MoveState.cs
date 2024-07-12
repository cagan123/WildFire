using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_MoveState : EnemyState
{
Enemy_Dirt enemy;
    public Dirt_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dirt _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(enemy.Patrolling()){
                enemy.PassStealthVelocity(enemy.patrol.patrolDirection);
                if(enemy.canSeePlayer()){
                    enemy.patrol.playerSeen = true;
                }
            }
        else{
            enemy.PassDashVelocity(enemy.direction());
            if (enemy.EnemytoPlayerDistance() < enemy.attackCheckRadius)
            {   
                enemy.stateMachine.ChangeState(enemy.attackUpState);
            }       
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Bug_namespace
{
    public class ChaseState : EnemyState
    {
        Enemy_Bug enemy;
        public ChaseState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bug _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(enemy.Patrolling()){
                enemy.ControlFlipforEnemybyVelocity();
                enemy.PassStealthVelocity(enemy.patrol.patrolDirection);
                if(enemy.canSeePlayer()){
                    enemy.patrol.playerSeen = true;
                }
            }
        else{
            enemy.ControlFlipforEnemytoPlayer();
            enemy.PassDashVelocity(enemy.direction());
            if (enemy.EnemytoPlayerDistance() < enemy.attackCheckRadius){   
                enemy.stateMachine.ChangeState(enemy.attackState);
                }       
            }       
        }
    }
}

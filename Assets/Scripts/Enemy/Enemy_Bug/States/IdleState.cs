using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace Enemy_Bug_namespace
{
    public class IdleState : EnemyState
    {
        public Enemy_Bug enemy;
        public IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bug _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        }

        public override void Update()
        {
            base.Update();

            if (enemy.canSeePlayer() || enemy.PlayerInAssasinArea() || enemy.EnemytoPlayerDistance() < enemy.agroDistance)
            {
                stateMachine.ChangeState(enemy.chaseState);
            }
            else if(enemy.Patrolling()){
                stateMachine.ChangeState(enemy.chaseState);
            }
        }
    }
}

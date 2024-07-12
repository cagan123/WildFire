using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Bug_namespace
{
    public class AttackState : EnemyState
    {
        Enemy_Bug enemy;
        public AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Bug _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
            enemy.lastTimeAttacked = Time.time;
        }

        public override void FixedUpdate()
        {
            enemy.ControlFlipforEnemytoPlayer();
            base.FixedUpdate();
            enemy.PassVelocity(Vector2.zero);
            if (triggerCalled)
            {
                stateMachine.ChangeState(enemy.chaseState);
            }
        }

        public override void Update()
        {
            
            base.Update();
        }
    }
}
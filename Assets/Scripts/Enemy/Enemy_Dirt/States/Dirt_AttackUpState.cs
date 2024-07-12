using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_AttackUpState : Dirt_DamagableState
{
    Enemy_Dirt enemy;
    public Dirt_AttackUpState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dirt _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
        enemy = _enemy;
    }

    public override void Enter()
        {
            base.Enter();
            enemy.PassVelocity(Vector2.zero);
        }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.attackDownState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

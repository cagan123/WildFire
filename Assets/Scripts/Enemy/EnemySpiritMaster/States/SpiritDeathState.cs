using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritDeathState : EnemyState
{
    EnemySpirit enemy;
    public SpiritDeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
        rb.velocity = Vector2.zero;
    }

}

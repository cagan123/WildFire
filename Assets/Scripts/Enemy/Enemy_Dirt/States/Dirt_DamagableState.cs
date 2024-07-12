using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_DamagableState : EnemyState
{
Enemy_Dirt enemy;
    public Dirt_DamagableState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Dirt _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.collider2d.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.collider2d.enabled = false;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        enemy.PassVelocity(Vector2.zero);
    }
}

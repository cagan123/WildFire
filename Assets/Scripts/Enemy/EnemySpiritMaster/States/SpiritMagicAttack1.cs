using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpiritMagicAttack1 : EnemyState
{
    EnemySpirit enemy;
    public SpiritMagicAttack1(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.InstantiateInState(enemy.magicAttacks[0].MagicPrefab, enemy.transform.position, Quaternion.identity);
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}

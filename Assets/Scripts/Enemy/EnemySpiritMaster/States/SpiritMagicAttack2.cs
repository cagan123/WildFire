using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMagicAttack2 : EnemyState
{
    EnemySpirit enemy;
    public SpiritMagicAttack2(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.InstantiateInState(enemy.magicAttacks[1].MagicPrefab, enemy.transform.position, Quaternion.identity);
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMagicPrep2 : EnemyState
{
    EnemySpirit enemy;
    public SpiritMagicPrep2(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.magicAttacks[1].prepDuration;
    }
    public override void Update()
    {
        base.Update();
        enemy.ControlFlipforEnemytoPlayer();      
        if(stateTimer < 0){
            stateTimer = enemy.magicAttacks[1].prepDuration;
            stateMachine.ChangeState(enemy.MagicAttack2);
        }        
    }
}

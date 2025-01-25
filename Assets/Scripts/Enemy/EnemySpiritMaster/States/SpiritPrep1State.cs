using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPrep1State : EnemyState
{
    EnemySpirit enemy;
    public SpiritPrep1State(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.enemyAttacks[0].prepDuration;
        enemy.PassDashVelocity(Vector2.zero);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        enemy.ControlFlipforEnemytoPlayer();      
        if(stateTimer < 0){
            stateTimer = enemy.enemyAttacks[0].prepDuration;
            stateMachine.ChangeState(enemy.Attack1State);
        }        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();    
        if(!enemy.noPrepDash){
            if(enemy.EnemytoPlayerDistance() <= enemy.enemyAttacks[0].attackRange){
                enemy.PassDashVelocity(-enemy.EnemyToPlayerDirection() * enemy.enemyAttacks[0].attackDashSpeed/2);
            }
            else{
                enemy.PassDashVelocity(enemy.EnemyToPlayerDirection() * enemy.enemyAttacks[0].attackDashSpeed/2);
            }
        }
        else{
            enemy.PassDashVelocity(Vector2.zero);
        }
    }
}

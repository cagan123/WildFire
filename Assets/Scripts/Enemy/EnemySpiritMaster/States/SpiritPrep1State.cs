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
        stateTimer = enemy.prepDuration1;
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
            stateTimer = enemy.prepDuration1;
            stateMachine.ChangeState(enemy.Attack1State);
        }        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();    
        if(!enemy.IsBoss){
            if(enemy.EnemytoPlayerDistance() <= enemy.attackDistance){
                enemy.PassDashVelocity(-enemy.EnemyToPlayerDirection() * enemy.Attack1DashSpeed/2);
            }
            else{
                enemy.PassDashVelocity(enemy.EnemyToPlayerDirection() * enemy.Attack2DashSpeed/2);
            }
        }
        else{
            enemy.PassVelocity(Vector2.zero);
        }
    }
}

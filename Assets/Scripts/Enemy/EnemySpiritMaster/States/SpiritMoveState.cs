using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMoveState : EnemyState
{
    EnemySpirit enemy;
    int attackDeterminator;
    public SpiritMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySpirit _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    
    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.attackCooldownTimer;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();      
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        if(enemy.Patrolling()){
                
                enemy.ControlFlipforEnemybyVelocity();
                enemy.PassRunVelocity(enemy.patrol.patrolDirection);
                if(enemy.canSeePlayer() || enemy.EnemytoPlayerDistance() < enemy.agroDistance){
                    enemy.patrol.playerSeen = true;
                }
            }
        else{
            enemy.PassDashVelocity(enemy.direction());
            enemy.ControlFlipforEnemytoPlayer();
            if(rb.velocity == Vector2.zero){
                stateMachine.ChangeState(enemy.idleState);
            }
            attackDeterminator = Random.Range(0, enemy.attackNumber);
            if (enemy.EnemytoPlayerDistance() < enemy.attackDistance)
            {   
                switch(attackDeterminator){
                    case 0:
                        stateMachine.ChangeState(enemy.Prep1State);
                        break;
                    case 1:
                        stateMachine.ChangeState(enemy.Prep2State);
                        break;
                    case 2:
                        stateMachine.ChangeState(enemy.Prep3State);
                        break;
                }
            }       
        }
    }
}

//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMoveState : EnemyState
{
    EnemySpirit enemy;
    int attackDeterminator;
    private EnemyState lastChosenPrep;
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
            AttackDesicion();
        }
    }

    public void AttackDesicion(){


        // Create a list to store valid preps and weights
        List<(EnemyState, float)> weightedPreps = new List<(EnemyState, float)>();

        // Check regular attacks
        for (int i = 0; i < enemy.enemyAttacks.Count; i++)
        {
            if (enemy.EnemytoPlayerDistance() <= enemy.enemyAttacks[i].attackRange)
            {
                EnemyState prepState = null;

                // Map states based on index
                switch (i)
                {
                    case 0: prepState = enemy.Prep1State; break;
                    case 1: prepState = enemy.Prep2State; break;
                    case 2: prepState = enemy.Prep3State; break;
                }

                if (prepState != null)
                {
                    float weight = enemy.enemyAttacks[i].baseWeight;

                    // Penalize the last chosen prep
                    if (prepState == lastChosenPrep)
                    {
                        weight *= 0.75f; // Reduce its weight by 75% (adjust as needed)
                    }

                    weightedPreps.Add((prepState, weight));
                }
            }
        }

        // Check magical attacks
        for (int i = 0; i < enemy.magicAttacks.Count; i++)
        {
            if (enemy.EnemytoPlayerDistance() <= enemy.magicAttacks[i].attackRange)
            {
                EnemyState prepState = null;

                // Map states based on index
                switch (i)
                {
                    case 0: prepState = enemy.MagicPrep1; break;
                    case 1: prepState = enemy.MagicPrep2; break;
                    case 2: prepState = enemy.MagicPrep3; break;
                }

                if (prepState != null)
                {
                    float weight = enemy.magicAttacks[i].baseWeight;

                    // Penalize the last chosen prep
                    if (prepState == lastChosenPrep)
                    {
                        weight *= 0.75f; // Reduce its weight by 75% (adjust as needed)
                    }

                    weightedPreps.Add((prepState, weight));
                }
            }
        }

        // Determine if enough attacks are valid
        int totalAttacks = enemy.enemyAttacks.Count + enemy.magicAttacks.Count;

        if (weightedPreps.Count >= 2 || totalAttacks == 1)
        {
            // Calculate total weight
            float totalWeight = 0f;
            foreach (var (_, weight) in weightedPreps)
            {
                totalWeight += weight;
            }

            // Pick a random value within the total weight
            float randomValue = Random.Range(0, totalWeight);

            // Find the selected prep based on the random value
            float cumulativeWeight = 0f;
            foreach (var (prep, weight) in weightedPreps)
            {
                cumulativeWeight += weight;
                if (randomValue <= cumulativeWeight)
                {
                    stateMachine.ChangeState(prep);

                    // Update the last chosen prep
                    lastChosenPrep = prep;
                    break;
                }
            }
            
        }
    }
}



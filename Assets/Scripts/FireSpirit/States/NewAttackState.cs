using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class NewAttackState : DamageState
{
    public NewAttackState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
        
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = fireSpirit.burstDuration;
        fireSpirit.TriggerBurst();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }
    public override void Update()
    {
        base.Update();
        if(stateTimer < 0){
            stateMachine.ChangeState(fireSpirit.followState);
        }
                
    }
}

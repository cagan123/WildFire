using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritPrep1State : FollowBehaviorState
{
    public FireSpiritPrep1State(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = fireSpirit.prepDuration1;
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
            stateTimer = fireSpirit.prepDuration1;
            stateMachine.ChangeState(fireSpirit.attack1State);
        } 
    }
}
public class FireSpiritPrepBackState : FollowBehaviorState
{
    public FireSpiritPrepBackState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = fireSpirit.prepDuration1;
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
            stateTimer = fireSpirit.prepDuration1;
            stateMachine.ChangeState(fireSpirit.attackbackState);
        } 
    }
}

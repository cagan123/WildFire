using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritShieldState : FollowBehaviorState
{
    public FireSpiritShieldState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonUp(1)){
            stateMachine.ChangeState(fireSpirit.followState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

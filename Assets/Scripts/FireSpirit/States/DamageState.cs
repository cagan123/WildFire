using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : FollowBehaviorState
{
    public DamageState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        fireSpirit.damageSourceActive = true;
    }
    public override void Exit()
    {
        base.Exit();
        fireSpirit.damageSourceActive = false;
    }

}

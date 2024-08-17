using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReturnState : ViolentState
{
    public ReturnState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
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
        Vector2 PlayerDir = fireSpirit.FireToPlayerDirection();
        fireSpirit.PassDashVelocity(PlayerDir);
        if (fireSpirit.distanceBetweenPlayerandFireSpirit <= 1.5f)
        {
            stateMachine.ChangeState(fireSpirit.followState);
        }
    }
}

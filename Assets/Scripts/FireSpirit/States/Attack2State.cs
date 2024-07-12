using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2State : ViolentState
{
    public Attack2State(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine) : base(_fireSpirit, _stateMachine)
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
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        
    }
    public override void Update()
    {
        base.Update();
        Vector2 newAttackDirection = Vector2.Perpendicular(fireSpirit.FireToMouseDirection());
        
    }
}

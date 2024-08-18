using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviorState : FireSpiritState
{
    public FollowBehaviorState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
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
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(Vector2.Distance(fireSpirit.transform.position, fireSpirit.PointBeweenPlayerandMouse()) < .1f){
            fireSpirit.PassVelocity(Vector2.zero);
        }
        else{
            fireSpirit.PassDashVelocity(fireSpirit.DirectionToPointBeweenPlayerandMouse());
        }
    }

}

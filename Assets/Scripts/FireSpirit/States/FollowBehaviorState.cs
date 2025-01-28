using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviorState : FireSpiritState
{
    public FollowBehaviorState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public Vector2 getPos;
    public Vector2 getDirection;
    public override void Enter()
    {
        base.Enter();
        getPos = fireSpirit.PointBeweenPlayerandMouse();
        getDirection = fireSpirit.DirectionToPointBeweenPlayerandMouse();

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
        if(Vector2.Distance(fireSpirit.transform.position, getPos) < .2f){
            fireSpirit.PassVelocity(Vector2.zero);
        }
        else{
            fireSpirit.PassDashVelocity( 3* getDirection);
        }
    }
}

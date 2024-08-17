using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : ViolentState
{
    public AttackState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        MouseDir = fireSpirit.FireToMouseDirection();
        MousePos = fireSpirit.MousePosition();
        RealMousePos = fireSpirit.cam.ScreenToWorldPoint(MousePos);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(fireSpirit.transform.position, fireSpirit.transformToFollow.position) > fireSpirit.spearDistance)
        {
            stateMachine.ChangeState(fireSpirit.returnState);
        }
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        fireSpirit.PassDashVelocity(MouseDir); // it is dash velocity so it is faster 
    }
    
}

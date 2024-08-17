using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowState : FireSpiritState
{
    public FollowState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
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
        if (Input.GetMouseButton(0)){
            stateMachine.ChangeState(fireSpirit.powerUpState);
        }

        if (Input.GetMouseButton(1)){
            stateMachine.ChangeState(fireSpirit.swordState);
        }
        
    
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (fireSpirit.distanceBetweenPlayerandFireSpirit < 1f)
        {
            fireSpirit.PassVelocity(0f, 0f);

        }
        else if (fireSpirit.distanceBetweenPlayerandFireSpirit > 50f) // just in case
        {
            stateMachine.ChangeState(fireSpirit.returnState);
        }
        else
        {
            fireSpirit.PassVelocity(fireSpirit.FireToPlayerDirection());
        }

    }
}

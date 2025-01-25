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
        //damageSource.gameObject.SetActive(false);
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
        if(fireSpirit.distanceBetweenPlayerandFireSpirit <= fireSpirit.followDistance){
            fireSpirit.PassVelocity(Vector2.zero);
        }
        else{
            fireSpirit.PassVelocity(fireSpirit.FireToPlayerDirection());
        }
    }
}

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
        damageSource.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0) && fireSpirit.stats.HasEnoughStamina(20)){
            if(fireSpirit.attackCounter % 2 == 1){
                stateMachine.ChangeState(fireSpirit.prep1State);
            }
            else{
                stateMachine.ChangeState(fireSpirit.prepBackState);
            }
        }
        if(Input.GetMouseButton(1) && fireSpirit.stats.HasEnoughStamina(fireSpirit.spells[0].staminaUse)){
            stateMachine.ChangeState(fireSpirit.fireballState);
        }
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

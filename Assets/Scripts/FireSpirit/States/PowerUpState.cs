using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;

public class PowerUpState : FireSpiritState
{
    public PowerUpState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine) : base(_fireSpirit, _stateMachine)
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
        fireSpirit.PassVelocity(fireSpirit.FireToPlayerDirection());

        if (fireSpirit.distanceBetweenPlayerandFireSpirit < 1f)
            fireSpirit.PassVelocity(Vector2.zero);

        if (Input.GetMouseButtonUp(0))
        {
            attackPower = Mathf.Lerp(fireSpirit.minChargePower, fireSpirit.maxChargePower, fireSpirit.currentChargeTime / fireSpirit.maxChargeTime);
            stateMachine.ChangeState(fireSpirit.attackState);
            fireSpirit.currentChargeTime = 0.0f;
        }

    }
    public override void Update()
    {
        base.Update();
        fireSpirit.currentChargeTime += fireSpirit.chargeRate * Time.deltaTime;
        fireSpirit.currentChargeTime = Mathf.Clamp(fireSpirit.currentChargeTime, 0.0f, fireSpirit.maxChargeTime);  

        if (Input.GetMouseButtonUp(0))
        {
            attackPower = Mathf.Lerp(fireSpirit.minChargePower, fireSpirit.maxChargePower, fireSpirit.currentChargeTime / fireSpirit.maxChargeTime);
            stateMachine.ChangeState(fireSpirit.attackState);
            fireSpirit.currentChargeTime = 0.0f;
        }
    }
}

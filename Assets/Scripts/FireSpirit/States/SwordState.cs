using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SwordState : ViolentState
{
    public SwordState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine) : base(_fireSpirit, _stateMachine)
    {
    }
    Quaternion originalRotation;
    float originalAttackCheckRadius;
    public override void Enter()
    {
        base.Enter();
        //stateTimer = fireSpirit.swordTime;
        var emission = fireSpirit.fire.emission;
        emission.rateOverTime = 60f;
        var shape = fireSpirit.fire.shape;
        shape.radius = 3f;
        rb.freezeRotation = false;
        originalRotation = fireSpirit.transform.rotation;
        fireSpirit.attackCheckRadius = fireSpirit.SwordAttackCheckRadius;
    }

    public override void Exit()
    {
        base.Exit();
        var emission = fireSpirit.fire.emission;
        emission.rateOverTime = 16f;
        var shape = fireSpirit.fire.shape;
        shape.radius = 0.0001f;
        rb.freezeRotation = true;
        fireSpirit.transform.rotation = originalRotation;
        fireSpirit.attackCheckRadius = originalAttackCheckRadius;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 direction = fireSpirit.PlayerToMouseDirection();        
        Vector3 targetPosition = fireSpirit.transformToFollow.position + direction * fireSpirit.SwordDistance;
        Vector2 newPosition = Vector2.Lerp(rb.position, targetPosition, fireSpirit.followSmoothness);
        rb.MovePosition(newPosition);      
        //rotation
        float currentAngularVelocity = rb.angularVelocity;
        float targetAngularVelocity = fireSpirit.rotationTorque;
        float newAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetAngularVelocity, fireSpirit.torqueSmoothnes);
        rb.angularVelocity = newAngularVelocity;
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyUp(KeyCode.Mouse1)) // Right mouse button
        {
            stateMachine.ChangeState(fireSpirit.followState);
        }       
    }
}

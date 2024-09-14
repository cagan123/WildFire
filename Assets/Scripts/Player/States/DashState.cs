using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class DashState : PlayerState
{
    public DashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    private float dashInputX;
    private float dashInputY;
    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        dashInputX = Input.GetAxis("Horizontal");
        dashInputY = Input.GetAxis("Vertical");;
    }

    public override void Exit()
    {
        base.Exit();
        player.PassVelocity(0,0);    
        player.CooldownTimer = player.Cooldown;   
    }

    public override void Update()
    {
        base.Update();

        player.PassDashVelocity(dashInputX, dashInputY);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}

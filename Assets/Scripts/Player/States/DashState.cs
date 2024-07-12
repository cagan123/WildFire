using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        
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

        player.PassDashVelocity(xInput, yInput);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}

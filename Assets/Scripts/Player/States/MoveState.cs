using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.PassVelocity(0f,0f);
        
    }

    public override void Update()
    {
        base.Update();

        player.PassVelocity(xInput, yInput);

        if (!thereIsMovementInput)        
            stateMachine.ChangeState(player.idleState);
        
        if (thereIsMovementInput && Input.GetKeyDown(KeyCode.Space) && player.CooldownTimer< 0)
          stateMachine.ChangeState(player.dashState);        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        rb.velocity = Vector2.zero;
        if (thereIsMovementInput)
            stateMachine.ChangeState(player.moveState);
    }
}

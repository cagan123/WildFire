using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundedState
{
    public RunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.anim.speed = 1.4f;
    }
    public override void Exit()
    {
        base.Exit();
        player.PassVelocity(0f,0f);
        player.anim.speed = 1f;
    }
    public override void Update()
    {
        base.Update();

        player.PassRunVelocity(xInput, yInput);

        if(thereIsMovementInput && Input.GetKeyUp(KeyCode.LeftShift))
            stateMachine.ChangeState(player.moveState);
    }
}

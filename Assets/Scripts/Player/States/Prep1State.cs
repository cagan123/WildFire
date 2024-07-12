using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prep1State : PlayerState
{
    public Prep1State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = player.prep1time;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        player.PassVelocity(Vector2.zero);
        if(stateTimer < 0){
            stateTimer = player.prep1time;
            stateMachine.ChangeState(player.attack1State);
        }
    }
}

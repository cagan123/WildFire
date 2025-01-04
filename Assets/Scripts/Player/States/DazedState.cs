using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazedState : PlayerState
{
    public DazedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateTimer = 2f;
    }
    public override void Exit()
    {
        base.Exit();
        player.stats.ResetPoise();
    }
    public override void Update()
    {
        base.Update();
        player.PassVelocity(Vector2.zero);
        if(stateTimer <= 0){
            stateMachine.ChangeState(player.idleState);
        }
    }
}

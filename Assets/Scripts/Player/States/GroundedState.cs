using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    public GroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter(); 
    }
    
    public override void Update()
    {
        base.Update();
        if (!thereIsMovementInput)        
            stateMachine.ChangeState(player.idleState);  
    }
}

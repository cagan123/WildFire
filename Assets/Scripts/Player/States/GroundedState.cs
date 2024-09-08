using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    public GroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    SpellManager spell;
    public override void Enter()
    {
        base.Enter();
        spell = SpellManager.instance;  
    }
    
    public override void Update()
    {
        base.Update();
        if (!thereIsMovementInput)        
            stateMachine.ChangeState(player.idleState);  
    }
}

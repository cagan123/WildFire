using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : PlayerState
{
    public Attack1State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    private int goToUpSwing = 0;
    public override void Enter()
    {
        base.Enter();
        goToUpSwing = 0;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        player.PassStealthVelocity(xInput, yInput);
        if(Input.GetMouseButtonDown(0)){
            goToUpSwing = 1;            
        }
        if(triggerCalled){
            switch(goToUpSwing){
            case 0:
                stateMachine.ChangeState(player.reco1State);
                break;
            case 1:
                stateMachine.ChangeState(player.up1State);
                break;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            stateMachine.ChangeState(player.dashState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up1State : PlayerState
{
    public Up1State(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    private int goTothrust = 0;
    public override void Enter()
    {
        base.Enter();
        goTothrust = 0;
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
            goTothrust = 1;            
        }
        if(triggerCalled){
            switch(goTothrust){
            case 0:
                stateMachine.ChangeState(player.recoUpState);
                break;
            case 1:
                stateMachine.ChangeState(player.attack1State);
                break;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            stateMachine.ChangeState(player.dashState);
        }
    }
}

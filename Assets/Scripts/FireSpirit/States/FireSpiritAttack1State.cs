using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritAttack1State : FollowBehaviorState
{
    public FireSpiritAttack1State(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    private bool AttackInputGiven;
    public override void Enter()
    {
        base.Enter();
        AttackInputGiven = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0)){
            AttackInputGiven = true;
        }
        
        if(triggerCalled){
            if(AttackInputGiven){
                stateMachine.ChangeState(fireSpirit.prepBackState);
            }
            else{
                stateMachine.ChangeState(fireSpirit.reco1State);
            }           
        }
    }
}
public class FireSpiritAttackBackState : FollowBehaviorState
{
    public FireSpiritAttackBackState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
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
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Update()
    {
        base.Update();
        if(triggerCalled){
            stateMachine.ChangeState(fireSpirit.recoBackState);
        }
    }
}

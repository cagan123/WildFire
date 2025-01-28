using UnityEngine;

public class FireballState : FireSpiritState
{
    public FireballState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
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

        if(stateTimer<0){
            stateMachine.ChangeState(fireSpirit.followState);
        }
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(fireSpirit.distanceBetweenPlayerandFireSpirit <= fireSpirit.followDistance){
            fireSpirit.PassVelocity(Vector2.zero);
        }
        else{
            fireSpirit.PassVelocity(fireSpirit.FireToPlayerDirection());
        }
    }



}

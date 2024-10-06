using UnityEngine;

public class FireballState : FireSpiritState
{
    public FireballState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName) : base(_fireSpirit, _stateMachine, _animBoolName)
    {
    }
    public Vector2 direction;
    public override void Enter()
    {
        base.Enter();
        direction = fireSpirit.PlayerToMouseDirection();
        fireSpirit.CreateFireball(direction);
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(fireSpirit.followState);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dirt : Enemy
{
    #region States
    public Dirt_IdleState idleState{get; private set;}
    public Dirt_MoveState moveState{get; private set;}
    public Dirt_DamagableState damagableState{get; private set;}
    public Dirt_AttackUpState attackUpState{get; private set;}
    public Dirt_AttackDownState attackDownState{get; private set;}

    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new Dirt_IdleState(this,stateMachine, "idle", this);
        moveState = new Dirt_MoveState(this, stateMachine, "idle", this);
        damagableState = new Dirt_DamagableState(this, stateMachine, null, this);
        attackUpState = new Dirt_AttackUpState(this, stateMachine, "attackUp", this);
        attackDownState = new Dirt_AttackDownState(this, stateMachine, "attackDown", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    protected void Update()
    {           
        stateMachine.currentState.Update();
    }
}

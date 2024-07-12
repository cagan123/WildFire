using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireSpiritState 
{
    #region Components
    protected FireSpiritStateMachine stateMachine;
    protected FireSpirit fireSpirit;
    protected ParticleSystem fireToPlay;
    protected Rigidbody2D rb;
    #endregion

    #region Variables

    protected float stateTimer;
    protected bool triggerCalled;
    public Vector2 RealMousePos;
    public Vector2 RealPos;
    public float attackPower;
    public float Distance { get; protected set; }
    public Vector2 CurrentVelocity{ get; protected set; }
    public Vector2 MouseDir { get; protected set; } 
    public Vector2 MousePos { get; protected set; }
    public Vector2 Position { get; protected set; }
    public Vector2 PlayerPosition { get; protected set; }

    #endregion


    public FireSpiritState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine)
    {
        this.fireSpirit = _fireSpirit;
        this.stateMachine = _stateMachine;
    }

    public virtual void Enter()
    {
        rb = fireSpirit.rb;
        Distance = fireSpirit.distanceBetweenPlayerandFireSpirit;
    }

    public virtual void Update()
    {
        
        PlayerPosition = fireSpirit.cam.WorldToScreenPoint(fireSpirit.transformToFollow.position);
        RealPos = fireSpirit.cam.ScreenToWorldPoint(Position);
    }

    public virtual void FixedUpdate()
    {
        stateTimer -= Time.fixedDeltaTime; // this is used by other states
        CurrentVelocity = rb.velocity;
        Position = fireSpirit.cam.WorldToScreenPoint(rb.position);
    }
    public virtual void Exit()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpirit : Enemy
{
    #region Variables
    public float Attack1DashSpeed = 1.5f;
    public float Attack2DashSpeed = 2f;
    public float Attack3DashSpeed;
    #endregion

    #region States
    public SpiritIdleState idleState{ get; private set; }
    public SpiritMoveState moveState{ get; private set; }
    public SpiritPrep1State Prep1State{ get; private set; }
    public SpiritAttack1State Attack1State{ get; private set; }
    public SpiritReco1State Recover1State{ get; private set; }
    public SpiritPrep2State Prep2State{ get; private set; }
    public SpiritAttack2State Attack2State{ get; private set; }
    public SpiritReco2State Recover2State{ get; private set;}
    public SpiritPrep3State Prep3State{ get; private set; }
    public SpiritAttack3State Attack3State{ get; private set; }
    public SpiritReco3State Recover3State{ get; private set;}
    public SpiritDeathState DeathState{ get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SpiritIdleState(this, stateMachine, "idle", this);
        moveState = new SpiritMoveState(this, stateMachine, "move", this);

        Prep1State = new SpiritPrep1State(this, stateMachine, "prep1", this);
        Attack1State = new SpiritAttack1State(this, stateMachine, "attack1", this);
        Recover1State = new SpiritReco1State(this, stateMachine, "reco1", this);

        Prep2State = new SpiritPrep2State(this, stateMachine, "prep2", this);
        Attack2State = new SpiritAttack2State(this, stateMachine, "attack2", this);
        Recover2State = new SpiritReco2State(this, stateMachine, "reco2", this);

        Prep3State = new SpiritPrep3State(this, stateMachine, "prep3", this);
        Attack3State = new SpiritAttack3State(this, stateMachine, "attack3", this);
        Recover3State = new SpiritReco3State(this, stateMachine, "reco3", this);

        DeathState = new SpiritDeathState(this, stateMachine, "death", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        GetDeathState(DeathState);
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

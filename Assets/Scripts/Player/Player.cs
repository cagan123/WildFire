using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : Entity
{
    [Header("Dash Info")]
    [SerializeField] public float dashDuration = 0.4f;
    [SerializeField] public float Cooldown = .3f;
    [HideInInspector]public float CooldownTimer;

    [Header("Attack Info")]
    public float prep1time;
    public float attack1speed;

    #region Components
    Camera cam;
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public DashState dashState { get; private set; }
    public Prep1State prep1state { get; private set; }
    public Attack1State attack1State{ get; private set; }
    public Reco1Sstate reco1State{ get; private set; }
    public Up1State up1State{ get; private set;}
    public RecoUpState recoUpState{ get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;

        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine, "Idle");
        moveState = new MoveState(this, stateMachine, "Run"); 
        dashState = new DashState(this, stateMachine, "dash"); 

        prep1state = new Prep1State(this, stateMachine, "swordprep");
        attack1State = new Attack1State(this, stateMachine, "sworddown");
        reco1State = new Reco1Sstate(this, stateMachine, "swordreco");
        up1State = new Up1State(this, stateMachine, "swordup");
        recoUpState = new RecoUpState(this, stateMachine, "swordrecoup");

    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        CooldownTimer = Cooldown;
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        ControlFlip();

        DashCooldownTimer();

        CheckForDashInput();
    }
    

    #region Timer
    public void DashCooldownTimer()
    {
        CooldownTimer -= Time.deltaTime;
    }
    #endregion

    public void CheckForDashInput(){
        if (stateMachine.currentState == moveState && Input.GetKeyDown(KeyCode.Space) && CooldownTimer< 0)
            stateMachine.ChangeState(dashState);
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public Vector2 PlayerToMouseDirection(){
    var direction = (Input.mousePosition - cam.WorldToScreenPoint(transform.position)).normalized;
    return direction;
    }
}

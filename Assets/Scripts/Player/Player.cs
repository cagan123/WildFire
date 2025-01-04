using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : Entity
{
    [Header("Dash Info")]
    [SerializeField] public float dashDuration = 0.4f;
    [SerializeField] public float Cooldown = .3f;
    [HideInInspector]public float CooldownTimer;

    [Header("Stamina")]
    [SerializeField] public int staminaRecoveryRate;
    [SerializeField] public int dashStamina;
    [SerializeField] public int runStamina;

    [Header("Attack Info")]
    public float prep1time;
    public float attack1speed;
    public Vector2 closestEnemyPos;

    [Header("Input Buffer Info")]
    private Queue <float> dashInputBuffer = new Queue <float>();
    [SerializeField] private float dashInputBufferTime = 0.2f; // Buffer duration in seconds

    #region Components
    Camera cam;

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public DashState dashState { get; private set; }
    public RunState runState { get; private set; }
    public GroundedState groundedState{get ;private set; }
    public DeathState deathState{ get; private set; }
    public DazedState dazedState{ get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine, "Idle");
        moveState = new MoveState(this, stateMachine, "Run"); 
        dashState = new DashState(this, stateMachine, "dash"); 
        runState = new RunState(this, stateMachine, "Run");
        groundedState = new GroundedState(this, stateMachine, null);
        dazedState = new DazedState(this, stateMachine, "dazed");
        
        deathState = new DeathState(this, stateMachine, "death");

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
        CheckForDashInputAndBuffer();    
        FlipByMovement();
        DashCooldownTimer();
        AnimMoveSetter();
        stats.StaminaRecovery();
        stats.PoiseRecovery();
        if(stats.poiseBroken){
            stateMachine.ChangeState(dazedState);
        }
    }
    

    #region Timer
    public void DashCooldownTimer()
    {
        CooldownTimer -= Time.deltaTime;
        if(CooldownTimer >= 0 && Input.GetKeyDown(KeyCode.Space)){
            VFX.NotEnoughStaminaVFX();
        }
    }
    #endregion

    public void CheckForDashInputAndBuffer(){
        // Store input if Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashInputBuffer.Enqueue(Time.time);
        }

        // Process buffered input
        if (dashInputBuffer.Count > 0)
        {
            float bufferedInputTime = dashInputBuffer.Peek();

            // Check if the buffered input is valid and dash conditions are met
            if (Time.time - bufferedInputTime <= dashInputBufferTime &&
                stateMachine.currentState == moveState &&
                CooldownTimer <= 0 &&
                stats.HasEnoughStamina(dashStamina))
            {
                // Execute dash
                stateMachine.ChangeState(dashState);
                dashInputBuffer.Dequeue(); // Remove processed input
            }
        else if (Time.time - bufferedInputTime > dashInputBufferTime)
        {
            // Discard expired input
            dashInputBuffer.Dequeue();
        }
    }
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public Vector2 PlayerToMouseDirection(){

    var direction = (Input.mousePosition - cam.WorldToScreenPoint(transform.position)).normalized;
    return direction;
    }

    public Vector2 KnockbackDirection(){
        return ((Vector2)transform.position - closestEnemyPos).normalized;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
    #region Flip
    public void AnimMoveSetter(){
        anim.SetFloat("move_Y", rb.velocity.y);
    }
    public void Flip_X()
    {
        facingDirection = facingDirection * -1;
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }
    public void Flip_Y(){
        facingUp = !facingUp;
        anim.SetBool("isLookingUp", facingUp);
    }
    public void FlipByMovement(){
        if(rb.velocity.x > 0 && !facingLeft){
            Flip_X();
        }
        if(rb.velocity.x < 0 && facingLeft){
            Flip_X();
        }
        if(rb.velocity.y > 0 && !facingUp){                      
            Flip_Y();
        }
        if(rb.velocity.y < 0 && facingUp){
            Flip_Y();
        }
    }
    #endregion
}

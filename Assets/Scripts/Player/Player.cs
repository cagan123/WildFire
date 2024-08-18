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
    public Vector2 closestEnemyPos;

    #region Components
    Camera cam;
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public DashState dashState { get; private set; }
    public DeathState deathState{ get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;

        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine, "Idle");
        moveState = new MoveState(this, stateMachine, "Run"); 
        dashState = new DashState(this, stateMachine, "dash"); 
        
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

        CheckForDashInput();
        
        ControlFlip();

        DashCooldownTimer();

        
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

    public Vector2 KnockbackDirection(){
        return ((Vector2)transform.position - closestEnemyPos).normalized;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }

}

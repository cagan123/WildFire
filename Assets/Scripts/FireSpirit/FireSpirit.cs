using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class FireSpirit : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] float movementSpeed;
    [SerializeField] public float dashSpeed;
    public float DashVelocitySmoothness = .1f;

    [Header("Attack Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    public float spearDistance = 3f;

    [Header("Charge Up Info")]
    public float chargeRate = 2.5f;
    public float maxChargeTime = 3.0f;
    public float minChargePower = 1.0f;
    public float maxChargePower = 5.0f;
    public float currentChargeTime = 0.0f;
    public float sizeMultipilier = 2f;
    [Header("Sword Info")]
    public float SwordDistance = 2f;
    public float rotationTorque = 50f;
    public float followSmoothness = .1f;
    public float torqueSmoothnes = .1f;
    public float SwordAttackCheckRadius = 1f;
    public float swordTime = 2f;

    #region Components
    [HideInInspector] public Transform transformToFollow{get;private set;}
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    [HideInInspector]public Camera cam;
    #endregion

    #region Variables
    public float distanceBetweenPlayerandFireSpirit { get; private set; }
    
    #endregion

    #region States
    public FireSpiritStateMachine stateMachine { get; private set; }
    public FollowState followState { get; private set; }
    public AttackState attackState { get; private set; }  
    public ReturnState returnState { get; private set; }
    public PowerUpState powerUpState { get; private set; }
    public ViolentState violentState { get; private set; }
    public SwordState swordState { get; private set; }
    public Attack2State attack2State{ get; private set; }

    #endregion
   
    [HideInInspector]public ParticleSystem fire;

    private void Awake()
    {
        stateMachine = new FireSpiritStateMachine();
        followState = new FollowState(this, stateMachine, null);
        attackState = new AttackState(this, stateMachine, null);
        attack2State = new Attack2State(this, stateMachine, null);
        returnState = new ReturnState(this, stateMachine, null);
        powerUpState = new PowerUpState(this, stateMachine, null);
        violentState = new ViolentState(this,stateMachine, null);
        swordState = new SwordState(this, stateMachine, "move");

        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();       
        fire = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        stateMachine.Initialize(followState);
        transformToFollow = PlayerManager.instance.player.transform;
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
        
    }
    private void Update()
    {
        stateMachine.currentState.Update();
        distanceBetweenPlayerandFireSpirit = Vector2.Distance(transform.position, transformToFollow.position);
    }
    

    #region Directions & Positions
    public Vector2 FireToMouseDirection()
    {
        var direction = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        return direction;
    }
    public Vector2 FireToPlayerDirection()
    {
        var direction = transformToFollow.position - transform.position;
        return direction;
    }
    public Vector2 MousePosition()
    {
        return Input.mousePosition;
    }
    public Vector2 PlayerToMouseDirection(){
        var direction = (Input.mousePosition - cam.WorldToScreenPoint(transformToFollow.position)).normalized;
        return direction;
    }

    #endregion

    #region LinearVelocity
    public void LinearVelocity(float x, float y) // it makes the tip of the flame move when the player moves
    {
        var velocity = fire.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;

        AnimationCurve curve_x = new AnimationCurve();
        AnimationCurve curve_y = new AnimationCurve();
        AnimationCurve curve_z = new AnimationCurve();

        curve_x.AddKey(0f,0f);
        curve_x.AddKey(0.3f, x);

        curve_y.AddKey(0f,0f);
        curve_y.AddKey(0.3f, y);

        velocity.x = new ParticleSystem.MinMaxCurve(1f, curve_x);
        velocity.y = new ParticleSystem.MinMaxCurve(1f, curve_y);
        velocity.z = new ParticleSystem.MinMaxCurve(0f, curve_z); // without this you get an error
    }
    public void LinearVelocity(Vector2 vector2) // it makes the tip of the flame move when the player moves
    {
        var velocity = fire.velocityOverLifetime;
        velocity.enabled = true;
        velocity.space = ParticleSystemSimulationSpace.Local;

        AnimationCurve curve_x = new AnimationCurve();
        AnimationCurve curve_y = new AnimationCurve();
        AnimationCurve curve_z = new AnimationCurve();

        curve_x.AddKey(0f, 0f);
        curve_x.AddKey(0.3f, vector2.x);

        curve_y.AddKey(0f, 0f);
        curve_y.AddKey(0.3f, vector2.y);

        velocity.x = new ParticleSystem.MinMaxCurve(1f, curve_x);
        velocity.y = new ParticleSystem.MinMaxCurve(1f, curve_y);
        velocity.z = new ParticleSystem.MinMaxCurve(0f, curve_z); // without this you get an error
    }
    #endregion

    #region Color
    public void ChangeColor(float attackPower)
    {
        fire = GetComponentInChildren<ParticleSystem>();

    }

    #endregion

    #region Velocity
    public virtual void PassVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput).normalized * movementSpeed;
        LinearVelocity(-rb.velocity/2.5f);
    }
    public virtual void PassVelocity(Vector2 vector2)
    {
        rb.velocity = vector2.normalized * movementSpeed;
        LinearVelocity(-rb.velocity/2.5f);
    }

    public virtual void PassDashVelocity(float _xInput, float _yInput)
    {
        float currentSpeed = rb.velocity.magnitude;
        float newDashSpeed = Mathf.Lerp(currentSpeed, dashSpeed, DashVelocitySmoothness);
        rb.velocity = new Vector2(_xInput, _yInput).normalized * newDashSpeed;
        LinearVelocity(-rb.velocity/1.75f);
    }

    public virtual void PassDashVelocity(Vector2 vector2)
    {
        float currentSpeed = rb.velocity.magnitude;
        float newDashSpeed = Mathf.Lerp(currentSpeed, dashSpeed, DashVelocitySmoothness);
        rb.velocity = vector2.normalized * newDashSpeed;
        LinearVelocity(-rb.velocity/1.75f);
    }
    #endregion

    #region Gizmos

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion
}


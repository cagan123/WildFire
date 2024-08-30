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
    public float followDistance = 1f;

    [Header("Attack Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    public float prepDuration1;
    
    #region Components
    [HideInInspector] public Transform transformToFollow{get;private set;}
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    [HideInInspector]public Camera cam;
    public CharacterStats stats{ get; private set; }
    #endregion

    #region Variables
    public float distanceBetweenPlayerandFireSpirit { get; private set; }
    
    #endregion

    #region States
    public FireSpiritStateMachine stateMachine { get; private set; }
    public FollowState followState { get; private set; }
    public FireSpiritPrep1State prep1State { get; private set; }
    public FireSpiritAttack1State attack1State { get; private set; }
    public FireSpiritReco1State reco1State { get; private set; }
    public FireSpiritPrepBackState prepBackState { get; private set; }
    public FireSpiritAttackBackState attackbackState {get ; private set; }
    public FireSpiritRecoBackState recoBackState { get ; private set; }
    public FollowBehaviorState followBehaviorState { get; private set; }

    #endregion
   
    [HideInInspector]public ParticleSystem fire;

    private void Awake()
    {
        stateMachine = new FireSpiritStateMachine();
        followState = new FollowState(this, stateMachine, null);
        followBehaviorState = new FollowBehaviorState(this, stateMachine, null);

        prep1State = new FireSpiritPrep1State(this, stateMachine, "prep1");
        attack1State = new FireSpiritAttack1State(this, stateMachine, "attack1");
        reco1State = new FireSpiritReco1State(this, stateMachine, "reco1");

        prepBackState = new FireSpiritPrepBackState(this, stateMachine, "prep2");
        attackbackState = new FireSpiritAttackBackState(this, stateMachine, "attack2");
        recoBackState = new FireSpiritRecoBackState(this, stateMachine, "reco2");


        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();       
        fire = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
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
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

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
    public Vector2 PointBeweenPlayerandMouse(){
        Vector3 direction = PlayerToMouseDirection();        
        Vector3 targetPosition = transformToFollow.position + direction * followDistance;
        return targetPosition;  
    }
    public Vector2 DirectionToPointBeweenPlayerandMouse(){
        var direction = (PointBeweenPlayerandMouse() - (Vector2)transform.position).normalized;
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


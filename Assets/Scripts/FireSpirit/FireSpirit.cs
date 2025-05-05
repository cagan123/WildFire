using System.Collections;
using System.Collections.Generic;
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
    public int burstCount = 60; // Number of particles to emit in the burst
    public float burstDuration = 0.3f; // Duration for the burst to appear
    public float intensityScale = 1.5f; // How much larger/intense the burst appears
    public float minBurstParticleLifetime = 1f; // Minimum lifetime for burst particles
    public float maxBurstParticleLifetime = 2f; // Maximum lifetime for burst particles
    public float minBurstParticleSpeed = 8f; // Minimum velocity for burst particles
    public float maxBurstParticleSpeed = 11f; // Maximum velocity for burst particles
    public int staminaUse;
    public int attackCounter;
    public bool damageSourceActive;
    public SpellData leftClickSpell;
    public SpellData rightClickSpell;
    public SpellData dashSpell;
    public SpellData eSpell;
    public SpellData qSpell;


    #region Components
    [HideInInspector] public Transform transformToFollow{get;private set;}
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    [HideInInspector]public Camera cam;
    public CharacterStats stats{ get; private set; }
    public damageSource damageSource { get; private set; }
    #endregion

    #region Variables
    public float distanceBetweenPlayerandFireSpirit { get; private set; }
    
    #endregion

    #region States
    public FireSpiritStateMachine stateMachine { get; private set; }
    public FollowState followState { get; private set; }
    public FollowBehaviorState followBehaviorState { get; private set; }
    public FireballState fireballState { get; private set; }
    public DamageState damageState { get; private set; }
    public NewAttackState AttackState { get; private set; }

    #endregion
   
    [HideInInspector]public ParticleSystem fire;

    private void Awake()
    {
        stateMachine = new FireSpiritStateMachine();
        followState = new FollowState(this, stateMachine, null);
        followBehaviorState = new FollowBehaviorState(this, stateMachine, null);

        damageState = new DamageState(this, stateMachine, null);
        fireballState = new FireballState(this, stateMachine, null);

        AttackState = new NewAttackState(this, stateMachine, null);

        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();       
        fire = GetComponentInChildren<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
        damageSource = GetComponentInChildren<damageSource>();
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
        GetMouseInput();
        GetInputFromInventory();
        distanceBetweenPlayerandFireSpirit = Vector2.Distance(transform.position, transformToFollow.position);
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
   
    #region Inputs and Buffer
        public void GetMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CastSpell(leftClickSpell);
            }
            if (Input.GetMouseButtonDown(1))
            {
                CastSpell(rightClickSpell);
            }
        }
    #endregion

    #region Directions & Positions
    public Vector3 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }
    public Vector2 FireToMouseDirection()
    {
        var direction = (MousePosition() - transform.position).normalized;
        return direction;
    }
    public Vector2 FireToPlayerDirection()
    {
        var direction = (transformToFollow.position - transform.position).normalized;
        return direction;
    }
    public Vector2 PlayerToMouseDirection() => PlayerManager.instance.player.PlayerToMouseDirection();
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

    #region Get Input from Inventory
    public void GetInputFromInventory()
    {
        leftClickSpell = Inventory.instance.ReturnLeftClickSpell();
        rightClickSpell = Inventory.instance.ReturnRightClickSpell();
        dashSpell = Inventory.instance.ReturnDashSpell();
        eSpell = Inventory.instance.ReturnESpell();
        qSpell = Inventory.instance.ReturnQSpell();
    }
    #endregion

    #region GetKeyInput
    public void GetKeyInput(){
        if(Input.GetMouseButtonDown(1))
        {
            if (rightClickSpell != null)
            {
                CastSpell(rightClickSpell);
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if (leftClickSpell != null)
            {
                CastSpell(leftClickSpell);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (qSpell != null)
            {
                CastSpell(qSpell);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (eSpell != null)
            {
                CastSpell(eSpell);
            }
        }
        
    }
        
    #endregion
    
    #region Instantiate Spell
    private bool CastSpell(SpellData spell){
        if (spell.spellPrefab!= null)
        {
            
            Instantiate(spell.spellPrefab, PointBeweenPlayerandMouse(), Quaternion.identity);
            return true; // Successfully cast spell
        }
        Debug.Log(spell.name + " is not assigned to the FireSpirit");
        return false; // Spell cast failed
    
    }
    #endregion
    
    #region Burst
    public void TriggerBurst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            // Create custom parameters for each particle
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                startLifetime = Random.Range(minBurstParticleLifetime, maxBurstParticleLifetime), // Random lifetime
            };

            // Randomize the speed of each particle
            float randomSpeed = Random.Range(minBurstParticleSpeed, maxBurstParticleSpeed);

            // Apply the velocity in the direction of the mouse with slight randomness
            emitParams.velocity = PlayerToMouseDirection() * randomSpeed + new Vector2(
                Random.Range(-1f, 1f), // Add slight X randomness
                Random.Range(-1f, 1f) // Add slight Y randomness
            );

            // Emit a single particle with the custom parameters
            fire.Emit(emitParams, 1);
        }

        // Optionally scale the particle system for a brief effect
        StartCoroutine(ScaleFireTemporarily());
    }
    
    public IEnumerator ScaleFireTemporarily()
    {
        // Store the original size of the particle system
        Vector3 originalScale = new Vector3(1, 1, 1);

        // Scale up the particle system
        transform.localScale = originalScale * intensityScale;

        // Wait for the burst duration
        yield return new WaitForSeconds(burstDuration);

        // Reset to the original size
        transform.localScale = originalScale;
    }
    #endregion
}


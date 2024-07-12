using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] float movementSpeed;
    [SerializeField] public float dashSpeed;
    [SerializeField] public float stealthSpeed;
    [HideInInspector] public bool isInStealth;

    [Header("Collision Info")]
    
    [SerializeField] protected LayerMask ObstacleLayer;

    [Header("Knockback Info")]
    [SerializeField] protected float knockbackForce;
    [SerializeField] private float knockbackTime = 1f;
    [HideInInspector]protected bool isKnocked;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public AnimationController animController { get; private set; }
    public EntityVFX VFX { get; private set; }
    public CharacterStats stats{ get; private set; }

    #endregion

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponentInChildren<AnimationController>();
        VFX = GetComponent<EntityVFX>();
        stats = GetComponent<CharacterStats>();
    }

    public virtual void DamageEffect()
    {
        VFX.StartCoroutine("FlashVFX_Routine");
    }
    public void GetKnockedBacked(Transform damageSource, float knockbackForce)
    {
        isKnocked = true;
        Vector2 difference = (transform.position-damageSource.position).normalized * knockbackForce * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity= Vector2.zero;
        isKnocked= false;
    }

    #region Flip Control

    public virtual void ControlFlip()
    {
        animController.CalculateLookAtDirection();
    }
    #endregion


    #region Velocity
    public virtual void PassVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput).normalized * movementSpeed;
    }
    public virtual void PassVelocity(Vector2 vector2)
    {
        rb.velocity = vector2.normalized * movementSpeed;
    }

    public virtual void PassDashVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput).normalized * dashSpeed;
    }

    public virtual void PassDashVelocity(Vector2 vector2)
    {
        rb.velocity = vector2.normalized * dashSpeed;
    }

    public virtual void PassStealthVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput).normalized * stealthSpeed;
    }

    public virtual void PassStealthVelocity(Vector2 vector2)
    {
        rb.velocity = vector2.normalized * stealthSpeed;
    }
    #endregion

}

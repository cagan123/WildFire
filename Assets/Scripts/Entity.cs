using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] float movementSpeed;
    [SerializeField] public float dashSpeed;
    [SerializeField] public float runSpeed;
    [HideInInspector]public bool facingLeft = true;
    [HideInInspector]public bool facingUp = true;
    [HideInInspector]public int facingDirection = 1;

    [Header("Collision Info")]
    
    [SerializeField] protected LayerMask ObstacleLayer;

    [Header("Knockback Info")]
    [SerializeField] protected float knockbackForce;
    public Vector2 knockbackDir;
    [SerializeField] private float knockbackTime = 1f;
    [HideInInspector]protected bool isKnocked;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
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
        VFX = GetComponent<EntityVFX>();
        stats = GetComponent<CharacterStats>();
    }

    public virtual void DamageEffect()
    {
        VFX.StartCoroutine("FlashVFX_Routine");
        StartCoroutine("KnockBackRoutine");
    }
    protected virtual IEnumerator KnockBackRoutine()
    {
        isKnocked = true;
        rb.velocity = knockbackDir * knockbackForce;
        yield return new WaitForSeconds(knockbackTime);
        isKnocked= false;
    }

    #region Velocity
    public virtual void PassVelocity(float _xInput, float _yInput)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = new Vector2(_xInput, _yInput).normalized * movementSpeed;
    }
    public virtual void PassVelocity(Vector2 vector2)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = vector2.normalized * movementSpeed;
    }

    public virtual void PassDashVelocity(float _xInput, float _yInput)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = new Vector2(_xInput, _yInput).normalized * dashSpeed;
    }

    public virtual void PassDashVelocity(Vector2 vector2)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = vector2.normalized * dashSpeed;
    }

    public virtual void PassRunVelocity(float _xInput, float _yInput)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = new Vector2(_xInput, _yInput).normalized * runSpeed;
    }

    public virtual void PassRunVelocity(Vector2 vector2)
    {
        if(isKnocked){
            return;
        }
        rb.velocity = vector2.normalized * runSpeed;
    }
    #endregion
    public virtual void Die(){
        
    }
}

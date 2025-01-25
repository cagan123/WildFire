using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FireSpiritState 
{
    #region Components
    protected FireSpiritStateMachine stateMachine;
    protected FireSpirit fireSpirit;
    public string animBoolName {get; private set;}
    protected Rigidbody2D rb;
    //protected damageSource damageSource;
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


    public FireSpiritState(FireSpirit _fireSpirit, FireSpiritStateMachine _stateMachine, string _animBoolName)
    {
        this.fireSpirit = _fireSpirit;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;

    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = fireSpirit.rb;
        //damageSource = fireSpirit.damageSource; 
        Distance = fireSpirit.distanceBetweenPlayerandFireSpirit;
        if(animBoolName != null){
            fireSpirit.fire.Stop();
            fireSpirit.anim.SetBool("empty", false);         
            fireSpirit.anim.SetBool(animBoolName, true);          
        }
        else{
            fireSpirit.anim.SetBool("empty", true);       
            fireSpirit.fire.Play();
        }
        
    }

    public virtual void Update()
    {   
        PlayerPosition = fireSpirit.cam.WorldToScreenPoint(fireSpirit.transformToFollow.position);
        RealPos = fireSpirit.cam.ScreenToWorldPoint(Position);
        RotationHandler();    
    }

    public virtual void FixedUpdate()
    {
        stateTimer -= Time.fixedDeltaTime; // this is used by other states
        CurrentVelocity = rb.velocity;
        Position = fireSpirit.cam.WorldToScreenPoint(rb.position);
    }
    public virtual void Exit()
    {      
        
        if(animBoolName != null){
            fireSpirit.anim.SetBool(animBoolName, false); 
        }
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    #region Rotation Managers
    public void RotationHandler(){
        float angle = Vector2.SignedAngle(Vector2.down, fireSpirit.FireToPlayerDirection());
        Quaternion newRotation = new Quaternion {
            eulerAngles = new Vector3(0, 0, angle)
        };  
        if(animBoolName == null){
            RotationResetter();
        }
        else{
            fireSpirit.transform.rotation = newRotation;
        }
        
    }
    public void RotationResetter(){
        
        Quaternion newRotation = new Quaternion { 
        eulerAngles = Vector3.zero
        };
        if(fireSpirit.transform.rotation == newRotation){
            return;
        }
        else{
            fireSpirit.transform.rotation  = newRotation;
        }
        
    }
    #endregion
}

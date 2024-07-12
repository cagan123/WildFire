using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    protected Vector2 movementInput;
    protected bool thereIsMovementInput = false;

    private string animBoolName;

    protected float stateTimer;

    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // this is used by other states

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(xInput, yInput);

        if (Mathf.Approximately(movementInput.x, 0) && Mathf.Approximately(movementInput.y, 0))
        {
            thereIsMovementInput = false;
        }
        else
        {
            thereIsMovementInput = true;
        }
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

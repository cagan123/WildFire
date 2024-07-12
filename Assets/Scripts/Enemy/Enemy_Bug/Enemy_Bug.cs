using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_Bug_namespace
{
    public class Enemy_Bug : Enemy
    {
        #region States
        public IdleState idleState { get; private set; }
        public ChaseState chaseState { get; private set; }
        public AttackState attackState { get; private set; }
        #endregion

        protected override void Awake()
        {
            base.Awake();
            idleState = new IdleState(this, stateMachine, "idle", this);
            chaseState = new ChaseState(this, stateMachine, "wonder", this);
            attackState = new AttackState(this, stateMachine, "attack", this);
        }

        protected override void Start()
        {
            base.Start();

            stateMachine.Initialize(idleState);
        }

        protected void FixedUpdate()
        {
            stateMachine.currentState.FixedUpdate();
        }

        protected void Update()
        {           
            stateMachine.currentState.Update();
        }
        
    }
}

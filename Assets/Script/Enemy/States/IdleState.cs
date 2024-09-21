using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] protected D_IdleState stateData;
    [SerializeField] protected float idleTime;
    [SerializeField] protected bool flipAfterIdle;
    [SerializeField] protected bool isIdleTimeOver;
    [SerializeField] protected bool isPlayerInMinAgroRange;


    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        this.entity.SetVelocity(0.0f);
        this.isIdleTimeOver = false;
       
        this.SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
        if (this.flipAfterIdle)
        {
            this.entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= this.startTime + this.idleTime)
        {
            this.isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        this.flipAfterIdle = flip;
    }
    protected void SetRandomIdleTime() 
    {
        this.idleTime = Random.Range(this.stateData.minIdleTime, this.stateData.maxIdleTime);
    }

}

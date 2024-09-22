using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.isGrounded = this.entity.CheckGround();
        this.performCloseRangeAction = this.entity.CheckPlayerInCloseRangeAction();
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        this.isStunTimeOver = false;
        this.isMovementStopped = false;
        this.entity.SetVelocity(this.stateData.stunKnockbackSpeed, this.stateData.stunKnocbackAngle, this.entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        this.entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= this.startTime + this.stateData.stunTime)
        {
            this.isStunTimeOver = true;
        }
        if(isGrounded && Time.time >= this.startTime + this.stateData.stunKnockbackTime && !this.isMovementStopped )
        {
            this.isMovementStopped = true;
            this.entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

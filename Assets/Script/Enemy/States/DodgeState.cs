using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        this.performCloseRangeAction = this.entity.CheckPlayerInCloseRangeAction();
        this.isPlayerInMaxAgroRange = this.entity.CheckPlayerInMaxAgroRange();
        this.isGrounded = this.entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        this.isDodgeOver = false;
        this.entity.SetVelocity(this.stateData.dodgeSpeed, this.stateData.dodgeAngle, -this.entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= this.startTime + this.stateData.dodgeTime && this.isGrounded)
        {
            this.isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

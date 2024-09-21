using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        this.entity.SetVelocity(0f);
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
        this.isPlayerInMaxAgroRange = this.entity.CheckPlayerInMaxAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
        this.isPlayerInMaxAgroRange = this.entity.CheckPlayerInMaxAgroRange();
    }
}

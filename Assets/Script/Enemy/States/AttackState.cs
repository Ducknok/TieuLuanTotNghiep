using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        this.entity.atsm.attackState = this;
        this.isAnimationFinished = false;
        this.entity.SetVelocity(0f);
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
    }
    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        this.isAnimationFinished = true;
    }
}

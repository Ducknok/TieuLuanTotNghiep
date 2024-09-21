using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] protected D_MoveState stateData;

    [SerializeField] protected bool isDetectingWall;
    [SerializeField] protected bool isDetectingLedge;
    [SerializeField] protected bool isPlayerInMinAgroRange;
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        this.entity.SetVelocity(this.stateData.movementSpeed);

        this.isDetectingLedge = entity.CheckLedge();
        this.isDetectingWall = entity.CheckWall();
        this.isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
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
        this.isDetectingLedge = this.entity.CheckLedge();
        this.isDetectingWall = this.entity.CheckWall();
        this.isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}

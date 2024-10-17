using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_MoveState : MoveState
{
    private WarriorSlayer wSlayer;
    public WSlayer_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wSlayer = wSlayer;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (this.isPlayerInMinAgroRange)
        {
            this.stateMachine.ChangeState(this.wSlayer.playerDetectedState);
        }

        else if (this.isDetectingWall || !this.isDetectingLedge)
        {
            this.wSlayer.idleState.SetFlipAfterIdle(true);
            this.stateMachine.ChangeState(wSlayer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

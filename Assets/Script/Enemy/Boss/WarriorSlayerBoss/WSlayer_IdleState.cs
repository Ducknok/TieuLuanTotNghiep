using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_IdleState : IdleState
{
    private WarriorSlayer wSlayer;

    public WSlayer_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
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

        else if (this.isIdleTimeOver)
        {
            this.stateMachine.ChangeState(this.wSlayer.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

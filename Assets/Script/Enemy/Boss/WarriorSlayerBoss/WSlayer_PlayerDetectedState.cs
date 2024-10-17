using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_PlayerDetectedState : PlayerDetectedState
{
    private WarriorSlayer wSlayer;
    public WSlayer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
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
        if (this.performCloseRangeAction)
        {
            this.stateMachine.ChangeState(this.wSlayer.meleeAttackState);
        }
        else if (this.performLongRangeAction)
        {
            this.stateMachine.ChangeState(this.wSlayer.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            this.stateMachine.ChangeState(this.wSlayer.lookForPlayerState);
        }
        else if (!this.isDetectingLedge)
        {
            this.entity.Flip();
            this.stateMachine.ChangeState(this.wSlayer.moveState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

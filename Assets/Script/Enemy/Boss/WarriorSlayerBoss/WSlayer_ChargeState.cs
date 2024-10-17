using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_ChargeState : ChargeState
{
    private WarriorSlayer wSlayer;
    public WSlayer_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (!isDetectingLedge || isDetectingWall)
        {

            this.stateMachine.ChangeState(this.wSlayer.lookForPlayerState);
        }
        else if (this.isChargeTimeOver)
        {

            if (isPlayerInMinAgroRange)
            {
                this.stateMachine.ChangeState(this.wSlayer.playerDetectedState);
            }
            else
            {
                this.stateMachine.ChangeState(this.wSlayer.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

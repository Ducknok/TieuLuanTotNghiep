using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_ChargeState : ChargeState
{
    private Enemy3 enemy;
    public E3_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
            this.stateMachine.ChangeState(this.enemy.meleeAttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
        {

            this.stateMachine.ChangeState(this.enemy.lookForPlayerState);
        }
        else if (this.isChargeTimeOver)
        {

            if (isPlayerInMinAgroRange)
            {
                this.stateMachine.ChangeState(this.enemy.playerDetectedState);
            }
            else
            {
                this.stateMachine.ChangeState(this.enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

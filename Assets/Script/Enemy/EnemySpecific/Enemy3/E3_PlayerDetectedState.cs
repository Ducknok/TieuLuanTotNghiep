using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_PlayerDetectedState : PlayerDetectedState
{
    private Enemy3 enemy;
    public E3_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (this.performLongRangeAction)
        {
            this.stateMachine.ChangeState(this.enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            this.stateMachine.ChangeState(this.enemy.lookForPlayerState);
        }
        else if (!this.isDetectingLedge)
        {
            this.entity.Flip();
            this.stateMachine.ChangeState(this.enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

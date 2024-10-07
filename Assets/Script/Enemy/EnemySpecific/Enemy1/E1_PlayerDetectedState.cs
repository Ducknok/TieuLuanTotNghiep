using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

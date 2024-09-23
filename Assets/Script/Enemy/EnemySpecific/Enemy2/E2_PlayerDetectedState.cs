using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy;
    public E2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if (!this.isPlayerInMaxAgroRange)
        {
            this.stateMachine.ChangeState(this.enemy.lookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

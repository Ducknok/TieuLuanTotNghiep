using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_LookForPlayerState : LookForPlayerState
{
    private Enemy3 enemy;
    public E3_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (this.isPlayerInMinAgroRange)
        {
            this.stateMachine.ChangeState(this.enemy.playerDetectedState);
        }
        else if (this.isAllTurnsTimeDone)
        {
            this.stateMachine.ChangeState(this.enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

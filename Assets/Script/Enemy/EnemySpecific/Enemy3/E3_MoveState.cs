using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    private Enemy3 enemy;
    public E3_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        else if(!this.isDetectingLedge || this.isDetectingWall)
        {
            this.enemy.idleState.SetFlipAfterIdle(true);
            this.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

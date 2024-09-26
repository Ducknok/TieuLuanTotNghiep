using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_IdleState : IdleState
{
    private Enemy3 enemy;
    public E3_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        //if (this.isPlayerInMinAgroRange)
        //{
        //    this.stateMachine.ChangeState();
        //}
        if (this.isIdleTimeOver)
        {
            this.stateMachine.ChangeState(this.enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangeAttackState : ArrowRangeAttackState
{
    private Enemy2 enemy;
    public E2_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_ArrowRangeAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (this.isPlayerInMinAgroRange)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_MeleeAttackState : MeleeAttackState
{
    private WarriorSlayer wSlayer;
    public WSlayer_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (this.isAnimationFinished)
        {
            if (this.isPlayerInMinAgroRange)
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

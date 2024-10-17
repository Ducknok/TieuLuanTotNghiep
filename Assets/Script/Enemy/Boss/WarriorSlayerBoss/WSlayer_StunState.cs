using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_StunState : StunState
{
    private WarriorSlayer wSlayer;
    public WSlayer_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
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
        if (this.isStunTimeOver)
        {
            if (this.performCloseRangeAction)
            {
                this.stateMachine.ChangeState(this.wSlayer.meleeAttackState);
            }
            else if (this.isPlayerInMinAgroRange)
            {
                this.stateMachine.ChangeState(this.wSlayer.chargeState);
            }
            else
            {
                this.wSlayer.lookForPlayerState.SetTurnImediately(true);
                this.stateMachine.ChangeState(this.wSlayer.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

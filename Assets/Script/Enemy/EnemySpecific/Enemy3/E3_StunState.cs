using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_StunState : StunState
{
    private Enemy3 enemy;
    public E3_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (this.isStunTimeOver)
        {
            if (this.performCloseRangeAction)
            {
                this.stateMachine.ChangeState(this.enemy.meleeAttackState);
            }
            else if (this.isPlayerInMinAgroRange)
            {
                this.stateMachine.ChangeState(this.enemy.chargeState);
            }
            else
            {
                this.enemy.lookForPlayerState.SetTurnImediately(true);
                this.stateMachine.ChangeState(this.enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

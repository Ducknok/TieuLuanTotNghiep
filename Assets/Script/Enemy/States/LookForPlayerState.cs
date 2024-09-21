using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayer stateData;
    protected float lastTurnTime;
    protected int amountOfTurnsDone;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;
    protected bool turnImediately;

    
    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        this.isPlayerInMinAgroRange = this.entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        this.isAllTurnsDone = false;
        this.isAllTurnsTimeDone = false;
        this.lastTurnTime = this.startTime;
        this.amountOfTurnsDone = 0;

        this.entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (this.turnImediately)
        {
            this.entity.Flip();
            this.lastTurnTime = Time.time;
            this.amountOfTurnsDone++;
            this.turnImediately = false;
        }
        else if (Time.time >= this.lastTurnTime + this.stateData.timeBetweenTurns && !this.isAllTurnsDone)
        {
            this.entity.Flip();
            this.lastTurnTime = Time.time;
            this.amountOfTurnsDone++;
        }
        if(this.amountOfTurnsDone >= this.stateData.amountOfTurns)
        {
            this.isAllTurnsDone = true;
        }
        if (Time.time >= this.lastTurnTime + this.stateData.timeBetweenTurns && isAllTurnsDone)
        {
            this.isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetTurnImediately(bool flip)
    {
        this.turnImediately = flip;
    }
}

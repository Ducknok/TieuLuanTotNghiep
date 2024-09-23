using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    [SerializeField] protected FiniteStateMachine stateMachine;
    [SerializeField] protected Entity entity;

    [SerializeField] public float startTime { get; protected set; }

    [SerializeField] protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        this.startTime = Time.time;
        this.entity.anim.SetBool(animBoolName, true);
        this.DoChecks();
    }
    public virtual void Exit()
    {
        this.entity.anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        this.DoChecks();
    }
    public virtual void DoChecks()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();
        this.moveState = new E1_MoveState(this, this.stateMachine, "move", this.moveStateData, this);
        this.idleState = new E1_IdleState(this, this.stateMachine, "idle", this.idleStateData, this);
        this.stateMachine.Initialize(this.moveState);
    }
}

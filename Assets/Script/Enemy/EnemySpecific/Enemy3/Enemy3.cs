using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_MoveState moveState { get; private set; }
    public E3_IdleState idleState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_ChargeState chargeState { get; private set; }
    public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    public E3_StunState stunState { get; private set; }
    public E3_DeadState deadState { get; private set; }

    public E2_RangeAttackState rangeAttackState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        this.moveState = new E3_MoveState(this, this.stateMachine, "move", this.moveStateData, this);
        this.idleState = new E3_IdleState(this, this.stateMachine, "idle", this.idleStateData, this);
        this.playerDetectedState = new E3_PlayerDetectedState(this, this.stateMachine, "playerDetected", this.playerDetectedStateData, this);
        this.chargeState = new E3_ChargeState(this, this.stateMachine, "charge", this.chargeStateData, this);
        this.meleeAttackState = new E3_MeleeAttackState(this, this.stateMachine, "meleeAttack", this.meleeAttackPosition, this.meleeAttackStateData, this);
        this.lookForPlayerState = new E3_LookForPlayerState(this, this.stateMachine, "lookForPlayer", this.lookForPlayerStateData, this);
        this.stunState = new E3_StunState(this, this.stateMachine, "stun", this.stunStateData, this);
        this.deadState = new E3_DeadState(this, this.stateMachine, "dead", this.deadStateData, this);
        

        this.stateMachine.Initialize(this.moveState);
    }
    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
        if (this.isDead)
        {
            this.stateMachine.ChangeState(deadState);
        }
        if (this.isStunned && this.stateMachine.currentState != this.stunState)
        {
            this.stateMachine.ChangeState(this.stunState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(this.meleeAttackPosition.position, this.meleeAttackStateData.attackRadius);
    }
}

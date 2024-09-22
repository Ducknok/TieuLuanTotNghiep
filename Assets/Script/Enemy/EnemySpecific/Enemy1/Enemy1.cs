using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;
    [SerializeField] protected D_PlayerDetected playerDetectedData;
    [SerializeField] protected D_ChargeState chargeStateData;
    [SerializeField] protected D_LookForPlayer lookForPlayerData;
    [SerializeField] protected D_MeleeAttack meleeAttackStateData;
    [SerializeField] protected D_StunState stunStateData;
    [SerializeField] protected D_DeadState deadStateData;

    [SerializeField] protected Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        this.moveState = new E1_MoveState(this, this.stateMachine, "move", this.moveStateData, this);
        this.idleState = new E1_IdleState(this, this.stateMachine, "idle", this.idleStateData, this);
        this.playerDetectedState = new E1_PlayerDetectedState(this, this.stateMachine, "playerDetected", this.playerDetectedData, this);
        this.chargeState = new E1_ChargeState(this, this.stateMachine, "charge", this.chargeStateData, this);
        this.lookForPlayerState = new E1_LookForPlayerState(this, this.stateMachine, "lookForPlayer", this.lookForPlayerData, this);
        this.meleeAttackState = new E1_MeleeAttackState(this, this.stateMachine, "meleeAttack", this.meleeAttackPosition, this.meleeAttackStateData, this);
        this.stunState = new E1_StunState(this, this.stateMachine, "stun", this.stunStateData, this);
        this.deadState = new E1_DeadState(this, this.stateMachine, "dead", this.deadStateData, this);
        this.stateMachine.Initialize(this.moveState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(this.meleeAttackPosition.position, this.meleeAttackStateData.attackRadius);
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
}

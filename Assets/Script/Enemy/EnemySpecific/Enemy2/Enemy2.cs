using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    [SerializeField] private Transform meleeAttackPosition;
    

    public override void Start()
    {
        base.Start();
        this.moveState = new E2_MoveState(this, this.stateMachine, "move", this.moveStateData, this);
        this.idleState = new E2_IdleState(this, this.stateMachine, "idle", this.idleStateData, this);
        this.playerDetectedState = new E2_PlayerDetectedState(this, this.stateMachine, "playerDetected", this.playerDetectedStateData, this);
        this.meleeAttackState = new E2_MeleeAttackState(this, this.stateMachine, "meleeAttack", this.meleeAttackPosition, this.meleeAttackStateData, this);
        this.lookForPlayerState = new E2_LookForPlayerState(this, this.stateMachine, "lookForPlayer", this.lookForPlayerStateData, this);
        this.stunState = new E2_StunState(this, this.stateMachine, "stun", this.stunStateData, this);
        this.deadState = new E2_DeadState(this, this.stateMachine, "dead", this.deadStateData, this);

        this.stateMachine.Initialize(this.moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (this.isDead)
        {
            this.stateMachine.ChangeState(this.deadState);
        }
        else if(this.isStunned && this.stateMachine.currentState != this.stunState)
        {
            this.stateMachine.ChangeState(stunState);
        }
        else if (!this.CheckPlayerInMinAgroRange())
        {
            this.lookForPlayerState.SetTurnImediately(true);
            this.stateMachine.ChangeState(this.lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(this.meleeAttackPosition.position, this.meleeAttackStateData.attackRadius);
    }
}

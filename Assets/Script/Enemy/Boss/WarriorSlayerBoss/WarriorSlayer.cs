using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarriorSlayerState
{
    Idle,
    Walk,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    ShieldBlock,
    Jump,
    Dive,

    Death,
}
public class WarriorSlayer : Entity
{
    public WSlayer_IdleState idleState { get; private set; }
    public WSlayer_MoveState moveState { get; private set; }
    public WSlayer_PlayerDetectedState playerDetectedState { get; private set; }
    public WSlayer_ChargeState chargeState { get; private set; }
    public WSlayer_LookForPlayerState lookForPlayerState { get; private set; }
    public WSlayer_MeleeAttackState meleeAttackState { get; private set; }
    public WSlayer_StunState stunState { get; private set; }
    public WSlayer_DeadState deadState { get; private set; }
    [SerializeField] protected D_IdleState idleStateData;
    [SerializeField] protected D_MoveState moveStateData;
    [SerializeField] protected D_PlayerDetected playerDetectedData;
    [SerializeField] protected D_ChargeState chargeStateData;
    [SerializeField] protected D_LookForPlayer lookForPlayerData;
    [SerializeField] protected D_MeleeAttack meleeAttackStateData;
    [SerializeField] protected D_StunState stunStateData;
    [SerializeField] protected D_DeadState deadStateData;
    [SerializeField] protected Transform meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();
        this.moveState = new WSlayer_MoveState(this, this.stateMachine, "move", this.moveStateData, this);
        this.idleState = new WSlayer_IdleState(this, this.stateMachine, "idle", this.idleStateData, this);
        this.playerDetectedState = new WSlayer_PlayerDetectedState(this, this.stateMachine, "playerDetected", this.playerDetectedData, this);
        this.chargeState = new WSlayer_ChargeState(this, this.stateMachine, "charge", this.chargeStateData, this);
        this.lookForPlayerState = new WSlayer_LookForPlayerState(this, this.stateMachine, "lookForPlayer", this.lookForPlayerData, this);
        this.meleeAttackState = new WSlayer_MeleeAttackState(this, this.stateMachine, "meleeAttack", this.meleeAttackPosition, this.meleeAttackStateData, this);
        this.stunState = new WSlayer_StunState(this, this.stateMachine, "stun", this.stunStateData, this);
        this.deadState = new WSlayer_DeadState(this, this.stateMachine, "dead", this.deadStateData, this);

    }
    protected virtual void Start()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{

    protected D_MeleeAttack stateData;
    protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        this.attackDetails.damageAmount = this.stateData.attackDamage;
        this.attackDetails.position = this.entity.aliveGo.transform.position;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(this.attackPosition.position, this.stateData.attackRadius, this.stateData.whatIsPlayer);
        bool isCritical = Random.Range(0, 100) < 30;
        if (isCritical) this.attackDetails.damageAmount *= 2;
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", this.attackDetails);
            this.stateData.instance.Create(this.attackPosition.position, this.attackDetails.damageAmount, isCritical);
        }
    }
}

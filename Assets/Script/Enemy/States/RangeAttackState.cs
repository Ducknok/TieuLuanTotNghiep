using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;
    protected GameObject projectile;
    protected ArrowProjectile projectileScript;
    [SerializeField] protected AnimationCurve projecttileMoveCurve;
    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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

        this.projectile = GameObject.Instantiate(stateData.projectile, this.attackPosition.position, this.attackPosition.rotation);
        this.projectileScript = projectile.GetComponent<ArrowProjectile>();
        this.projectileScript.FireProjectTile(this.stateData.projectileSpeed, this.stateData.projectileTravelDistance, this.stateData.projectileDamage);
    }
}

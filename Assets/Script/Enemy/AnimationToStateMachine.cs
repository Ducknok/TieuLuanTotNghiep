using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : DucMonobehavior
{
    public AttackState attackState;

    public override void TriggerAttack()
    {
        this.attackState.TriggerAttack();
    }
    public override void FinishAttack()
    {
        this.attackState.FinishAttack();
    }
}

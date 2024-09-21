using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;

    private void TriggerAttack()
    {
        this.attackState.TriggerAttack();
    }
    private void FinishAttack()
    {
        this.attackState.FinishAttack();
    }
}

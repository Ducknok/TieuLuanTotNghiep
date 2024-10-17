using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSlayer_DeadState : DeadState
{
    private WarriorSlayer wSlayer;
    public WSlayer_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, WarriorSlayer wSlayer) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.wSlayer = wSlayer;
    }
}

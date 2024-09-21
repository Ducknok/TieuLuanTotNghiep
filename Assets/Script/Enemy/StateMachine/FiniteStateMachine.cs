using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    [SerializeField] public State currentState { get; private set; }
    
    public void Initialize(State startingState)
    {
        this.currentState = startingState;
        this.currentState.Enter();
    }
    public void ChangeState(State newState)
    {
        this.currentState.Exit();
        this.currentState = newState;
        this.currentState.Enter();
    }
}

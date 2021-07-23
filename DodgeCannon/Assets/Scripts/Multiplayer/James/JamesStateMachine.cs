using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesStateMachine
{
    public JamesState CurrentState { get; private set; }

    public void Start(JamesState initialState)
    {
        CurrentState = initialState;
        CurrentState.OnEnter();
    }

    public void ChangeState(JamesState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyStateMachine
{
    public RemyState CurrentState { get; private set; }

    public void Start(RemyState initialState)
    {
        CurrentState = initialState;
        CurrentState.OnEnter();
    }

    public void ChangeState(RemyState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}

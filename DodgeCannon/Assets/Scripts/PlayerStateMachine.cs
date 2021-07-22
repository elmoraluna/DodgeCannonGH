using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Start(PlayerState initialState)
    {
        CurrentState = initialState;
        CurrentState.OnEnter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}

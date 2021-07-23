using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerController controller;
    protected PlayerStateMachine fsm;

    protected PlayerState(PlayerController controller, PlayerStateMachine fsm)
    {
        this.controller = controller;
        this.fsm = fsm;
    }

    public virtual void OnEnter() {}
    public virtual void OnExit() {}
    public virtual void OnHandleInput() {}
    public virtual void OnLogicUpdate() {}
    public virtual void OnPhysicsUpdate() {}
}

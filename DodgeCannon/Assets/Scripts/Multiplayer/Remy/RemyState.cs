using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RemyState
{
    protected RemyController controller;
    protected RemyStateMachine fsm;

    protected RemyState(RemyController controller, RemyStateMachine fsm)
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

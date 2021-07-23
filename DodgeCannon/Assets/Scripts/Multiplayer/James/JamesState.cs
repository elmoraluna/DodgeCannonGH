using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JamesState
{
    protected JamesController controller;
    protected JamesStateMachine fsm;

    protected JamesState(JamesController controller, JamesStateMachine fsm)
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

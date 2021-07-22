using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    /*protected RemyController remyController;
    protected JamesController jamesController;*/
    protected PlayerController controller;
    protected PlayerStateMachine fsm;

    /*protected PlayerState(RemyController remyController, PlayerStateMachine fsm)
    {
        this.remyController = remyController;
        this.fsm = fsm;
    }

    protected PlayerState(JamesController jamesController, PlayerStateMachine fsm)
    {
        this.jamesController = jamesController;
        this.fsm = fsm;
    }*/

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

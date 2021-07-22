using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    private Animator animator;

    public IdleState(PlayerController controller, PlayerStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            controller.Move();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            controller.Punch();
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetBool("isRunning", false);
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }
}


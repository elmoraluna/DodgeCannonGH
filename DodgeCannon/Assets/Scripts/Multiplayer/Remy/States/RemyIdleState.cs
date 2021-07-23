using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyIdleState : RemyState
{
    private Animator animator;

    public RemyIdleState(RemyController controller, RemyStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        if (Input.GetAxisRaw("p1_Horizontal") != 0 || Input.GetAxisRaw("p1_Vertical") != 0)
        {
            controller.Move();
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            controller.Punch();
        }*/
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

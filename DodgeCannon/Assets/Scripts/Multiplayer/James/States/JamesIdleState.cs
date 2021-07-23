using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesIdleState : JamesState
{
    private Animator animator;

    public JamesIdleState(JamesController controller, JamesStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        if (Input.GetAxisRaw("p2_Horizontal") != 0 || Input.GetAxisRaw("p2_Vertical") != 0)
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

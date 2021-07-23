using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesKnockedState : JamesState
{
    private Animator animator;
    private Rigidbody rb;
    public JamesKnockedState(JamesController controller, JamesStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
        rb = controller.GetComponent<Rigidbody>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetTrigger("isKnocked");
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
        rb.velocity = Vector3.zero;
    }
}

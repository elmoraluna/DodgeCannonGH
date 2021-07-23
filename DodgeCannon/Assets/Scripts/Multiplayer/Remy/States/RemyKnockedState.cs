using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyKnockedState : RemyState
{
    private Animator animator;
    private Rigidbody rb;
    public RemyKnockedState(RemyController controller, RemyStateMachine fsm) : base(controller, fsm)
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

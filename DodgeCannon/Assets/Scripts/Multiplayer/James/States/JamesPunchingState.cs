using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesPunchingState : JamesState
{
    private Animator animator;
    private Rigidbody rb;
    private GameObject punchPosition;
    private float knockedDuration;
    public JamesPunchingState(JamesController controller, JamesStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
        rb = controller.GetComponent<Rigidbody>();
        punchPosition = controller.punchPosition;
        knockedDuration = controller.knockedDuration;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetTrigger("isPunching");
        Punch();
        controller.Stop();
    }

    public override void OnHandleInput()
    {
        base.OnHandleInput();
        /*if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            controller.Move();
        }*/
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
        rb.velocity = Vector3.zero;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void Punch()
    {
        Collider[] colliders = Physics.OverlapSphere(punchPosition.transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Remy"))
            {
                collider.GetComponent<RemyController>().Knocked(knockedDuration);
            }
        }
    }
}

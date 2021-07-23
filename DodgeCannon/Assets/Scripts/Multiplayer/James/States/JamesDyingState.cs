using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesDyingState : JamesState
{
    private Animator animator;
    private int deathSoundChooser;
    private AudioSource audiosource;
    public JamesDyingState(JamesController controller, JamesStateMachine fsm) : base(controller, fsm)
    {
        animator = controller.GetComponent<Animator>();
        deathSoundChooser = Random.Range(0, controller.deathSound.Length);
        audiosource = controller.GetComponent<AudioSource>();
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        animator.SetTrigger("isDying");
        audiosource.clip = controller.deathSound[deathSoundChooser];
        audiosource.Play();
        controller.isAlive = false;
    }
}

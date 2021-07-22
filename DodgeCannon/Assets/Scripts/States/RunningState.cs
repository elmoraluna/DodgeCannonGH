using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunningState : PlayerState
    {
        private float horizontalMovement;
        private float verticalMovement;
        private float speed;
        private int playerInput;
        private Rigidbody rb;
        private Animator animator;
        private float rotationSpeed;
        public RunningState(PlayerController controller, PlayerStateMachine fsm) : base(controller, fsm)
        {
            rb = controller.GetComponent<Rigidbody>();
            speed = controller.speed;
            animator = controller.GetComponent<Animator>();
            playerInput = controller.playerInput;
            rotationSpeed = controller.rotationSpeed;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            animator.SetBool("isRunning", true);
        }
    
        public override void OnHandleInput()
        {
            base.OnHandleInput();
            if (playerInput == 0 || playerInput == 1)
            {
                horizontalMovement = Input.GetAxis("Horizontal");
                verticalMovement = Input.GetAxis("Vertical");
            }else if (playerInput == 2)
            {
                
            }
            if (Input.GetKey(KeyCode.Space))
            {
                controller.Punch();
            }
        }
    
        public override void OnLogicUpdate()
        {
            base.OnLogicUpdate();
        }
    
        public override void OnPhysicsUpdate()
        {
            base.OnPhysicsUpdate();
            Vector3 movement = new Vector3(horizontalMovement,
                0f,
                verticalMovement);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
            controller.transform.Translate(movement * (speed * Time.deltaTime), Space.World);
            Vector3 offset = controller.transform.position - controller.centerPosition.position;
            controller.transform.position = controller.centerPosition.position + Vector3.ClampMagnitude(offset, 7f);
            if (rb.velocity == Vector3.zero)
            {
                controller.Stop();
            }
        }
    }


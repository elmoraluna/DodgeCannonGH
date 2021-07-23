using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesController : MonoBehaviour
{
    private JamesStateMachine fsm;
    private JamesIdleState idleState;
    private JamesDyingState dyingState;
    private JamesKnockedState knockedState;
    private JamesPunchingState punchingState;
    private JamesRunningState runningState;
    private AudioSource audiosource;
    
    public int playerInput = 0;
    public float speed;
    public float rotationSpeed;
    public bool isAlive = true;
    public Transform centerPosition;
    public GameObject punchPosition;
    public float knockedDuration;
    public AudioClip[] deathSound;
    // Start is called before the first frame update
    void Start()
    {
        if (centerPosition == null)
        {
            centerPosition = GameManagerMultiplayer.Instance.centerPosition;
        }
        fsm = new JamesStateMachine();
        idleState = new JamesIdleState(this, fsm);
        runningState = new JamesRunningState(this, fsm);
        dyingState = new JamesDyingState(this, fsm);
        knockedState = new JamesKnockedState(this, fsm);
        punchingState = new JamesPunchingState(this, fsm);
        audiosource = GetComponent<AudioSource>();
        knockedDuration = 1f;
        fsm.Start(idleState);
    }
    
    // Update is called once per frame
    void Update()
    {
        fsm.CurrentState.OnHandleInput();
        fsm.CurrentState.OnLogicUpdate();
    }
    
    private void FixedUpdate()
    {
        fsm.CurrentState.OnPhysicsUpdate();
    }
    
    public void Move()
    {
        if (isAlive)
        {
            fsm.ChangeState(runningState);
        }
    }
    
    public void Stop()
    {
        if (isAlive)
        {
            fsm.ChangeState(idleState);
        }
    }

    public void Punch()
    {
        if (isAlive)
        {
            fsm.ChangeState(punchingState);
        }
    }

    public void Knocked(float knockDuration)
    {
        if (isAlive)
        {
            fsm.ChangeState(knockedState);
            StartCoroutine(GetUnknocked(knockDuration));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Cannonball") && isAlive)
        {
            fsm.ChangeState(dyingState);
        }
    }

    IEnumerator GetUnknocked(float knockDuration)
    {
        if (isAlive)
        {
            yield return new WaitForSeconds(knockDuration);
            fsm.ChangeState(idleState);
        }
    }
}

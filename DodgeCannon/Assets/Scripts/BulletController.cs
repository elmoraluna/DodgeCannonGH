using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    private AudioSource audiosource;
    //public float force;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        audiosource = GetComponent<AudioSource>();
        yield return new WaitForSeconds(0.5f);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * GameManager.Instance.cannonForce, ForceMode.Impulse);
        audiosource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Cannonball"))
        {
            Destroy(gameObject);
        }
    }
}

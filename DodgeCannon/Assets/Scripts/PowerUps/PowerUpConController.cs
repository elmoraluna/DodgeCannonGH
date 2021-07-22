using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpConController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroyed", 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            int powerUpChooser = Random.Range(0, 2);
            switch (powerUpChooser)
            {
                case 0:
                    GameManager.Instance.PlusForce();
                    break;
                case 1:
                    GameManager.Instance.SetDifficultyHard();
                    break;
            }
            GameManager.Instance.isPowerUpActive = true;
            Destroyed();
        }
    }

    private void Destroyed()
    {
        GameManager.Instance.powerUpSpawned = false;
        Destroy(gameObject);
    }
}

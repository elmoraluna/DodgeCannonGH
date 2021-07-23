using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpProMultiplayer : MonoBehaviour
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
        if (collision.transform.CompareTag("Remy") || collision.transform.CompareTag("James"))
        {
            int powerUpChooser = Random.Range(0, 2);
            switch (powerUpChooser)
            {
                case 0:
                    GameManagerMultiplayer.Instance.MinusForce();
                    break;
                case 1:
                    GameManagerMultiplayer.Instance.SetDifficultyEasy();
                    break;
            }
            GameManagerMultiplayer.Instance.isPowerUpActive = true;
            Destroyed();
        }
    }
    
    private void Destroyed()
    {
        GameManagerMultiplayer.Instance.powerUpSpawned = false;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMultiplayer : MonoBehaviour
{
    private static GameManagerMultiplayer instance;
    public static GameManagerMultiplayer Instance
    {
        get { return instance;  }
    }
    
    /*public Transform playerRemy;
    public Transform playerJames;*/
    private RemyController playerControllerRemy;
    private JamesController playerControllerJames;
    //private PlayerController playerController;
    private float distancia = 8.6f;
    public GameObject cannonPrefab;
    public GameObject bulletPrefab;
    public GameObject remy;
    public GameObject james;
    private GameObject player;
    public GameObject powerUpPro;
    public GameObject powerUpCon;
    private Transform firePoint;
    public Transform centerPosition;
    public Image image;
    public Button restart;
    public Button menu;
    public Image powerUpBackground;
    public Text powerUpText;
    public Multiplayer endBackground;
    public Text segundos;
    public AudioClip endOfGame;
    private AudioSource audiosource;
    private float tiempo;
    private GameManagerMultiplayer.Difficulty dificultad;
    private float cantidadCannon;
    private float siguienteSpawn;
    private float periodo;
    private int choosePlayer;
    public bool isPowerUpActive = false;
    private float siguientePowerUp;
    public bool powerUpSpawned = false;
    public float cannonForce;
    private bool plusMinusForce = false;
    private bool endGameEntrance = true;
    public float duracionPowerUp = 15f;

    enum Difficulty
    {
        EASY,
        NORMAL,
        HARD
    }
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerRemy = remy.GetComponent<RemyController>();
        playerControllerJames = james.GetComponent<JamesController>();
        dificultad = GameManagerMultiplayer.Difficulty.NORMAL;
        tiempo = 0f;
        siguienteSpawn = 0f;
        periodo = 2f;
        choosePlayer = Random.Range(0, 2);
        //SpawnPlayer(choosePlayer);
        //playerController = player.GetComponent<PlayerController>();
        var circulo = new GameObject {name = "CirculoArena" };
        circulo.transform.Translate(new Vector3(25, 1, 25));
        circulo.DrawCircle(8f, 0.5f);
        audiosource = GetComponent<AudioSource>();
        siguientePowerUp = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerJames.isAlive && playerControllerRemy.isAlive)
        {
            tiempo += Time.deltaTime;
            FormatoTiempo(tiempo);
            if (tiempo > siguienteSpawn)
            {
                siguienteSpawn += periodo;
                StartCoroutine(SpawnCannon(GetCannonCount(tiempo, dificultad)));
            }
            if (tiempo > 45 && tiempo > siguientePowerUp && !isPowerUpActive && !powerUpSpawned)
            {
                siguientePowerUp = tiempo + Random.Range(10, 20);
                float powerUpDistance = Random.Range(0f, 7.8f);
                Vector3 powerUpPosition = PowerUpLocation(powerUpDistance);
                //int powerUpChooser = Random.Range(0, 2);
                int powerUpChooser = 1;
                powerUpSpawned = true;
                switch (powerUpChooser)
                {
                    case 0:
                        Instantiate(powerUpPro, powerUpPosition, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(powerUpCon, powerUpPosition, Quaternion.identity);
                        break;
                }
            }
        }
        else
        {
            if (endGameEntrance)
            {
                audiosource.loop = false;
                audiosource.clip = endOfGame;
                audiosource.Play();
                endGameEntrance = false;
                Invoke("EndOfGame", 3f);
            }
            
        }
        /*if (Input.GetKey(KeyCode.R))
        {
            float angulo = Random.Range(-Mathf.PI, Mathf.PI);
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Mathf.Cos(angulo),
                0f,
                Mathf.Sin(angulo)) * distancia;
            var cannon = Instantiate(cannonPrefab, spawnPosition, Quaternion.identity) as GameObject;
            cannon.transform.LookAt(transform);
            cannon.transform.rotation = Quaternion.Euler(-90f, cannon.transform.eulerAngles.y, -90f);
            firePoint = cannon.transform.GetChild(0).transform;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(cannon, 1.5f);
        }*/
    }

    public void EndOfGame()
    {
        endGameEntrance = false;
        image.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        endBackground.Setup(tiempo);
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void FormatoTiempo(float tiempo)
    {
        tiempo += 1;

        float minutos = Mathf.FloorToInt(tiempo / 60);
        float seconds = Mathf.FloorToInt(tiempo % 60);
        float milisegundos = (tiempo % 1) * 1000;
        segundos.text = string.Format("{0:00}:{1:00}", minutos, seconds);
    }

    IEnumerator SpawnCannon(float numCannon)
    {
        for (int i = 0; i < numCannon; i++)
        {
            float angulo = Random.Range(-Mathf.PI, Mathf.PI);
            Vector3 spawnPosition = transform.position;
            spawnPosition += new Vector3(Mathf.Cos(angulo),
                0f,
                Mathf.Sin(angulo)) * distancia;
            var cannon = Instantiate(cannonPrefab, spawnPosition, Quaternion.identity) as GameObject;
            cannon.transform.LookAt(transform);
            cannon.transform.rotation = Quaternion.Euler(-90f, cannon.transform.eulerAngles.y, -90f);
            firePoint = cannon.transform.GetChild(0).transform;
            yield return new WaitForSeconds(0.2f);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(cannon, 1.5f);
        }
    }

    private static float GetCannonCount(float seconds, GameManagerMultiplayer.Difficulty difficulty)
    {
        int factor = 1;
        if (difficulty == GameManagerMultiplayer.Difficulty.EASY)
        {
            factor = 8;
        }else if (difficulty == GameManagerMultiplayer.Difficulty.NORMAL)
        {
            factor = 6;
        }else if(difficulty == GameManagerMultiplayer.Difficulty.HARD)
        {
            factor = 4;
        }
        return Mathf.Round(Mathf.Sqrt(seconds / factor)) + 1;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void SpawnPlayer(int number)
    {
        if (number == 0)
        {
            player = Instantiate(remy, transform.position, transform.rotation) as GameObject;
        }
        else
        {
            player = Instantiate(james, transform.position, transform.rotation) as GameObject;
        }
    }

    public void SetDifficultyEasy()
    {
        dificultad = GameManagerMultiplayer.Difficulty.EASY;
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Dificultad fácil";
        Invoke("HidePowerUpText", 3f);
        Invoke("SetDifficultyNormal", duracionPowerUp);
    }

    public void SetDifficultyNormal()
    {
        dificultad = GameManagerMultiplayer.Difficulty.NORMAL;
        siguientePowerUp = tiempo + Random.Range(10, 20);
        isPowerUpActive = false;
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Dificultad normal";
        Invoke("HidePowerUpText", 3f);
    }

    public void SetDifficultyHard()
    {
        dificultad = GameManagerMultiplayer.Difficulty.HARD;
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Dificultad difícil";
        Invoke("HidePowerUpText", 3f);
        Invoke("SetDifficultyNormal", duracionPowerUp);
    }

    private Vector3 PowerUpLocation(float distancia)
    {
        float angulo = Random.Range(-Mathf.PI, Mathf.PI);
        Vector3 spawnPosition = transform.position;
        spawnPosition += new Vector3(Mathf.Cos(angulo),
            0f,
            Mathf.Sin(angulo)) * distancia;
        return spawnPosition;
    }

    public void NormalForce()
    {
        if (plusMinusForce)
        {
            cannonForce /= 1.5f;
        }
        else
        {
            cannonForce /= 0.75f;
        }
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Velocidad normal";
        Invoke("HidePowerUpText", 3f);
        siguientePowerUp = tiempo + Random.Range(10, 20);
        isPowerUpActive = false;
    }

    public void PlusForce()
    {
        plusMinusForce = true;
        cannonForce *= 1.5f;
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Balas de cañón más veloces";
        Invoke("HidePowerUpText", 3f);
        Invoke("NormalForce", duracionPowerUp);
    }

    public void MinusForce()
    {
        plusMinusForce = false;
        cannonForce *= 0.75f;
        powerUpBackground.gameObject.SetActive(true);
        powerUpText.text = "Balas de cañón más lentas";
        Invoke("HidePowerUpText", 3f);
        Invoke("NormalForce", duracionPowerUp);
    }

    public void HidePowerUpText()
    {
        powerUpBackground.gameObject.SetActive(false);
    }
}

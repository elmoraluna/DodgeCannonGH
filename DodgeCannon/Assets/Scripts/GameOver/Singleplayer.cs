using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Singleplayer : MonoBehaviour
{
    public Text textoPuntaje;

    public void Setup(float tiempo)
    {
        gameObject.SetActive(true);
        FormatoTiempo(tiempo);
    }
    
    private void FormatoTiempo(float tiempo)
    {
        tiempo += 1;

        float minutos = Mathf.FloorToInt(tiempo / 60);
        float seconds = Mathf.FloorToInt(tiempo % 60);
        float milisegundos = (tiempo % 1) * 1000;
        textoPuntaje.text = string.Format("Sobreviviste {0:00}:{1:00} minutos", minutos, seconds);
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

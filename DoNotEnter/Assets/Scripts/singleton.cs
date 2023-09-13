using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class singleton : MonoBehaviour
{
    public static singleton Instance;
 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
       
    }
    public void EscenaJuego()
    {
        SceneManager.LoadScene(1);
    }
    public void SalirDelJuego()
    {

            // Si estás en una compilación independiente (por ejemplo, una aplicación ejecutable), usa Application.Quit()
            Application.Quit();

    }
}


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class singleton : MonoBehaviour
{
    public static singleton Instance;
    [SerializeField] TMP_InputField input;
    [SerializeField] string folder = "Intentos";
    string nombre;
    [SerializeField] bool gameStarted = false;
    float timer = 0;

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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "menu" && gameStarted)
        {
            gameStarted = false;
            IntentoInfo a = new IntentoInfo();
            a.nombre = nombre;
            a.time = timer;
            string json = JsonUtility.ToJson(a);
            string fileName = nombre + "_" + timer + ".json";
            string path = Path.Combine(Application.persistentDataPath, folder);

            if(!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, fileName);

            System.IO.File.WriteAllText(path, json);
            timer = 0;
        }
        if(scene.name == "EscenaIsla")
        {
            gameStarted = true;
        }
        Debug.Log(scene.name);
        timer += Time.deltaTime;
    }
    public void EscenaJuego()
    {
        nombre = input.text;
        SceneManager.LoadScene(1);
        timer = 0;
    }
    public void SalirDelJuego()
    {
        // Si estás en una compilación independiente (por ejemplo, una aplicación ejecutable), usa Application.Quit()
        Application.Quit();
    }
}


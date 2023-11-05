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
    [SerializeField] string fileName = "Intentos.jsop";
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

            //string json = JsonUtility.ToJson(a);
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json;
            IntentoInfo a;

            if (System.IO.File.Exists(path))
            {
                json = System.IO.File.ReadAllText(path);
                a = JsonUtility.FromJson<IntentoInfo>(json);
            }
            else
            {
                a = new IntentoInfo();
                a.nombre = new string[0];
                a.time = new float[0];
            }

            string[] nombres = new string[a.nombre.Length + 1];
            float[] times = new float[a.time.Length + 1];

            for (int i = 0; i < a.nombre.Length; i++)
            {
                nombres[i] = a.nombre[i];
                times[i] = a.time[i];
            }
            nombres[nombres.Length - 1] = nombre;
            times[nombres.Length - 1] = timer;

            a.nombre = nombres;
            a.time = times;

            json = JsonUtility.ToJson(a);
            System.IO.File.WriteAllText(path, json);
            Debug.Log("PATH: " +  path  + " JSON: " + json);

            timer = 0;
        }
        if(scene.name == "EscenaIsla")
        {
            gameStarted = true;
        }
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


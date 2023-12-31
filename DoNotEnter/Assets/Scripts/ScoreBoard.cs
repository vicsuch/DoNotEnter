﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ScoreBoard : MonoBehaviour
{
    TextMeshProUGUI UI;
    [SerializeField] string fileName = "Intentos.json";
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Escribir();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 3f)
        {
            Escribir();
            time = 0f;
        }
    }
    
    void Escribir()
    {
        UI = GetComponent<TextMeshProUGUI>();
        UI.text = "Puntuaciones:\n";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            IntentoInfo a = JsonUtility.FromJson<IntentoInfo>(json);
            int[] top3 = a.TopScores(3);
            for (int i = 0; i < top3.Length; i++)
            {
                UI.text += "\n" + a.nombre[top3[i]] + " Monedas: " + a.monedas[top3[i]] + " Tiempo: " + Mathf.Round((a.time[top3[i]] /60f)*100f)/100f + " Minutos ";
            }
        }
        else
        {
            UI.text += "\nParece que nadie a jugado hasta ahora\nTermina el nivel para aparecer en las puntuaciones!";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sol : MonoBehaviour
{
    public Light sol1; // La luz direccional que representa el sol o la luna.
    public float cicloDuracion = 120.0f; // Duración del ciclo día-noche en segundos.
    private float tiempoPasado = 0.0f;
    public int rotationscale = 10;
    public GameObject yo;
    void Update()
    {

     

            /*
        tiempoPasado += Time.deltaTime;

        // Calcula el valor normalizado del ciclo de día-noche (0 a 1).
        float cicloNormalizado = tiempoPasado / cicloDuracion;

        // Ajusta la intensidad de la luz basándose en el ciclo normalizado.
        sol1.intensity = Mathf.Lerp(0.2f, 1.2f, cicloNormalizado);

        // Ajusta el color del fondo del cielo (Skybox) o la cámara según el ciclo.
        RenderSettings.ambientIntensity = Mathf.Lerp(0.2f, 1.2f, cicloNormalizado);

        // Resetea el tiempo cuando el ciclo termina.
        if (tiempoPasado > cicloDuracion)
        {
            tiempoPasado = 0.0f;
        }*/
    }

}

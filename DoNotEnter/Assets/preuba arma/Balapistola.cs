using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balapistola : MonoBehaviour
{
    public float tiempoDeVida = 3.3f; // Tiempo antes de autodestruirse

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        // Puedes añadir aquí lógica para gestionar colisiones si es necesario
        // Por ejemplo, puedes añadir efectos visuales o sonidos al impacto.

        Destroy(gameObject); // Destruir el proyectil al colisionar con algo
    }
}

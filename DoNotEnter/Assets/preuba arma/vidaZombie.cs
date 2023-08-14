﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaZombie : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    public int abcprueba = 1000;
    private SaludJugador saludJugador;

    // Start is called before the first frame update
    void Start()
    {
        saludJugador = jugador.GetComponent<SaludJugador>();
        if (gameObject.CompareTag("zombietag"))
        {
            vida_zombie = 100;
        }else if (gameObject.CompareTag("cocotag"))
        {
            vida_zombie = 50;

        }else if (gameObject.CompareTag("maderatag"))
        {
            vida_zombie = 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie <= 0)
        {
            // Realizar acciones cuando el zombie muere, como destruir el GameObject.
            Destroy(gameObject);
        }
    } 
     public void RestarVida(int amount)
    {
        vida_zombie -= amount;
        
        
    }
}

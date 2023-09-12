﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaenemigo : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    public  SaludJugador componenteEncontrado;
    [SerializeField] private int dañoPorFuego = 4;

    // Start is called before the first frame update
    void Start()
    {
       //SaludJugador componenteEncontrado = FindObjectOfType<SaludJugador>();
       jugador= GameObject.Find("FPSController");
       componenteEncontrado = jugador.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie <= 0)
        {
            componenteEncontrado.SumarMuerte();
            Destroy(gameObject);
        }
    }
    public void RestarVida(int amount)
    {
        vida_zombie -= amount;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("parti");
        if(other.CompareTag("Fuego"))
        {
            RestarVida(dañoPorFuego);
        }
    }
}
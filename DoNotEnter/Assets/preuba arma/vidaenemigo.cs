using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaenemigo : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    public int abcprueba = 1000;
    private SaludJugador saludJugador;

    // Start is called before the first frame update
    void Start()
    {
        saludJugador = jugador.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void RestarVida(int amount)
    {
        vida_zombie -= amount;


    }
}

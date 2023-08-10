using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaZombie : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    private SaludJugador saludJugador;
    // Start is called before the first frame update
    void Start()
    {
        saludJugador = jugador.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie < 1)
        {
            Destroy(gameObject);
            // falta agregar 1 al contador
        }
    }
}

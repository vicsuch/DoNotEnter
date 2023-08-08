using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaZombie : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    private Saludjugador saludjugador;
    // Start is called before the first frame update
    void Start()
    {
        saludjugador = jugador.GetComponent<Saludjugador>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vida_zombie < 1)
        {
         
            Destroy(gameObject);
            saludjugador.balaslaser++;
            saludjugador.balasfuego++;

        }
    }
}

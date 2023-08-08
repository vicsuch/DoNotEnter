using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saludjugador : MonoBehaviour
{
    public GameObject jugador;
    public int vida = 100;
    public Vector3 nuevaPosicion = new Vector3(0f, 2f, 0f);
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (vida<1)
        {
            controller.enabled = false;

            jugador.transform.position = nuevaPosicion;
            vida = 100;
            controller.enabled = true;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject jugador; // asignar al fpscontroller
    private SaludJugador saludJugador;
    public Transform puntoDisparo; //asignar a la camara
    
    // Start is called before the first frame update
    void Start()
    {
        saludJugador = jugador.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o")) // Cambiar a click
        {
            DispararRayoLaser();
           
        }
    }
    void DispararRayoLaser()
    {
        //raycast
        Ray rayo = new Ray(puntoDisparo.position, puntoDisparo.forward);
        RaycastHit[] hits = Physics.RaycastAll(rayo, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
        {
            //accede al script del enemigo
            //GameObject enemigo = hit.collider.gameObject;
           // VidaZombie vidaZombie = enemigo.GetComponent<VidaZombie>();
            VidaZombie vidaZombie = hit.collider.gameObject.GetComponent<VidaZombie>();
            if (vidaZombie != null)
            {
                // baja vida
                int puntuacion = Random.Range(40, 70);
                vidaZombie.vida_zombie -= 10;
            }
            
        }
    }
}

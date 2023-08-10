using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Transform puntoDisparo; //asignar a la camara

    public float alcance = 100f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g")) // cambiar a click
        {
          
            Disparar();
        }
    }
    void Disparar()
    {
        //raycast
        Ray rayo = new Ray(puntoDisparo.position, puntoDisparo.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, alcance))
        {
            //accede al script del enemigo
            GameObject objetoImpactado = hitInfo.collider.gameObject;
            VidaZombie vidaZombie = objetoImpactado.GetComponent<VidaZombie>();

           
            // baja vida
            int puntuacion = Random.Range(20, 40);
            vidaZombie.vida_zombie -= puntuacion;

           
        }
    }
}

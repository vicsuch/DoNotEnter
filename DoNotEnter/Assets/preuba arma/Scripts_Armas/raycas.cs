using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycas : MonoBehaviour
{
    public Transform puntoDisparo;
    public float alcance = 100f;

    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g")) // Cambia "Fire1" a la entrada que uses para disparar
        {
            Debug.Log("hf");
            Disparar();
        }
    }
    void Disparar()
    {
        Ray rayo = new Ray(puntoDisparo.position, puntoDisparo.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, alcance))
        {
            GameObject objetoImpactado = hitInfo.collider.gameObject;
            vidaZombie vidaZombie = objetoImpactado.GetComponent<vidaZombie>();

            int puntuacion = Random.Range(20, 40);
            vidaZombie.vida_zombie -= puntuacion;

            Debug.Log("¡Conseguido! Objeto impactado: " + hitInfo.collider.name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ItemData))]
public class Raycast : MonoBehaviour
{
    [SerializeField] private ItemData itemData; //asignar a la camara

    public float alcance = 100f;

    private void Start()
    {
        itemData = transform.GetComponent<ItemData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar")) // cambiar a click
        {
            Debug.Log("dsparog");
            Disparar();
        }
    }
    
    void Disparar()
    {
        if(itemData.puntoDeVista == null)
        {
            Debug.Log("No se asigno el punto de disparo en la arma: " + this.name);
            return;
        }

        Ray rayo = new Ray(itemData.puntoDeVista.position, itemData.puntoDeVista.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, alcance))
        {
            //accede al script del enemigo
            //  GameObject objetoImpactado = hitInfo.collider.gameObject;
            //  VidaZombie vidzom = objetoImpactado.GetComponent<VidaZombie>();

            vidaenemigo vidzom = hitInfo.collider.gameObject.GetComponent<vidaenemigo>();
            // baja vida
            int puntuacion = Random.Range(20, 40);
           
            vidzom.RestarVida(puntuacion);
        }
        Debug.Log(hitInfo.transform.name);
    }
}

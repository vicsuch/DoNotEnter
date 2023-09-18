using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ItemData))]
public class Raycast : MonoBehaviour
{
    [SerializeField] private int maxDamage = 50;
    [SerializeField] private int minDamage = 30;
    [SerializeField] private ItemData itemData; //asignar a la camara
    [SerializeField] private LayerMask ObjetosQueLePuedeDisparar;
    private bool isCoolDownOver = true;
    private float coolDownInSeconds = 0.1f;

    public float alcance = 100f;

    private void Start()
    {
        itemData = transform.GetComponent<ItemData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar") && isCoolDownOver && itemData.isGrabbed && itemData.balasRestantes > 0) // cambiar a click
        {
            Disparar();
        }
    }
    
    void Disparar()
    {
        isCoolDownOver = false;
        itemData.balasRestantes--;
        if (itemData.puntoDeVista == null)
        {
            Debug.Log("No se asigno el punto de disparo en la arma: " + this.name);
            return;
        }

        Ray rayo = new Ray(itemData.puntoDeVista.position, itemData.puntoDeVista.forward);
        Debug.DrawLine(itemData.puntoDeVista.position, itemData.puntoDeVista.position + (itemData.puntoDeVista.forward * alcance));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayo, out hitInfo, alcance, ObjetosQueLePuedeDisparar))
        {
            //accede al script del enemigo
            //  GameObject objetoImpactado = hitInfo.collider.gameObject;
            //  VidaZombie vidzom = objetoImpactado.GetComponent<VidaZombie>();
            Debug.Log(hitInfo.collider.gameObject.name);
            vidaenemigo vidzom = hitInfo.collider.gameObject.GetComponent<vidaenemigo>();
            // baja vida
            int puntuacion = Random.Range(minDamage, maxDamage);
           
            if (vidzom != null)
            {
                vidzom.RestarVida(puntuacion);
            }
        }
        Invoke("SetCoolDownOver", coolDownInSeconds);
    }
    void SetCoolDownOver()
    {
        isCoolDownOver = true;
    }
}

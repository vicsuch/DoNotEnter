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
    [SerializeField] GameObject prefabdardo;
    private bool isCoolDownOver = true;
    private float coolDownInSeconds = 0.4f;
    [SerializeField] AudioSource audio;
    public float alcance = 100f;
    public Animator anim;


    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        itemData = transform.GetComponent<ItemData>();

    }

    // Update is called once per frame
    void desactivo()
    {
        anim.SetBool("disparo", false);
        isCoolDownOver = true;
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar") && isCoolDownOver && itemData.isGrabbed && itemData.balasRestantes > 0) // cambiar a click
        {
            isCoolDownOver = false;
            anim.SetBool("disparo", true);
            Invoke("desactivo", 0.4f);
            Disparar();
            SonidoDisparo();
        }
    }
    void SonidoDisparo()
    {
        audio.Play();
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
            Quaternion dardoRotation = Quaternion.LookRotation(rayo.direction);

            GameObject dardo = Instantiate(prefabdardo, hitInfo.point, dardoRotation);
            dardo.transform.SetParent(hitInfo.collider.transform.GetChild(1).gameObject.transform.GetChild(3)); // Hacer que el dardo sea hijo del objeto impactado
            Rigidbody dardoRigidbody = dardo.GetComponent<Rigidbody>();
            if (dardoRigidbody != null)
            {
                dardoRigidbody.isKinematic = true; // Desactivar la física del dardo para que se quede pegado
            }

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

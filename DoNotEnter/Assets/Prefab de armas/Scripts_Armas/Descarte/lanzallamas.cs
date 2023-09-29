using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LanzaLlamas : MonoBehaviour
{
    [SerializeField] private SaludJugador saludJugador;

    public float alcance = 5f;
    public float anguloDisparo = 45f; // Ángulo de disparo en grados

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar")) // Cambia "Fire1" a la entrada que uses para disparar
        {
            Atacar();
        }
    }
    void Atacar()
    {
        Vector3 direccionDisparo = transform.forward;
        float anguloRadianes = anguloDisparo * Mathf.Deg2Rad;
        direccionDisparo = Quaternion.Euler(0, -anguloDisparo / 2, 0) * direccionDisparo;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, alcance, direccionDisparo, 0f);

        foreach (RaycastHit hit in hits)
        {
            GameObject enemigo = hit.collider.gameObject;
        //    VidaZombie vidaZombie = enemigo.GetComponent<VidaZombie>();

           // if (vidaZombie != null)
            {
                int puntuacion = Random.Range(25, 45);
              //  vidaZombie.vida_zombie -= puntuacion;

                Debug.Log("Daño a " + enemigo.name + ": " + puntuacion);
            }
        }
    }
}
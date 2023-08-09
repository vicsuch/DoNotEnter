using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaLlamas : MonoBehaviour
{
    public float alcance = 5f;
    public GameObject jugador;
    private SaludJugador saludJugador;

    // Start is called before the first frame update
    void Start()
    {
        saludJugador = jugador.GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c")) // Cambia "Fire1" a la entrada que uses para disparar
        {
            Atacar();
        }
    }
    void Atacar()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, alcance, transform.forward, 0f);

        foreach(RaycastHit hit in hits)
        {
            GameObject enemigo = hit.collider.gameObject;
        }
    }

}

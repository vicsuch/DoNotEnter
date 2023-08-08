using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanzallamas : MonoBehaviour
{
    public float alcance = 5f;
    public GameObject jugador;
    private Saludjugador saludjugador;

    // Start is called before the first frame update
    void Start()
    {
        saludjugador = jugador.GetComponent<Saludjugador>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c")&& saludjugador.balasfuego > 0) // Cambia "Fire1" a la entrada que uses para disparar
        {
           
            Atacar();
            saludjugador.balasfuego--;
        }
    }
    void Atacar()
    {
        Debug.Log("ufdguksd");
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, alcance, transform.forward, 0f);

        foreach (RaycastHit hit in hits)
        {
            GameObject enemigo = hit.collider.gameObject;

           
            Debug.Log(enemigo.name);
           
        }
    }

}

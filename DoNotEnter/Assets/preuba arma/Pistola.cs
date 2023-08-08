using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float velocidadProyectil = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) 
        {
            Debug.Log("hfj");
            Disparar();
        }
    }
    void Disparar()
    {
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        //        hitInfo.collider.gameObject.
        if (rb != null)
        {
            rb.velocity = puntoDisparo.forward * velocidadProyectil;
        }
    }
}

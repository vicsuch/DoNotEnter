using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneda : MonoBehaviour
{
    public SaludJugador componenteEncontrado;

    // Start is called before the first frame update
    void Start()
    {
        componenteEncontrado = GameObject.Find("FPSController").GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        componenteEncontrado.monedas_recogidas++;
        Destroy(gameObject);
    }
}

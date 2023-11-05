using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneda : MonoBehaviour
{
    public SaludJugador componenteEncontrado;
    float degresPerSecond = 90f;
    float unitMovement = 0.5f;
    float movementSpeed = 1f;
    float initialY;


    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        componenteEncontrado = GameObject.Find("FPSController").GetComponent<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + degresPerSecond * Time.deltaTime, 0f);
        transform.position = new Vector3 (transform.position.x, initialY + Mathf.Sin(Time.timeSinceLevelLoad * movementSpeed) * unitMovement, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            componenteEncontrado.monedas_recogidas++;
            Destroy(gameObject);
        }
    }
}

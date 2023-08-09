using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SaludJugador : MonoBehaviour
{
    public GameObject jugador;
    public int vida = 100;
    public Vector3 spawnPosition = new Vector3(0f, 2f, 0f);
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida < 0)
        {
            controller.enabled = false;
            jugador.transform.position = spawnPosition;
            controller.enabled = true;
            vida = 100;
        }
    }
}

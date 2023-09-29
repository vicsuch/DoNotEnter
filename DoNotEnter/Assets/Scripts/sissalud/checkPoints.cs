using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoints : MonoBehaviour
{
    public Vector3 targetPosition;
    public float distanceThreshold = 5f; // Distancia umbral para el cambio
    public int minimoActivar;
    public int numeroDeHoguera;
    SaludJugador comprobacion;
    public GameObject hijo;
    [SerializeField] GameObject[] objetosParaAparecer;
    AudioSource audiolas;
    int a = 0;
    private void Start()
    {
        audiolas = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        // Encuentra el objeto del jugador por su etiqueta (asegúrate de etiquetar al jugador como "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        comprobacion = player.GetComponent<SaludJugador>();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= distanceThreshold && comprobacion.zombiesAsesinados>=minimoActivar)
            {
               
                SaludJugador playerScript = player.GetComponent<SaludJugador>();
                if (playerScript != null)
                {

                    if (a == 0)
                    {
                        musica();
                        a++;
                    }
                    Debug.Log("Empieza el audio");
                
                    playerScript.ChangeVariable(targetPosition);
                    playerScript.ChangeVariableHoguera(numeroDeHoguera);
                    hijo.SetActive(true);
                    ActivarObjetos();
                }
            }
        }
    }
    void musica()
    {
        audiolas.Play();
    }
    void ActivarObjetos()
    {
        for (int i = 0; i < objetosParaAparecer.Length; i++)
        {
            objetosParaAparecer[i].SetActive(true);
        }
    }
}

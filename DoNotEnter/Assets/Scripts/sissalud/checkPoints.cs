using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoints : MonoBehaviour
{
    public Vector3 targetPosition;
    public int minimoActivar;
    public int numeroDeHoguera;
    SaludJugador comprobacion;
    public GameObject hijo;
    [SerializeField] GameObject[] objetosParaAparecer;
    [SerializeField] GameObject[] objetosParaDesaparecer;
    AudioSource audiolas;
    bool yaActivado = false;

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

            if (comprobacion.zombiesAsesinados >= minimoActivar && !yaActivado)
            {
                yaActivado = true;
                SaludJugador playerScript = player.GetComponent<SaludJugador>();
                if (playerScript != null)
                {
                    musica();
                
                    playerScript.ChangeVariable(targetPosition);
                    playerScript.ChangeVariableHoguera(numeroDeHoguera);
                    hijo.SetActive(true);
                    ActivarYDesactivarObjetos();
                }
            }
        }
    }
    void musica()
    {
        audiolas.Play();
    }
    void ActivarYDesactivarObjetos()
    {
        for (int i = 0; i < objetosParaAparecer.Length; i++)
        {
            objetosParaAparecer[i].SetActive(true);
        }
        for (int i = 0; i < objetosParaDesaparecer.Length; i++)
        {
            objetosParaDesaparecer[i].SetActive(false);
        }
    }
}

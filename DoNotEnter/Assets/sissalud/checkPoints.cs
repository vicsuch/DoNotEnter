﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoints : MonoBehaviour
{
    public Vector3 targetPosition;
    public float distanceThreshold = 5f; // Distancia umbral para el cambio
    public int minimoActivar;
    public int numeroDeHoguera;
    SaludJugador comprobacion;

    void Update()
    {
        // Encuentra el objeto del jugador por su etiqueta (asegúrate de etiquetar al jugador como "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        comprobacion = player.GetComponent<SaludJugador>();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= distanceThreshold &&comprobacion.zombiesAsesinados>=minimoActivar)
            {
                SaludJugador playerScript = player.GetComponent<SaludJugador>();
                if (playerScript != null)
                {
                    playerScript.ChangeVariable(targetPosition);
                    playerScript.ChangeVariableHoguera(numeroDeHoguera);
                }
            }
        }
    }
}

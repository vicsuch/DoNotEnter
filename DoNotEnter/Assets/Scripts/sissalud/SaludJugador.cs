﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class SaludJugador : MonoBehaviour
{
    public int vida = 100;
    public Vector3 spawnPosition = new Vector3(0f, 2f, 0f);
    public CharacterController controller;
    public int zombiesAsesinados = 0;
    [SerializeField] float alturaParaMorirse = 0f;

    public int numHoguera = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida < 1 || transform.position.y < alturaParaMorirse)
        {
            Morir();
        }
    }
    private void Morir()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ChangeVariable(Vector3 nuevoSpawn)
    {
        spawnPosition = nuevoSpawn ; 
    }
    public void ChangeVariableHoguera(int numero_)
    {
        numHoguera = numero_;
    }
    public void AtaqueZombie()
    {
        vida -= 20;
    }
    public void AtaqueCoco()
    {
        vida -= 40;
    }
    public void SumarMuerte()
    {
        zombiesAsesinados++;
    }
    public void AtaqueProyectilMuñeco()
    {
        vida -= 20;
    }
}

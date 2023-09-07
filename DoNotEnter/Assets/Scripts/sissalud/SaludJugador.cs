using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(CharacterController))]
public class SaludJugador : MonoBehaviour
{
    public int vida = 100;
    public Vector3 spawnPosition = new Vector3(0f, 2f, 0f);
    public CharacterController controller;
    public int zombiesAsesinados = 0;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI hogueraText;
    public int numHoguera=0;
    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        killText.text ="Kills: "+ zombiesAsesinados.ToString();
        hogueraText.text = "Numero de Hoguera: " + numHoguera.ToString();
        
        if (vida < 1)
        {
           
            controller.enabled = false;
            transform.position = spawnPosition;
            controller.enabled = true;
            vida = 100;
        }
     
    }
    public void ChangeVariable(Vector3 nuevoSpawn)
    {
        spawnPosition = nuevoSpawn ; 
    }
    public void ChangeVariableHoguera(int numero_)
    {
        numHoguera = numero_;
    }
    public void AtatqueZombie()
    {
        vida -= 20;
        
    }
    public void SumarMuerte()
    {
        Debug.Log("Sumar");
        zombiesAsesinados++; ;
    }
}

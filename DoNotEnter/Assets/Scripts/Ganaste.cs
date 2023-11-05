using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ganaste : MonoBehaviour
{
    [SerializeField] GameObject cartelDeGanar;
    [SerializeField] int numeroDeEscena = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Escena()
    {
        SceneManager.LoadScene(numeroDeEscena);
    }
    private void OnTriggerEnter(Collider other)
    {
        cartelDeGanar.SetActive(true);
        Invoke("Escena", 3f);
    }
}

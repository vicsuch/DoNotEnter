using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciadorEnemigo : MonoBehaviour
{
    [SerializeField] GameObject objetoParaInstanciar;
    [SerializeField] int maxObjects;
    [SerializeField] float spawnRadius;
    [SerializeField] Transform jugador;
    [SerializeField] List<Transform> objetosGenerados = new List<Transform>();
    [SerializeField] float tiempoDeEsperaParaSpawn = 1f;
    float tiempo = 0f;

    void Start()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            InstanciarObjeto();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < objetosGenerados.Count; i++)
        {
            if(objetosGenerados[i] == null)
            {
                objetosGenerados.RemoveAt(i);
            }
        }
        
        TryToInstanciate();
        
    }
    void TryToInstanciate()
    {
        tiempo += Time.deltaTime;
        if(tiempo >= tiempoDeEsperaParaSpawn && objetosGenerados.Count < maxObjects)
        {
            InstanciarObjeto();
            tiempo = 0f;
        }
    }
    void InstanciarObjeto()
    {
        Transform a = Instantiate(objetoParaInstanciar, transform.position + (new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0, spawnRadius)), Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f)).transform;
        ZombieController o = a.GetComponent<ZombieController>();
        if(o != null)
        {
            o.Instanciar(jugador);
        }
        objetosGenerados.Add(a);
    }
}

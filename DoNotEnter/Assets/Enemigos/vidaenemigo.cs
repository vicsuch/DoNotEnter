using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class vidaenemigo : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    public SaludJugador componenteEncontrado;
    [SerializeField] private int dañoPorFuego = 100;
    public animacionzombie animzombiescript;
    public NavMeshAgent movimiento;
    [SerializeField]  bool muerto = false;
    // Start is called before the first frame update
    void Start()
    {
        movimiento = GetComponent<NavMeshAgent>();
        //SaludJugador componenteEncontrado = FindObjectOfType<SaludJugador>();
        jugador = GameObject.Find("FPSController");
        componenteEncontrado = jugador.GetComponent<SaludJugador>();
        if (CompareTag("zombietag"))
        {
            animzombiescript = transform.GetChild(1).gameObject.GetComponent<animacionzombie>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie <= 0)
        {
            matarzombieanim();
        }
    }
    public void matarzombieanim()
    {
        movimiento.enabled = false;
        if (CompareTag("zombietag"))
        {
            if (muerto)
            {

            }
            else
            {
                animzombiescript.muerto();
                // Encuentra el GameObject hijo que deseas desligar
                GameObject objetoHijo = gameObject.transform.GetChild(0).gameObject; // Cambia el índice (0) según tus necesidades

                // Desliga el objeto hijo del objeto padre
                objetoHijo.transform.parent = null;
                Invoke("desmayo", 3);
                muerto = true;
            }
            
        }
        if (CompareTag("cocotag"))
        {
            componenteEncontrado.SumarMuerte();
            Destroy(gameObject);
        }
        if (CompareTag("muniecotag"))
        {
            GetComponent<MuñecoAtack>().matarmunie();

            Invoke("matarzombie", 3);
        }

    

    }
    public void desmayo()
    {
        componenteEncontrado.SumarMuerte();

        // Destruye el objeto padre
        Destroy(gameObject);
    }
    public void matarzombie()
    {
        componenteEncontrado.SumarMuerte();
        Destroy(gameObject);
    }
    public void RestarVida(int amount)
    {
        vida_zombie -= amount;
        BroadcastMessage("EnemigoRecibioDaño");
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Onparticle collision");
        if (other.CompareTag("Fuego"))
        {
            RestarVida(dañoPorFuego);
        }
        else
        {
            RestarVida(100);
        }
    }
}


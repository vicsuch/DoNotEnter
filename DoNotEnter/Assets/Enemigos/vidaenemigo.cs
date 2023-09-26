using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class vidaenemigo : MonoBehaviour
{
    public GameObject jugador;
    public int vida_zombie = 100;
    public  SaludJugador componenteEncontrado;
    [SerializeField] private int dañoPorFuego = 100;
    public animacionzombie animzombiescript;
    public NavMeshAgent movimiento;
    // Start is called before the first frame update
    void Start()
    {
       movimiento = GetComponent<NavMeshAgent>();
       //SaludJugador componenteEncontrado = FindObjectOfType<SaludJugador>();
       jugador= GameObject.Find("FPSController");
       componenteEncontrado = jugador.GetComponent<SaludJugador>();
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
        animzombiescript.muerto();
        Invoke("matarzombie", 3);
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
        if(other.CompareTag("Fuego"))
        {
            RestarVida(dañoPorFuego);
        }
        else
        {
            RestarVida(100);
        }
    }
}

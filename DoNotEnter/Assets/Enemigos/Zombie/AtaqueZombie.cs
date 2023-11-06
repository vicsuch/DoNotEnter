using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtaqueZombie : MonoBehaviour
{
    [SerializeField] private float knockBackFoward = 5f;
    [SerializeField] private float knockBackUp = 2.5f;
    [SerializeField] SaludJugador saludPlayer;
    NavMeshAgent agent;
    ZombieController controller;
    Transform transformAtacar;
    bool atackStarted;
    private float atackTimer;
    public animacionzombie animscript;
    [SerializeField] AudioSource audio;
    // Start is called before the first frame update

    void Start()
    {
        audio = GetComponent<AudioSource>();    
        atackTimer = Time.time;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<ZombieController>();
        animscript = transform.GetChild(1).gameObject.GetComponent<animacionzombie>();
        animscript.asignarnav(agent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Atack()
    {
        if (atackStarted)
        {
            return;
        }
        else
        {
            atackStarted = true;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController a = transformAtacar.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            audio.Play();
            a.AddForce(transform.forward * knockBackFoward + transform.up * knockBackUp);
            saludPlayer.AtaqueZombie();
            
        }
        atackStarted = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        SaludJugador nuevo = other.GetComponent<SaludJugador>();
        if(nuevo)
        {
            saludPlayer = nuevo;
            transformAtacar = other.transform;
            animscript.animationpegar();
            Invoke("Atack",0.8f);
        }
    }
}

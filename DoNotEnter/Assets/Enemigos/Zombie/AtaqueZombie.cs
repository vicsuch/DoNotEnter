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
    // Start is called before the first frame update
    void Start()
    {
        atackTimer = Time.time;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<ZombieController>();
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
            a.AddForce(transform.forward * knockBackFoward + transform.up * knockBackUp);
            Debug.Log("abc");
            saludPlayer.AtatqueZombie();
        }
        atackStarted = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        saludPlayer = other.GetComponent<SaludJugador>();
        if(saludPlayer != null)
        {
            transformAtacar = other.transform;
            Atack();
        }
    }
}

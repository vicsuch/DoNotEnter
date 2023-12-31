﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    //Para que la velocidad de los zombies no sean todas iguales
    //Esto los diferenciara mas y dara mas individualidad
    [SerializeField] Transform jugador;
    [SerializeField] Transform puntoDeVista;
    [SerializeField] float velVarietion = 0.7f;
    [SerializeField] float angularVelVarietion = 10f;
    [SerializeField] float accelerationVarietion = 2f;
    [SerializeField] float maxRaycastDistance = 200f;
    [SerializeField] float maxViewDistance = 100;
    [SerializeField] float maxViewAngle = 70;
    [SerializeField] private float startSleep = 0f;
    [SerializeField] public bool seeingPlayer = false;
    [SerializeField] private bool randomMovementStarted = false;
    [SerializeField] private LayerMask raycastLayerIgnore;
    private CharacterController jugadorCC;
    [SerializeField] private Vector3 lastVelocity = Vector3.zero;
    Vector3 destination;
    NavMeshAgent agent;
    bool agentExists = false;

    void Start()
    {
        if (!jugador)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }
        jugadorCC = jugador.GetComponent<CharacterController>();
        Iniciar();
    }

    void Update()
    {
        if(agentExists)
        {
            UpdateLineOfSight();
            UpdateDestination();
        }
        else
        {
            if(agent == null)
            {
                Iniciar();
            }
        }
    }
    void Iniciar()
    {
        try
        {
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1))
            {
                transform.position = closestHit.position;
                gameObject.AddComponent<NavMeshAgent>();
                agentExists = true;
            }
        }
        catch
        {
            agentExists = false;
            return;
        }
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        agent.speed += Random.Range(-velVarietion, velVarietion);
        agent.angularSpeed += Random.Range(-angularVelVarietion, angularVelVarietion);
        agent.acceleration += Random.Range(-accelerationVarietion, accelerationVarietion);
        destination = agent.destination;
    }
    public void Instanciar(Transform j)
    {
        Debug.Log("instancio");
        jugador = j;
    }
    void UpdateLineOfSight()
    {
        RaycastHit hit;
        Physics.Raycast(puntoDeVista.position, jugador.position - puntoDeVista.position, out hit, maxRaycastDistance, raycastLayerIgnore, QueryTriggerInteraction.Ignore);
        bool hasHit = false;
        hasHit = hit.transform == jugador;
    
        if (seeingPlayer)
        {
            SeeingPlayer(hasHit);
        }
        else
        {
            Vector3 sideView = jugador.position - puntoDeVista.position;
            Vector3 fow = puntoDeVista.forward;
            fow.y = 0;
            sideView.y = 0;
            float angle = Vector3.Angle(fow, sideView);
            SeeingPlayer(hasHit && angle < maxViewAngle && Vector3.Distance(puntoDeVista.position, jugador.position) < maxViewDistance);
        }
        if(hasHit)
        {
            Debug.DrawLine(puntoDeVista.position, hit.transform.position, Color.green);
        }
        else
        {
            if(hit.transform != null)
            {
                Debug.DrawLine(puntoDeVista.position, hit.transform.position, Color.red);
            }
        }
    }
    void EnemigoRecibioDaño()
    {
        seeingPlayer = true;
    }

    void UpdateDestination()
    {
        if (seeingPlayer)
        {
            destination = jugador.position;
            if (agent.isOnNavMesh)
            {
                agent.destination = destination;
                Debug.DrawLine(transform.position, destination);
            }
        }
        else
        {
            StartRandomMovement();
        }
    }
    void StartRandomMovement()
    {
        if (agent.velocity == Vector3.zero && !randomMovementStarted && (Time.time - startSleep) < 15f)
        {
            randomMovementStarted = true;
            Invoke("MoveRandomly", (Random.Range(0f, 2f)));
        }
    }
    void MoveRandomly()
    {
        destination += lastVelocity.normalized * Random.Range(1f, 3f) + new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
        if(agent.isOnNavMesh)
        {
            agent.destination = destination;
        }
        randomMovementStarted = false;
    }
    void SeeingPlayer(bool state)
    {
        if (!state && seeingPlayer)
        {
            startSleep = Time.time;
            lastVelocity = jugadorCC.velocity;
        }
        seeingPlayer = state;
    }
}
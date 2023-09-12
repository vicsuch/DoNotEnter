using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.CrossPlatformInput;
public class ElCocoController : MonoBehaviour
{
    [SerializeField] Transform jugador;
    [SerializeField] Transform puntoParaAtacar;
    [SerializeField] float maxNodeDistanceFromPrevious;
    [SerializeField] List<Vector3> pathToPlayerNodes;
    [SerializeField] int maxNodeCant = 5;
    [SerializeField] float nodeSideOffset = 0.1f;
    [SerializeField] float distanceBeforeEndingPath;
    [SerializeField] float maxRaycastDistance = 10f;
    [SerializeField] LayerMask raycastLayerNotIgnore;

    [SerializeField] bool hasSeenPlayer;
    bool isAtacking = false;
    [SerializeField] float atackPosLerp = 0f;
    [SerializeField] float lerpTimeMultiplier = 0.1f;
    Vector3 startLerpPos;
    Quaternion startLerpRotation;
    Vector3 playerLastPosition = Vector3.zero;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
    /*    if(jugador == null)
        {
            jugador = GameObject.Find("FPSController").transform;
        }
        if(puntoParaAtacar == null)
        {
            puntoParaAtacar = GameObject.Find("PuntoParaCoco").transform;
        }
        agent = GetComponent<NavMeshAgent>();*/
    
    
    
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSeenPlayer && !isAtacking)
        {
            GetPathToPlayer();
            DrawPath();
        }
        IsAtackActive();
        if(isAtacking)
        {
            EnemyLerpToPlayerFace();
        }
        else
        {
            WalkPath();
            SeeingPlayer();
        }
    }
    void Death()
    {
        if(CrossPlatformInputManager.GetButtonDown("Interact") && atackPosLerp >= 1f)
        {
            vidaenemigo salud = GetComponent<vidaenemigo>();
            salud.RestarVida(999999);
        }
    }
    void EnemyLerpToPlayerFace()
    {
        Death();
        transform.position = Vector3.Lerp(startLerpPos, puntoParaAtacar.position, atackPosLerp);
        transform.rotation = Quaternion.Lerp(startLerpRotation, puntoParaAtacar.rotation, atackPosLerp);
        atackPosLerp += Time.deltaTime * lerpTimeMultiplier;
    }
    void DeactivateColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    void IsAtackActive()
    {
        if (isAtacking) { return; }
        DeactivateColliders();
        if (Vector3.Distance(jugador.transform.position, transform.position) < 1f)
        {
            isAtacking = true;
            startLerpPos = transform.position;
            startLerpRotation = transform.rotation;
            agent.enabled = false;
        }
    }
    void SeeingPlayer()
    {
        if (hasSeenPlayer) { return; }
        RaycastHit hit;
        Physics.Raycast(transform.position, jugador.position - transform.position, out hit, maxRaycastDistance, raycastLayerNotIgnore, QueryTriggerInteraction.Ignore);
        
        Debug.DrawRay(transform.position, jugador.position - transform.position);
        
        if (hit.transform == jugador)
        {
            hasSeenPlayer = true;
        }
    }
    void Iniciar()
    {
        try
        {
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(transform.position, out closestHit, 500, 1) && agent == null)
            {
                gameObject.AddComponent<NavMeshAgent>();
            }
        }
        catch
        {
            
        }
        agent = GetComponent<NavMeshAgent>();
    }
    void WalkPath()
    {
        if (pathToPlayerNodes.Count == 0) { if (hasSeenPlayer) { agent.destination = jugador.position; } return; }
        
        Debug.Log("active and enabled: " + agent.isActiveAndEnabled + " on nav: " + agent.isOnNavMesh);
        if (agent.isActiveAndEnabled && agent.isOnNavMesh && agent != null)
        {
            if(agent.isStopped || Vector3.Distance(transform.position, pathToPlayerNodes[0]) < 1.5f)
            {
                pathToPlayerNodes.RemoveAt(0);
            }
            if(pathToPlayerNodes.Count > 0)
            {
                agent.destination = pathToPlayerNodes[0];
            }
        }
        else
        {
            Iniciar();
        }
    }    
    void DrawPath()
    {
        for (int i = 0; i < pathToPlayerNodes.Count - 1; i++)
        {
            Debug.Log("draw node: " + i);
            Debug.DrawLine(pathToPlayerNodes[i], pathToPlayerNodes[i + 1], Color.blue, 5f);
        }
    }

    void GetPathToPlayer()
    {
        if(Vector3.Distance(jugador.position, playerLastPosition) < 1f)
        {
            return;
        }
        playerLastPosition = jugador.position;
        pathToPlayerNodes = new List<Vector3>();
        for (int i = 1; i < maxNodeCant; i++)
        {
            Vector3 newNode = Vector3.Lerp(transform.position, jugador.position, i * 1f / maxNodeCant);
            if (Vector3.Distance(newNode, jugador.position) < distanceBeforeEndingPath)
            {
                newNode = jugador.position;
                pathToPlayerNodes.Add(newNode);
                break;
            }
            if(NavMeshCheckPoint(newNode))
            {
                pathToPlayerNodes.Add(newNode);
            }
        }
        for (int i = 0; i < pathToPlayerNodes.Count - 1; i++)
        {
            Vector3 dir;
            if(i % 2 == 0)
            {
                dir = Quaternion.Euler(0f, 90f, 0f) * (jugador.position - transform.position);
            }
            else
            {
                dir = Quaternion.Euler(0f, -90f, 0f) * (jugador.position - transform.position);
            }
            dir.Normalize();
            pathToPlayerNodes[i] += dir * Random.Range(0, nodeSideOffset);
        }
    }
    bool NavMeshCheckPoint(Vector3 pos, float maxDistance = 2f)
    {
        NavMeshHit hit;
        bool hasHit = NavMesh.SamplePosition(pos, out hit, maxDistance, NavMesh.AllAreas);
        return hasHit;
    }
}

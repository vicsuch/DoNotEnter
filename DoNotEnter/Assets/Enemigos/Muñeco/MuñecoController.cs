using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuñecoController : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    [SerializeField] Transform[] hidingPlaces;
    [SerializeField] List<Transform> validHidingPlaces = new List<Transform>();
    [SerializeField] LayerMask raycastLayerNotIgnore;
    [SerializeField] float ValidHidingPlaceMinDistance;
    [SerializeField] float ValidHidingPlaceMaxDistance;
    [SerializeField] float maxViewDistance;
    [SerializeField] bool hasSeenPlayer = false;
    [SerializeField] float fov;
    [SerializeField] float backwardsStepDistance = 5f;
    [SerializeField] float distanceToPlayerToStopEscaping = 2f;
    [SerializeField] bool escaping = false;
    NavMeshAgent agent;
    public MuñecoAtack scriptatack;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        scriptatack = GetComponent<MuñecoAtack>();
    }

    // Update is called once per frame
    void Update()
    {
        scriptatack.velocidad = agent.velocity.magnitude;
        RotateTowardsPlayer();
        CheckForPlayer();
        if(SeeingPlayer())
        {
            CheckHidingPlaces();
            float distancia = Vector3.Distance(jugador.transform.position, transform.position);
            SelectHidingPlace();

        }
    }
   
    void CheckForPlayer()
    {
        if (hasSeenPlayer)
        {
            return;
        }
        RaycastHit hit;
        Physics.Raycast(
            transform.position,
            jugador.transform.position - transform.position,
            out hit,
            maxViewDistance,
            raycastLayerNotIgnore,
            QueryTriggerInteraction.Ignore);
        if (hit.transform == jugador.transform)
        {
            hasSeenPlayer = true;
        }
    }

    bool SeeingPlayer()
    {
        if (hasSeenPlayer) { return true; }
        RaycastHit hit;
        Physics.Raycast(transform.position, jugador.transform.position - transform.position, out hit, maxViewDistance, raycastLayerNotIgnore, QueryTriggerInteraction.Ignore);
        bool hasHit = false;
        hasHit = hit.transform == jugador.transform;
        if (!hasHit) { return false; }
        Vector3 sideView = jugador.transform.position - transform.position;
        Vector3 fow = transform.forward;
        fow.y = 0;
        sideView.y = 0;
        float angle = Vector3.Angle(fow, sideView);
        hasSeenPlayer = (hasHit && angle < fov && Vector3.Distance(transform.position, jugador.transform.position) < maxViewDistance);
        return hasSeenPlayer;
    }
    void RotateTowardsPlayer()
    {
        if (!hasSeenPlayer) { return; }
        Vector3 dir = jugador.transform.position - transform.position;
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0f, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0f),transform.rotation, 0.4f * Time.deltaTime);
    }
    void SelectHidingPlace()
    {
        if(validHidingPlaces.Count == 0) { WalkBackWards(); return; }
        escaping = false;
        int closest = 0;
        float closestDistance = Vector3.Distance(validHidingPlaces[closest].position, jugador.transform.position);
        for (int i = 0; i < validHidingPlaces.Count; i++)
        {
            float distance = Vector3.Distance(validHidingPlaces[i].position, jugador.transform.position);
            if(distance < closestDistance)
            {
                closest = i;
                closestDistance = Vector3.Distance(validHidingPlaces[closest].position, jugador.transform.position);
            }
        }
        agent.destination = validHidingPlaces[closest].position;
    }
    void WalkBackWards()
    {
        if ((agent.remainingDistance > 1f && escaping) || distanceToPlayerToStopEscaping < Vector3.Distance(jugador.transform.position, transform.position)) { return; }
        escaping = true;
        agent.destination = ReflectRay(transform.position, transform.forward * -1f, backwardsStepDistance);
    }
    Vector3 ReflectRay(Vector3 start, Vector3 dir, float distance)
    {
        bool hasHit = Physics.Raycast(start, dir, out RaycastHit hit, distance, raycastLayerNotIgnore, QueryTriggerInteraction.Ignore);
        if (!hasHit) 
        {
            Vector3 destination = start + dir * distance;
            Debug.DrawLine(start, destination, Color.red, 2f);
            return (destination);
        }
        float distanceRemainingToTravel = distance - Vector3.Distance(start, hit.point);
        Vector3 newDir = dir;
        Vector3 normal = hit.normal;
        dir.y = 0;
        normal.y = 0f;
        dir.Normalize();
        normal.Normalize();
        newDir = Vector3.Reflect(newDir, normal);

        Vector3 newDestination = ReflectRay(hit.point, newDir, distanceRemainingToTravel);
        //Vector3 newDestination = hit.point;
        
        Debug.DrawLine(start, hit.point, Color.red, 10f);
        Debug.DrawLine(hit.point, hit.point + normal * 2, Color.blue, 10f);

        return newDestination;
    }
    void CheckHidingPlaces()
    {
        validHidingPlaces = new List<Transform>();
        for (int i = 0; i < hidingPlaces.Length; i++)
        {
            float distance = Vector3.Distance(hidingPlaces[i].position, jugador.transform.position);
            if(distance > ValidHidingPlaceMinDistance && distance < ValidHidingPlaceMaxDistance)
            {
                RaycastHit hit;
                Physics.Raycast(
                    hidingPlaces[i].position,
                    jugador.transform.position - hidingPlaces[i].position,
                    out hit,
                    float.PositiveInfinity,
                    raycastLayerNotIgnore,
                    QueryTriggerInteraction.Ignore);
                if (hit.transform == jugador.transform)
                {
                    validHidingPlaces.Add(hidingPlaces[i]);
                }
            }
        }
    }
}

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
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            CheckHidingPlaces();
            SelectHidingPlace();
        }
    }

    void SelectHidingPlace()
    {
        int closest = 0;
        for (int i = 0; i < validHidingPlaces.Count; i++)
        {
            float distance = Vector3.Distance(validHidingPlaces[i].position, jugador.transform.position);
            float closestDistance = Vector3.Distance(validHidingPlaces[i].position, jugador.transform.position);
           
            {

            }
        }
    }

    void CheckHidingPlaces()
    {
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

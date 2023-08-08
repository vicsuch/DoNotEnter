using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : MonoBehaviour
{
    //Para que la velocidad de los zombies no sean todas iguales
    //Esto los diferenciara mas y dara mas individualidad
    [SerializeField] float velVarietion = 0.7f; 
    [SerializeField] float angularVelVarietion = 10f; 
    [SerializeField] float accelerationVarietion = 2f; 
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        agent.speed += Random.Range(-velVarietion, velVarietion);
        agent.angularSpeed += Random.Range(-angularVelVarietion, angularVelVarietion);
        agent.acceleration += Random.Range(-accelerationVarietion, accelerationVarietion);
        destination = agent.destination;
    }

    void Update()
    {
        // Update destination if the target moves one unit
        if (Vector3.Distance(destination, target.position) > 1.5f)
        {
            destination = target.position;
            agent.destination = destination;
        }
    }
}
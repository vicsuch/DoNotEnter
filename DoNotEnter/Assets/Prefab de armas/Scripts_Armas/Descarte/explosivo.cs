using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class explosivo : MonoBehaviour
{
    public float explosionForce = 10000f;
    public float explosionRadius = 5f;
    private Vector3 fuerza = new Vector3(5f, 0f, 5f);
    bool isInFloor = true;
    private Rigidbody rb;
    private bool thrown = false;
    private bool wasKinematic = false;
    private Transform originalParent; // Almacena el padre original
    private NavMeshAgent[] cachedNavMeshAgents; // Almacena los NavMeshAgents a reactivar

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        wasKinematic = rb.isKinematic;
        originalParent = transform.parent; // Almacenar el padre original al inicio

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !thrown)
        {
            Throw();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown)
        {
            Invoke("Explode", 1f); // Llama a Explode() después de 1 segundo
        }
    }
    private void Throw()
    {
        /* rb.isKinematic = false;
         rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
         thrown = true;*/

        rb.isKinematic = false;
        rb.velocity = transform.forward * 10f; // Usamos velocity para mover objetos kinemáticos
        thrown = true;

        transform.SetParent(null);  
    }
    private void Explode()
    {
        {
            /* Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

             foreach (Collider collider in hitColliders)
             {
                 Rigidbody enemyRigidbody = collider.GetComponent<Rigidbody>();
                 NavMeshAgent navMeshAgent = collider.GetComponent<NavMeshAgent>();

                 if (enemyRigidbody != null)
                 {
                     if (enemyRigidbody.isKinematic)
                     {
                         enemyRigidbody.isKinematic = false;
                     }
                     Vector3 explosionDirection = (enemyRigidbody.transform.position - transform.position).normalized;
                     enemyRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
                     if (!enemyRigidbody.isKinematic)
                     {
                         enemyRigidbody.isKinematic = true;
                         Debug.Log("uvhud");
                     }
                     if (navMeshAgent != null)
                     {
                         navMeshAgent.enabled = false;

                         Invoke("ReactivateNavMeshAgent", 2f);


                     }
                 }

             }*/
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody enemyRigidbody = collider.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                if (enemyRigidbody.isKinematic)
                {
                    enemyRigidbody.isKinematic = false;
                }

                Vector3 explosionDirection = (enemyRigidbody.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

                if (enemyRigidbody.isKinematic && wasKinematic)
                {
                    enemyRigidbody.isKinematic = true;
                }

                // Acceder al componente NavMeshAgent del enemigo y desactivarlo
                NavMeshAgent navMeshAgent = collider.GetComponent<NavMeshAgent>();
               /* if (navMeshAgent != null)
                {
                    navMeshAgent.enabled = false;
                    CacheNavMeshAgent(navMeshAgent);
                }*/
            }
        }

        Destroy(gameObject);

    }
    /* private System.Collections.IEnumerator ReactivateNavMeshAgent(NavMeshAgent agent)
     {

         yield return new WaitForSeconds(0.4f); // Esperar 2 segundos
         Debug.Log("bd");
         agent.enabled = true; // Volver a activar el NavMeshAgent
     }*/
    private void ReactivateNavMeshAgent()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }
    }
    private void CacheNavMeshAgent(NavMeshAgent agent)
    {
        if (cachedNavMeshAgents == null)
        {
            cachedNavMeshAgents = new NavMeshAgent[0];
        }

        // Expandir el arreglo para almacenar el nuevo NavMeshAgent
        NavMeshAgent[] newCachedAgents = new NavMeshAgent[cachedNavMeshAgents.Length + 1];
        for (int i = 0; i < cachedNavMeshAgents.Length; i++)
        {
            newCachedAgents[i] = cachedNavMeshAgents[i];
        }

        newCachedAgents[cachedNavMeshAgents.Length] = agent;
        cachedNavMeshAgents = newCachedAgents;

        Invoke("ReactivateCachedNavMeshAgents", 2f);
    }

    private void ReactivateCachedNavMeshAgents()
    {
        if (cachedNavMeshAgents != null)
        {
            foreach (NavMeshAgent agent in cachedNavMeshAgents)
            {
                if (agent != null)
                {
                    agent.enabled = true;
                }
            }
        }
    }
}

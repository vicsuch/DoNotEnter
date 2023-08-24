using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosivo : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
   

    private Rigidbody rb;
    private bool thrown = false;
    // Start is called before the first frame update
    void Start()
    {
        
            rb = GetComponent<Rigidbody>();
        
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
        if (thrown )
        {
            Invoke("Explode", 1f); // Llama a Explode() después de 1 segundo
        }
    }
    private void Throw()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        thrown = true;
    }
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody enemyRigidbody = collider.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 explosionDirection = (enemyRigidbody.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

}

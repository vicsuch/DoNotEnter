using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivoNuevo : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    private Rigidbody rb;
    private bool thrown = false;
    private Transform originalParent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
    }

    private void Update()
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
            Explode();
        }
    }

    private void Throw()
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward * 10f;
        thrown = true;
        transform.SetParent(null);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        rb.useGravity = true;
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody objectRigidbody = collider.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                objectRigidbody.isKinematic = false;

                Vector3 explosionDirection = (objectRigidbody.transform.position - transform.position).normalized;
                objectRigidbody.AddForce(explosionDirection * explosionForce, ForceMode.Impulse);

                  StartCoroutine(EnableKinematicAfterDelay(objectRigidbody));
                
            }
        }
        StartCoroutine(Destruir(gameObject));

       
    }
    private IEnumerator Destruir (GameObject explosivo)
    {
        yield return new WaitForSeconds(1.2f); // Adjust the delay as needed
        Destroy(gameObject);
    }

    private IEnumerator EnableKinematicAfterDelay(Rigidbody rb)
    {
        Debug.Log("Empieza espera");
       
   
        yield return new WaitForSeconds(1.2f); // Adjust the delay as needed
        Debug.Log("se Espero");
        rb.isKinematic = true;

    }
}

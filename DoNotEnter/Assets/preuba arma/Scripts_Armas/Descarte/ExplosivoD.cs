using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivoD : MonoBehaviour
{
    public float explosionRadius = 4f;
    public int damageAmount = 100;
    private bool thrown = false;
    private Rigidbody rb;
    private Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !thrown)
        {
            Throw();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (thrown)
        {
            Explode();
        }
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider);
            
                vidaenemigo vidaEnemigo = collider.GetComponent<vidaenemigo>();
                if (vidaEnemigo != null)
                {
                    vidaEnemigo.RestarVida(damageAmount);
                }
            
        }

        StartCoroutine(Destruir(gameObject));
    }
    private IEnumerator Destruir(GameObject explosivo)
    {
        yield return new WaitForSeconds(4f); // Adjust the delay as needed
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulasGranada : MonoBehaviour

{
    public ParticleSystem particleSystem;
    public int damageAmount = 15;  // La cantidad de daño a restar a la vida del enemigo.
    private Rigidbody rb;
    private bool thrown = false;
    private Transform originalParent;
    private bool particlesActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("jhaid");
        // Verificar si el objeto colisionado es un enemigo (puedes ajustar esto según tus tags o componentes).
        vidaenemigo enemy = other.GetComponent<vidaenemigo>();
        if (enemy != null)
        {
            // Restar vida al enemigo.
            enemy.RestarVida(damageAmount);
        }
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
        particlesActive = true;
        particleSystem.Play();
        StartCoroutine(StopParticlesAfterDuration());

        StartCoroutine(Destruir(gameObject));


    }
    private IEnumerator StopParticlesAfterDuration()
    {
        yield return new WaitForSeconds(5f  );
        particlesActive = false;
        particleSystem.Stop();
    }
    private IEnumerator Destruir(GameObject explosivo)
    {
        yield return new WaitForSeconds(10.2f); // Adjust the delay as needed
        Destroy(gameObject);
    }
}

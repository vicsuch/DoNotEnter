using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class ParticulasGranada : MonoBehaviour

{
    public ParticleSystem particleSystem;
    public int damageAmount = 15;  // La cantidad de daño a restar a la vida del enemigo.
    private Rigidbody rb;
    private bool thrown = false;
    private Transform originalParent;
    private bool particlesActive = false;
    private ItemData itemData;
    public bool unaVez=true;
    AudioSource audiolas;

    // Start is called before the first frame update
    void Start()
    {
        audiolas = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        itemData = GetComponent<ItemData>();
        originalParent = transform.parent;
    }
    private void OnParticleCollision(GameObject other)
    {
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
        if (CrossPlatformInputManager.GetButtonDown("Disparar") && !thrown && itemData.isGrabbed)
        {
            Throw();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (thrown&&unaVez)
        {
            Debug.Log("entra una ves");
            StartCoroutine(explocion());
            unaVez = false;
            
        }
    }
    private void Throw()
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward * 18f;
        thrown = true;
        transform.SetParent(null);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        rb.useGravity = true;
    }

    private IEnumerator explocion()
    {
        Debug.Log("activacion");
        yield return new WaitForSeconds(1f);
        particlesActive = true;
        particleSystem.Play();
        audiolas.Play();
        StartCoroutine(Destruir(gameObject));

    }

    private void Explode()
    {
        particlesActive = true;
        particleSystem.Play();
       // StartCoroutine(StopParticlesAfterDuration());

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
        yield return new WaitForSeconds(5.2f); // Adjust the delay as needed
        Destroy(gameObject);
    }
}

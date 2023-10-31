using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuñecoAtack : MonoBehaviour
{
    public float velocidad;
    [SerializeField] Transform jugador;
    [SerializeField] Transform mano;
    [SerializeField] Transform proyectil;
    [SerializeField] float atackCoolDownInSeconds;
    [SerializeField] bool seeingPlayer = false;
    [SerializeField] float maxViewingDistance;
    [SerializeField] LayerMask raycastLayerNotIgnore;
    bool coolDownOver = true;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void matarmunie()
    {
        anim.SetBool("muerto", true);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("velocidad: " + velocidad);
        anim.SetFloat("velocida", velocidad);

        IsSeeingPlayer();
        Debug.Log(seeingPlayer);
        if(seeingPlayer)
        {
            Atack();
        }
    }

    void Atack()
    {
        if (!coolDownOver) { return; }
        anim.SetBool("atac", true);
        Invoke("spawnproyectil", 2.1f);
        coolDownOver = false;
    }
    void spawnproyectil()
    {
        
        Transform proyectilActual = Instantiate(proyectil, mano.position, Quaternion.identity);
        proyectilActual.GetComponent<ProyectoMuñeco>().objetivo = jugador.position;
    
        Invoke("CoolDownCountDown", atackCoolDownInSeconds);
        Invoke("terminaranimacion", 1.9f);
    }
    
    void terminaranimacion()
    {
        anim.SetBool("atac", false);
    }

    void CoolDownCountDown()
    {
        coolDownOver = true;
    }

    void IsSeeingPlayer()
    {
        bool hasHit = Physics.Raycast(transform.position, jugador.position - transform.position, out RaycastHit hit, maxViewingDistance, raycastLayerNotIgnore, QueryTriggerInteraction.Ignore);
        if (!hasHit || hit.transform != jugador.transform) { seeingPlayer = false; return; }
        seeingPlayer = true;
    }
}

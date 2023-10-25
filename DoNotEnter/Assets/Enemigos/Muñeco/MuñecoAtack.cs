using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuñecoAtack : MonoBehaviour
{
    [SerializeField] Transform jugador;
    [SerializeField] Transform mano;
    [SerializeField] Transform proyectil;
    [SerializeField] float atackCoolDownInSeconds;
    [SerializeField] bool seeingPlayer = false;
    [SerializeField] float maxViewingDistance;
    [SerializeField] LayerMask raycastLayerNotIgnore;
    bool coolDownOver = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Transform proyectilActual = Instantiate(proyectil, mano.position, Quaternion.identity);
        proyectilActual.GetComponent<ProyectoMuñeco>().objetivo = jugador.position;
        coolDownOver = false;
        Invoke("CoolDownCountDown", atackCoolDownInSeconds);
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

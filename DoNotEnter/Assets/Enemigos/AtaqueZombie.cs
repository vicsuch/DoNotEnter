using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueZombie : MonoBehaviour
{
    [SerializeField] private float knockBack = 10f;
    Transform transformAtacar;
    bool atackStarted;
    private float atackTimer;
    // Start is called before the first frame update
    void Start()
    {
        atackTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Atack()
    {
        if (atackStarted)
        {
            return;
        }
        else
        {
            Debug.Log("asda");
            atackStarted = true;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController a = transformAtacar.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            a.m_MoveDir += transform.forward * knockBack + transform.up * knockBack * 0.3f;
        }
        atackStarted = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        SaludJugador salud = other.GetComponent<SaludJugador>();
        if(salud != null)
        {
            transformAtacar = other.transform;
            Atack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueZombie : MonoBehaviour
{
    [SerializeField] private float knockBack = 10f;
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

    private void OnTriggerEnter(Collider other)
    {
        Saludjugador salud = other.GetComponent<Saludjugador>();
        if(salud != null)
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            salud.vida -= 10;
        }
    }
}

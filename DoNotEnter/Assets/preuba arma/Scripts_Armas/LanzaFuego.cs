using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class LanzaFuego : MonoBehaviour
{

    public int damageMin = 25;
    public int damageMax = 40;
    public int maxRange = 10;
    public int coneAngle = 30; // Ángulo del cono de fuego

    public Transform flameSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar")) // cambiar a click
        {
            ShootFlame();
        }
    }
    private void ShootFlame()
    {
        Collider[] hitColliders = Physics.OverlapSphere(flameSpawnPoint.position, maxRange);

        foreach (Collider collider in hitColliders)
        {
            Vector3 directionToCollider = collider.transform.position - flameSpawnPoint.position;
            float angleToCollider = Vector3.Angle(directionToCollider, flameSpawnPoint.forward);

            if (angleToCollider <= coneAngle * 0.5f)
            {
                vidaenemigo vidaEnemigoScript = collider.GetComponent<vidaenemigo>();
                if (vidaEnemigoScript != null)
                {
                    int damage = Random.Range(damageMin, damageMax);
                    vidaEnemigoScript.RestarVida(damage);
                }
            }
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class rayoLaserViejo : MonoBehaviour
{
    public int laserDamageMin = 40;
    public int laserDamageMax = 55;

    //   private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar"))
        {
            ShootLaser();
        }
    }
    void ShootLaser()
    {


        Debug.Log("Funcion funiona");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);

        //   lineRenderer.positionCount = hits.Length * 2;

        for (int i = 0; i < hits.Length; i++)
        {
            // lineRenderer.SetPosition(i * 2, ray.origin);
            // lineRenderer.SetPosition(i * 2 + 1, hits[i].point);


            vidaenemigo vidaZombieScript = hits[i].collider.GetComponent<vidaenemigo>();
            Debug.Log(vidaZombieScript);
            if (vidaZombieScript != null)
            {
                int damage = Random.Range(laserDamageMin, laserDamageMax);
                vidaZombieScript.RestarVida(damage);
            }
        }
    }
}


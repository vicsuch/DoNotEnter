using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class LanzaFuego : MonoBehaviour
{
    [SerializeField] private ParticleSystem fuego;
    [SerializeField] private float duracionDeDisparo;

    private ItemData itemData;

    // Start is called before the first frame update
    void Start()
    {
        itemData = GetComponent<ItemData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ItemData>().balasRestantes>0&&CrossPlatformInputManager.GetButton("Disparar") && itemData.isGrabbed && itemData.balasRestantes > 0 && !fuego.isEmitting) // cambiar a click
        {
            ShootFlame();
            gameObject.GetComponent<ItemData>().balasRestantes--;
        }
    }
    private void ShootFlame()
    {
        fuego.Play();
        Invoke("StopFlame", duracionDeDisparo);
    }
    private void StopFlame()
    {
        fuego.Stop();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class pocion : MonoBehaviour
{
    public SaludJugador playerscript;
    private ItemData itemData;
    // Start is called before the first frame update
    void Start()
    {
        itemData = transform.GetComponent<ItemData>();
        playerscript = FindObjectOfType<SaludJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar") && itemData.isGrabbed) // cambiar a click
        {
            playerscript.pocion();
            Destroy(gameObject);
        }
    }
}

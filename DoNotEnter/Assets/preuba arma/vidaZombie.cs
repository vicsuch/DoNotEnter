using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vidaZombie : MonoBehaviour
{
    public int vida_zombie = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vida_zombie < 1)
        {
            Destroy(gameObject);
        }
    }
}

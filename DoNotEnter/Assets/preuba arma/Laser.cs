using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o")) // Cambia "Fire1" a la entrada que uses para disparar
        {
            DispararRayoLaser();
        }
    }
    void DispararRayoLaser()
    {
        Ray rayo = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(rayo, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
        {
            GameObject enemigo = hit.collider.gameObject;
            vidaZombie vidaZombie = enemigo.GetComponent<vidaZombie>();
            Debug.Log("Enemigo detectado: " + enemigo.name);
            int puntuacion = Random.Range(40, 70);
            vidaZombie.vida_zombie -= puntuacion;

            // Aquí puedes agregar lógica adicional, como daño al enemigo o efectos visuales.
        }
    }
}

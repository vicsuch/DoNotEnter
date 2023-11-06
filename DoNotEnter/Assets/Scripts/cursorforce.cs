using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorforce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Establece el estado del cursor como visible y desbloqueado
        Cursor.lockState = CursorLockMode.None;
        // Hacer que el cursor sea visible
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

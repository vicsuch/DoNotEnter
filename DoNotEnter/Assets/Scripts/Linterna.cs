using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] Light linterna;
    [SerializeField] Transform sol;
    // Update is called once per frame
    void Update()
    {
        linterna.enabled = sol.forward.y > 0f;
        Debug.Log(sol.forward.y);
    }
}

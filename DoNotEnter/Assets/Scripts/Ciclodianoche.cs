using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciclodianoche : MonoBehaviour
{
    public float rotationscale = 10;
    public bool dia=true;
    public float rotacionxactual;
    public Light luz;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        rotacionxactual = transform.rotation.eulerAngles.x;
        transform.Rotate(rotationscale * Time.deltaTime, 0, 0);
        //RenderSettings.skybox.SetFloat("_Exposure", -luz.transform.forward.y * 0.5f + 0.5f);
    }
}

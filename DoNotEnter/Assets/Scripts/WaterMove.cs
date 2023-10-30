using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    [SerializeField] float movementDistance;
    [SerializeField] float speed;
    [SerializeField] Material water;
    [SerializeField] float waterSpeed;
    [SerializeField] Texture2D[] normal;
    Vector3 initialPos;
    float timeInSeconds;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        water.EnableKeyword("_NORMALMAP");
    }

    // Update is called once per frame
    void Update()
    {
        timeInSeconds = Time.realtimeSinceStartup;
        water.SetTexture("_BumpMap", normal[((int)(timeInSeconds * waterSpeed))%normal.Length]);
        
        transform.position = new Vector3(0f, Mathf.Sin(timeInSeconds * speed) * movementDistance, 0f) + initialPos;
    }
}

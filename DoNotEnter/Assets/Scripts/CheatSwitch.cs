using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CheatSwitch : MonoBehaviour
{
    [SerializeField] Transform lever;
    [SerializeField] float leverAngle = 45f;
    [SerializeField] float minDistance = 5f;
    [SerializeField] float lerpTimeMultiplier = 1f;
    [SerializeField] bool isOn = false;
    float time = 0f;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lever.rotation = Quaternion.Euler(leverAngle, 0f, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) < minDistance && CrossPlatformInputManager.GetButtonDown("CheatsOn"))
        { isOn = !isOn; }
        if (isOn && time > 0f) { time -= Time.deltaTime * lerpTimeMultiplier; }
        else if (!isOn && time < 1f) { time += Time.deltaTime * lerpTimeMultiplier; }
        lever.localRotation = Quaternion.Euler(Mathf.Lerp(-leverAngle, leverAngle, time), 0f, -90f) ;
        if(isOn && CrossPlatformInputManager.GetButtonDown("KillZombies"))
        {
            ZombieController[] zombies = GameObject.FindObjectsOfType<ZombieController>();
            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].GetComponent<vidaenemigo>().RestarVida(1000);
            }
        }
        if (isOn && CrossPlatformInputManager.GetButtonDown("KillCoco"))
        {
            ElCocoController[] zombies = GameObject.FindObjectsOfType<ElCocoController>();
            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].GetComponent<vidaenemigo>().RestarVida(1000);
            }
        }
        if (isOn && CrossPlatformInputManager.GetButtonDown("KillMuñeco"))
        {
            MuñecoController[] zombies = GameObject.FindObjectsOfType<MuñecoController>();
            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].GetComponent<vidaenemigo>().RestarVida(1000);
            }
        }
    }
}

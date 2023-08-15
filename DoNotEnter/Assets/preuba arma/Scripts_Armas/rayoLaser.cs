using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class rayoLaser : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private int damage;
    [SerializeField] private int damageVarietion;
    [SerializeField] private float laserPersistanceInSeconds;
    [SerializeField] private float coolDownInSeconds;

    private bool IsCoolDownOver = true;
   
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Disparar") && IsCoolDownOver)
        {
            ShootLaser();
        }
    }
    void ShootLaser()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        lineRenderer.SetPosition(0, transform.position);
        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, Vector3.Lerp(transform.position, transform.forward * maxDistance + transform.position, (i + 1) / lineRenderer.positionCount));
        }
        for (int i = 0; i < hits.Length; i++)
        {
            vidaenemigo vidaZombieScript = hits[i].collider.GetComponent<vidaenemigo>();
            if (vidaZombieScript != null)
            {
                vidaZombieScript.RestarVida(Random.Range(damage - damageVarietion, damage + damageVarietion));
            }
        }
        lineRenderer.enabled = true;
        IsCoolDownOver = false;
        Invoke("SetCoolDownOver", coolDownInSeconds);
        Invoke("DesaparecerLinea", laserPersistanceInSeconds);
    }
    void DesaparecerLinea()
    {
        lineRenderer.enabled = false;
    }
    void SetCoolDownOver()
    {
        IsCoolDownOver = true;
    }
}

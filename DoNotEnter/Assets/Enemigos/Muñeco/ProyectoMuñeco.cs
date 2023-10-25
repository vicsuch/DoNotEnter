using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectoMuñeco : MonoBehaviour
{
    public Vector3 objetivo;
    [SerializeField] float speed;
    [SerializeField] float speedRandomAdd;
    [SerializeField] float deleteTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = objetivo - transform.position;
        dir.Normalize();
        Vector3 obj = objetivo;
        obj.y = 0;
        Vector3 tr = transform.position;
        tr.y = 0;
        float finishTime = Vector3.Distance(obj, tr)/speed;
        float startPos = transform.position.y;
        float finishPos = objetivo.y;
        float gravity = Physics.gravity.y;
        // Ecuacion: P(t) = startPos + startVelocity * t + 0.5 * gravity * t*t
        // finishPos = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime
        // 0 = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity * -finishTime = startPos + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity = (startPos + 0.5 * gravity * finishTime * finishTime - finishPos) / -finishTime
        float startVelocity = (startPos + (0.5f * gravity * finishTime * finishTime) - finishPos) / (-1f * finishTime);
        Debug.Log(startVelocity + " g: " + gravity + " finishT: " + finishTime);
        transform.GetComponent<Rigidbody>().velocity = dir * speed + Vector3.up * startVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Delete()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("Delete", deleteTime);
        SaludJugador salud = collision.gameObject.GetComponent<SaludJugador>();
        if(salud)
        {
            salud.AtaqueProyectilMuñeco();
        }
    }
}

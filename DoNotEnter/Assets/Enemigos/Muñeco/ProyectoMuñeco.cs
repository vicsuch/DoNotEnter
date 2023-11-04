using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectoMuñeco : MonoBehaviour
{
    public Vector3 objetivo;
    [SerializeField] float speed;
    [SerializeField] float speedRandomAdd;
    [SerializeField] float deleteTime = 5f;
    [SerializeField] float multiGrav = 1f;
    Rigidbody rb;
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
        float gravity = Physics.gravity.y * multiGrav;
        // Ecuacion: P(t) = startPos + startVelocity * t + 0.5 * gravity * t*t
        // finishPos = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime
        // 0 = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity * -finishTime = startPos + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity = (startPos + 0.5 * gravity * finishTime * finishTime - finishPos) / -finishTime
        float startVelocity = (startPos + (0.5f * gravity * finishTime * finishTime) - finishPos) / (-1f * finishTime);
        Debug.Log(startVelocity + " g: " + gravity + " finishT: " + finishTime);
        transform.GetComponent<Rigidbody>().velocity = dir * speed + Vector3.up * startVelocity;
        
        Dibujar(startPos, startVelocity, gravity, finishTime);
        rb = GetComponent<Rigidbody>();
        Debug.Log(rb.velocity - (dir * speed + Vector3.up * startVelocity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Dibujar(float startPos, float startVelocity, float gravity, float finishTime)
    {
        for(int i = 0; i < 10; i++)
        {
            Vector3 pos = Vector3.Lerp(transform.position, objetivo, i * 0.1f);
            Vector3 pos2 = Vector3.Lerp(transform.position, objetivo, (i + 1) * 0.1f);
            pos.y = Funcion(startPos, startVelocity, gravity, finishTime * i * 0.1f);
            pos2.y = Funcion(startPos, startVelocity, gravity, finishTime * (i + 1) * 0.1f);
            Debug.DrawLine(pos, pos2, Color.red, 10f);
        }
    }
    float Funcion(float startPos, float startVelocity, float gravity, float time)
    {
        return startPos + startVelocity * time + 0.5f * gravity * time * time;
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

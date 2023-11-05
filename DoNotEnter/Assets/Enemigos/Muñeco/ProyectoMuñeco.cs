using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectoMuñeco : MonoBehaviour
{
    public Vector3 objetivo;
    Vector3 start;
    float speed;
    [SerializeField] float speedRandomAdd;
    [SerializeField] float deleteTime = 5f;
    [SerializeField] float multiGrav = 1f;
    [SerializeField] ParticleSystem p;
    float time = 0f;
    float startPos;
    float finishPos;
    float gravity;
    float startVelocity;
    [SerializeField] float finishTime = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        rb = GetComponent<Rigidbody>();
        p = GetComponent<ParticleSystem>();
        Vector3 dir = objetivo - transform.position;
        dir.Normalize();
        Vector3 obj = objetivo;
        obj.y = 0;
        Vector3 tr = transform.position;
        tr.y = 0;
        speed = Vector3.Distance(transform.position, obj) / finishTime;
        startPos = transform.position.y;
        finishPos = objetivo.y;
        gravity = Physics.gravity.y * multiGrav;
        // Ecuacion: P(t) = startPos + startVelocity * t + 0.5 * gravity * t*t
        // finishPos = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime
        // 0 = startPos + startVelocity * finishTime + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity * -finishTime = startPos + 0.5 * gravity * finishTime * finishTime - finishPos
        // startVelocity = (startPos + 0.5 * gravity * finishTime * finishTime - finishPos) / -finishTime
        startVelocity = (startPos + (0.5f * gravity * finishTime * finishTime) - finishPos) / (-1f * finishTime);
       // Debug.Log("startVel: " + startPos + " gravity: " + startPos + " finishTime: " + finishTime + " finishPos: " + finishPos);
        
        Dibujar(startPos, startVelocity, gravity, finishTime);
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        time += Time.deltaTime;
    }
    void Mover()
    {
        Vector3 pos = Vector3.Lerp(start, objetivo, time * finishTime);
        pos.y = Funcion(startPos, startVelocity, gravity, finishTime * time);
        transform.position = pos;
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
        SaludJugador salud = collision.gameObject.GetComponent<SaludJugador>();
        if(salud)
        {
            salud.AtaqueProyectilMuñeco();
        }
        p.Emit(111);
        Collider[] all = GetComponents<Collider>();
        for(int i = 0; i < all.Length; i++)
        {
            all[i].enabled = false;
        }
        MeshRenderer[] all2 = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < all2.Length; i++)
        {
            all2[i].enabled = false;
        }
        Invoke("Delete", p.main.startLifetimeMultiplier);
    }

}

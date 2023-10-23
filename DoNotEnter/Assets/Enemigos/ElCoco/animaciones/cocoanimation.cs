using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cocoanimation : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void asignarnav(NavMeshAgent agen)
    {
        agent = agen;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("velocidad", agent.velocity.magnitude);
    }
}

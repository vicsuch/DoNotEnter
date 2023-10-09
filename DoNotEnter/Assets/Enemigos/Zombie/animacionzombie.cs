using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animacionzombie : MonoBehaviour
{
    public Animator anim;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       anim= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void asignarnav(NavMeshAgent agen)
    {
        agent = agen;

    }
    void Update()
    {
        anim.SetFloat("velocidad", agent.velocity.magnitude);
    }
    public void animationpegar()
    {
        Debug.Log("llamo");
        anim.SetBool("pego", true);
        Invoke("sacarpego", 3);
        
    }
    void sacarpego()
    {
        Debug.Log("funka el invoque");
        anim.SetBool("pego", false);
    }
    public void muerto()
    {
        anim.SetBool("0vida",true);
    }
}

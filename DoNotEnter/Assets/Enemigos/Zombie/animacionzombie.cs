using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacionzombie : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       anim= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

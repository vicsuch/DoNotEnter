using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aparicionUI : MonoBehaviour
{
    // Start is called before the first frame update
  public   GameObject[] todo;
    void Start()
    {
        
    }
   public void abilita()
    {
        for(int i = 0; i > todo.Length - 1; i++)
        {
            todo[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

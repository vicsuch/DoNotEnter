using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerItemGrabber : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject grabbedItem;
    private bool grabing = false;
    private bool wasRigid = false;
    private GameObject grabbedObject;
    private Transform grabbedLastParent;
    private List<GameObject> itemNearPlayer = new List<GameObject>();
    private List<ItemData> itemNearPlayerData = new List<ItemData>();
    private bool interactKey = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interactKey = CrossPlatformInputManager.GetButton("Interact");
        if (interactKey)
        {
            if(!grabing)
            {
                Debug.Log("Inter");
                int nearestObject = 0;
                if (itemNearPlayerData[nearestObject].grabable)
                {
                    grabing = true;
                    grabbedLastParent = itemNearPlayer[nearestObject].transform.parent;
                    itemNearPlayer[nearestObject].transform.SetParent(hand.transform);
                    itemNearPlayer[nearestObject].transform.position = hand.transform.position;
                    grabbedItem = itemNearPlayer[nearestObject];
                }
            }
            else
            {
                grabing = false;
                grabbedItem.transform.SetParent(grabbedLastParent);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemData data = other.transform.GetComponent<ItemData>();
        if (data != null)
        {
            itemNearPlayer.Add(other.transform.gameObject);
            itemNearPlayerData.Add(data);
        }
        Debug.Log("enter");
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < itemNearPlayer.Count; i++)
        {
            if(other.gameObject == itemNearPlayer[i])
            {
                itemNearPlayer.RemoveAt(i);
                itemNearPlayerData.RemoveAt(i);
                Debug.Log("exit");
            }
        }
    }
}

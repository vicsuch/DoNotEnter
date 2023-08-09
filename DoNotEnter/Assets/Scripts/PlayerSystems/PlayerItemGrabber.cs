using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerItemGrabber : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject grabbedItem;
    [SerializeField] private bool usingGunSlot = true;
    [SerializeField] private int gunNum = 0;
    [SerializeField] private int secondaryNum = 0;
    public GameObject[] gunSlots = new GameObject[3];
    private Transform[] gunLastParent = new Transform[3];
    public GameObject[] secondarySlots = new GameObject[2];
    private Transform[] secondaryLastParent = new Transform[2];
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
            GrabNearestItem();
        }
        GetButtons();
    }
    void GetButtons()
    {
        bool changeToSecondary = CrossPlatformInputManager.GetButtonDown("ChantoToSecondary");
        if(changeToSecondary)
        {
            usingGunSlot = !usingGunSlot;
            if (gunSlots[gunNum] != null)
            {
                gunSlots[gunNum].SetActive(usingGunSlot);
            }
            if (secondarySlots[secondaryNum] != null)
            {
                secondarySlots[secondaryNum].SetActive(usingGunSlot);
            }
        }
        bool[] itemKey = new bool[3];
        for(int i = 0; i < 3; i++)
        {
            itemKey[i] = CrossPlatformInputManager.GetButtonDown("Item" + (i + 1));
        }
        if (usingGunSlot)
        {
            for (int i = 0; i < gunSlots.Length; i++)
            {
                if (itemKey[i])
                {
                    if (gunSlots[gunNum] != null)
                    {
                        gunSlots[gunNum].SetActive(false);
                    }
                    gunNum = i;
                    if (gunSlots[gunNum] != null)
                    {
                        gunSlots[gunNum].SetActive(true);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < secondarySlots.Length; i++)
            {
                if (itemKey[i])
                {
                    if (secondarySlots[secondaryNum] != null)
                    {
                        secondarySlots[secondaryNum].SetActive(false);
                    }
                    secondaryNum = i;
                    if (secondarySlots[secondaryNum] != null)
                    {
                        secondarySlots[secondaryNum].SetActive(true);
                    }
                }
            }
        }
    }
    void GrabNearestItem()
    {
        if (isCurrentItemSlotEmpty() && itemNearPlayerData.Count > 0)
        {
            int nearestObject = 0;
            if (itemNearPlayerData[nearestObject].grabable)
            {
                if (usingGunSlot && itemNearPlayerData[nearestObject].isGun)
                {
                    gunSlots[gunNum] = itemNearPlayer[nearestObject];
                    gunLastParent[gunNum] = itemNearPlayer[nearestObject].transform.parent;
                    GrabingObjectSetup(gunSlots[gunNum]);
                    itemNearPlayer.RemoveAt(nearestObject);
                    itemNearPlayerData.RemoveAt(nearestObject);
                }
                else if(!usingGunSlot && !itemNearPlayerData[nearestObject].isGun)
                {
                    secondarySlots[secondaryNum] = itemNearPlayer[nearestObject];
                    secondaryLastParent[secondaryNum] = itemNearPlayer[nearestObject].transform.parent;
                    GrabingObjectSetup(secondarySlots[secondaryNum]);
                    itemNearPlayer.RemoveAt(nearestObject);
                    itemNearPlayerData.RemoveAt(nearestObject);
                }
            }
        }
        else
        {

        }
    }
    void GrabingObjectSetup(GameObject grabing)
    {
        Rigidbody rb = grabing.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.isKinematic = true;
        }
        Collider[] col = grabing.GetComponents<Collider>();
        for(int i = 0; i < col.Length; i++)
        {
            col[i].enabled = false;
        }
        col = grabing.GetComponentsInChildren<Collider>();
        for (int i = 0; i < col.Length; i++)
        {
            col[i].enabled = false;
        }
        grabing.transform.SetParent(hand.transform);
        grabing.transform.position = hand.transform.position;
        grabing.transform.rotation = hand.transform.rotation;
    }
    bool isCurrentItemSlotEmpty()
    {
        if(usingGunSlot)
        {
            return gunSlots[gunNum] == null;
        }
        else
        {
            return secondarySlots[secondaryNum] == null;
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

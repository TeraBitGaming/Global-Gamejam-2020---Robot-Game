using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private Item theItemData;
    [SerializeField]
    private bool IsBeingHeld;

    public void SetItemData(Item i)
    {
        theItemData = i;
    }

    public Item GetItemData()
    {
        return theItemData;
    }

    public bool GetHolding()
    {
        return IsBeingHeld;
    }
}

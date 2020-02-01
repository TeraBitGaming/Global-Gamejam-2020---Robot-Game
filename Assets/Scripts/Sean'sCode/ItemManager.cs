using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemObject> existItemObjects = new List<ItemObject>();
    [SerializeField]
    private int MaxAllowedItems = 15;

    private void Update()
    {

    }

    public bool AddItem(ItemObject io)
    {
        if (!existItemObjects.Contains(io))
        {
            existItemObjects.Add(io);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveItem(ItemObject io)
    {
        if(existItemObjects.Contains(io))
        {
            existItemObjects.Remove(io);
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool ListIsFull()
    {
        if(existItemObjects.Count >= MaxAllowedItems)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

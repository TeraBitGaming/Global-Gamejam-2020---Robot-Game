﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemObject> existItemObjects = new List<ItemObject>();

    public bool AddItem(ItemObject io)
    {
        if(!existItemObjects.Contains(io))
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
}

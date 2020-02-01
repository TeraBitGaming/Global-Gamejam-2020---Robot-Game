using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemObject> existItemObjects = new List<ItemObject>();
    [SerializeField]
    private int MaxAllowedItems = 15;

    public bool AddItem(ItemObject io)
    {
        KillOvermuchItem();
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

    private void KillOvermuchItem()
    {
        if(existItemObjects.Count >= MaxAllowedItems-1)
        {
            existItemObjects[0].gameObject.GetComponent<Animator>().SetTrigger("Destroy");
        }
    }

    public void Execution(ItemObject io)
    {

        Destroy(io.gameObject);
        existItemObjects.Remove(io);
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

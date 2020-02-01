using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField]
    private int capability = 10;
    [SerializeField]
    private int itemCount = 0;
    [SerializeField]
    private Item itemType;
    private ItemManager im;
    [SerializeField]
    private GameObject inside;
    [SerializeField]
    private GameObject emptyItemPrefab;

    private void Start()
    {
        im = FindObjectOfType<ItemManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ItemObject>())
        {
            if(!collision.GetComponent<ItemObject>().GetHolding())
            {
                if(itemCount < capability)
                {
                    if (itemCount == 0 && itemType == null)
                    {
                        itemType = collision.GetComponent<ItemObject>().GetItemData();
                        itemCount++;
                        if(itemType.GetSprite() != null)
                            inside.GetComponent<SpriteRenderer>().sprite = itemType.GetSprite();
                        im.RemoveItem(collision.GetComponent<ItemObject>());
                        Destroy(collision.gameObject);
                    }
                    else if (itemType == collision.GetComponent<ItemObject>().GetItemData())
                    {
                        itemCount++;
                        im.RemoveItem(collision.GetComponent<ItemObject>());
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }

    private void EmptyCheck()
    {
        if(itemCount == 0)
        {
            itemType = null;
            inside.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    
    public GameObject GetItemFromContainer()
    {
        GameObject itemToReturn;
        //If the player is not carrying things
        if (itemCount > 0)
        {
            itemToReturn = Instantiate(emptyItemPrefab, FindObjectOfType<ItemManager>().transform);
            itemToReturn.GetComponent<ItemObject>().SetItemData(itemType);
            if (itemToReturn.GetComponent<ItemObject>().GetItemData().GetName() != "")
                itemToReturn.name = itemToReturn.GetComponent<ItemObject>().GetItemData().GetName() + itemToReturn.name;
            if (itemToReturn.GetComponent<ItemObject>().GetItemData().GetSprite() != null)
                itemToReturn.GetComponent<SpriteRenderer>().sprite = itemToReturn.GetComponent<ItemObject>().GetItemData().GetSprite();
            im.AddItem(itemToReturn.GetComponent<ItemObject>());
            itemCount--;
            EmptyCheck();
            return itemToReturn;
        }
        else
        {
            return null;
        }
    }
}

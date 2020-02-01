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
    
    private void PickUpItem()
    {
        //If the player is not carrying things
    }
}

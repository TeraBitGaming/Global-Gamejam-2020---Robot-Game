using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemData = new List<Item>();
    [SerializeField]
    private float secondsPerDrop = 30;
    [SerializeField]
    private float counter = 0;
    [SerializeField]
    private GameObject emptyItemPrefab;
    [SerializeField]
    private ItemManager im;
    [SerializeField]
    private bool StopDropping = false;//For testing purpose.

    private void Start()
    {
        im = FindObjectOfType<ItemManager>();
    }

    private bool DropCounter()
    {
        counter += Time.deltaTime;
        if(counter >= secondsPerDrop)
        {
            counter = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DropItem()
    {
        Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), 0);
        var theOne = Instantiate(emptyItemPrefab, (Vector2)transform.position + offset, Quaternion.identity,FindObjectOfType<ItemManager>().transform);
        theOne.GetComponent<ItemObject>().SetItemData(itemData[Random.Range(0, itemData.Count-1)]);
        if (theOne.GetComponent<ItemObject>().GetItemData().GetName() != "")
            theOne.name = theOne.GetComponent<ItemObject>().GetItemData().GetName() + theOne.name;
        if (theOne.GetComponent<ItemObject>().GetItemData().GetSprite() != null)
            theOne.GetComponent<SpriteRenderer>().sprite = theOne.GetComponent<ItemObject>().GetItemData().GetSprite();
        im.AddItem(theOne.GetComponent<ItemObject>());
    }

    private void Update()
    {
        if(DropCounter())
        {
            if (!StopDropping)
            {
                if(!FindObjectOfType<ItemManager>().ListIsFull())
                    DropItem();
            }
        }
    }

}

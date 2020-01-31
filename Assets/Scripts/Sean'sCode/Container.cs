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
    private ItemObject itemType;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ItemObject>())
        {
            if(!collision.GetComponent<ItemObject>().GetHolding())
            {
                if(itemCount == 0 && itemType == null)
                {
                    itemType = collision.GetComponent<ItemObject>();
                    itemCount++;
                    Destroy(collision.gameObject);
                }
                
                if(itemType == collision.GetComponent<ItemObject>())
                {
                    itemCount++;
                    Destroy(collision.gameObject);
                }

            }
        }
    }
}

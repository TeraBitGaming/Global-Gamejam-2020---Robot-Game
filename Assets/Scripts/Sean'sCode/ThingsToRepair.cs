using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsToRepair : MonoBehaviour
{
    [SerializeField]
    private bool multipleItemsRequired;
    [SerializeField]
    private Item[] requiredItems;
    [SerializeField]
    private Item requiredItem;
    [SerializeField]
    private float timeLimit = 10f;
    [SerializeField]
    private float timeCounter = 0;
    [SerializeField]
    private bool Active = false;
    [SerializeField]
    private bool isBroken = false;

    private void limitCounter()
    {
        if(timeCounter < timeLimit)
        {
            timeCounter++;
        }
        else
        {
            isBroken = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController2D>())
        {
            if(!isBroken)
            {
                if(Input.GetKey(KeyCode.E))
                {
                    Active = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController2D>())
        {
            if (!isBroken)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Active = false;
                }
            }
        }
    }

}

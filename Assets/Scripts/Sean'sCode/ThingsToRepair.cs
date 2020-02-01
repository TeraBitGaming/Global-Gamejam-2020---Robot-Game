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
    private float timeLimit;
    [SerializeField]
    private float counter;
    [SerializeField]
    private Vector2 timeBeforeNextRepair;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController2D>())
        {
            if (Input.GetKey(KeyCode.E))
            {

            }
        }
    }

}

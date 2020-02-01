using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private Item theItemData;
    [SerializeField]
    private bool IsBeingHeld;
    [SerializeField]
    private float lifeTime = 10f;
    [SerializeField]
    private float counter = 0;

    public void SetHolding(bool holding)
    {
        IsBeingHeld = holding;
    }

    private void Start()
    {
        Invoke("PlayBlinkingAnim", lifeTime);
    }

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

    public void PlayBlinkingAnim()
    {
        GetComponent<Animator>().SetBool("Dying", true);
    }

    public void SelfDestruction()
    {
        Destroy(gameObject);
        FindObjectOfType<ItemManager>().RemoveItem(this);
    }

    public void GetPickedUp()
    {
        GetComponent<Animator>().SetBool("Dying", false);
        IsBeingHeld = true;
    }

    public void GetThrowedAway()
    {
        Invoke("PlayBlinkingAnim", lifeTime);
        IsBeingHeld = false;
    }
}

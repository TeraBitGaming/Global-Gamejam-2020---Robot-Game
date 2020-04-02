using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialHole : MonoBehaviour
{

    [SerializeField]
    private List<Item> requiredItems = new List<Item>();

    public void loadScene(int sceneSelect){
        SceneManager.LoadScene(sceneSelect);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ItemObject>())
        {
            if (requiredItems.Contains(collision.GetComponent<ItemObject>().GetItemData()))
            {
                requiredItems.Remove(collision.GetComponent<ItemObject>().GetItemData());
            }

            if (requiredItems.Count <= 0)   
            {
                loadScene(2);
            }
        }
    }
}

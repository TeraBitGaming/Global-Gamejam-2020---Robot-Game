using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsToRepair : MonoBehaviour
{
    [SerializeField]
    private int stage = 0;// stage1 = start breaking, and 5 is broken totally.
    [SerializeField]
    private Sprite[] stageSprites = new Sprite[5];
    [SerializeField]
    private float stageCounter = 0;
    [SerializeField]
    private float secondsPerStage;
    [SerializeField]
    private float spawningChance;
    [SerializeField]
    private float RandomNum;
    [SerializeField]
    private Vector2 timeBetween = new Vector2(10, 30);
    [SerializeField]
    private List<Item> requiredItems = new List<Item>();

    private void Start()
    {
        requiredItems = FindObjectOfType<DifficultyController>().GenerateRequiredItems();
        StartCoroutine("SpawningMobs");
    }


    private void stageCounting()
    {
        if (stage < 5)
        {
            if (stageCounter < secondsPerStage)
            {
                stageCounter += Time.deltaTime;
            }
            else
            {
                stageCounter = 0;
                stage++;
                RefreshStage();
            }
        }
    }

    IEnumerator SpawningMobs()
    {
        if(stage < 5)
        {

        }
        else
        {
            RandomNum = UnityEngine.Random.Range(0f, 1f);
            if(RandomNum <= spawningChance)
            {
                //Spawning Mob
                print("Mob spawned, chance was " + RandomNum);
            }
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(SpawningMobs());

    }

    private void RefreshStage()
    {
        GetComponent<SpriteRenderer>().sprite = stageSprites[stage - 1];
    }

    private void Update()
    {
        secondsPerStage = FindObjectOfType<DifficultyController>().GetSecPerStage();
        spawningChance = FindObjectOfType<DifficultyController>().GetChance();
        stageCounting();
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
                Destroy(this.gameObject);
            }
        }
    }
}

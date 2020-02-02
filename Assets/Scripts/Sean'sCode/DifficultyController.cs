using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField]
    private float secondsPerStageBase = 10;
    [SerializeField]
    private float secondsPerStage;
    [SerializeField]
    private float chanceOfSpawningMobsBase = 0.25f;
    [SerializeField]
    private float chanceOfSpawningMobs;

    [SerializeField]
    private float chancePara = 0.1f;
    [SerializeField]
    private float difficulty = 1;
    [SerializeField]
    private List<Item> itemList;

    private void Start()
    {
        secondsPerStage = secondsPerStageBase;
        chanceOfSpawningMobs = chanceOfSpawningMobsBase;
    }

    public List<Item> GenerateRequiredItems()
    {
        int countOfItems = (int)Mathf.Round(difficulty);
        List<Item> requiredItems = new List<Item>();
        for (int i = 0; i < countOfItems; i++)
        {
            requiredItems.Add(itemList[UnityEngine.Random.Range(0, itemList.Count)]);
        }
        return requiredItems;
    }

    private void DifficultyAdder()
    {
        difficulty += Time.deltaTime / 60;

        if (secondsPerStage >= 4)
        {
            secondsPerStage = secondsPerStageBase - 2 * (difficulty - 1);
        }

        if (chanceOfSpawningMobs <= 0.85f)
        {
            chanceOfSpawningMobs = chanceOfSpawningMobsBase + chancePara * (difficulty - 1);
        }
    }

    private void Update()
    {
        DifficultyAdder();
    }

    public float GetCurrentDiff()
    { return difficulty; }

    public float GetSecPerStage()
    { return secondsPerStage; }

    public float GetChance()
    { return chanceOfSpawningMobs; }


}

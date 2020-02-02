using System;
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
    private float difficulty = 1;
    [SerializeField]
    private List<Item> itemList;
    [SerializeField]
    private Vector2 secBetween = new Vector2(5, 60);
    [SerializeField]
    private List<Transform> spots = new List<Transform>();
    [SerializeField]
    private GameObject prefab;

 
    private void Start()
    {
        secondsPerStage = secondsPerStageBase;
        chanceOfSpawningMobs = chanceOfSpawningMobsBase;
        InitSpots();
    }

    private void InitSpots()
    {
        foreach(Transform t in spots)
        {
            StartCoroutine(NewWallRequest(t));
        }
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
            chanceOfSpawningMobs = chanceOfSpawningMobsBase + 0.2f * (difficulty - 1);
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

    public void SpawnNewWall(Transform t)
    {
        Instantiate(prefab, t.position, Quaternion.identity);
        prefab.GetComponent<ThingsToRepair>().SetCounterTo(-UnityEngine.Random.Range(secBetween.x, secBetween.y));
    }

    public IEnumerator NewWallRequest(Transform t)
    {
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(secBetween.x, secBetween.y));
        Instantiate(prefab, t.position, Quaternion.identity);
        print("1");
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    [SerializeField]
    private float secPerSpawningTry = 5;

    // Wesley's additions
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Transform player;

    private GameObject obj;

    private AudioManager audio;

    private ScoreCounter score;

    private void Start()
    {
        player = FindObjectOfType<PlayerController2D>().transform;
        //requiredItems = FindObjectOfType<DifficultyController>().GenerateRequiredItems();
        StartCoroutine("SpawningMobs");

        // get the audiomanager script.
        audio = GameObject.FindWithTag("SoundManager").GetComponent<AudioManager>();
        score = GameObject.FindWithTag("ScoreCounter").GetComponent<ScoreCounter>();
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
                if (stage == 5){
                    audio.playSoundClipOneShot(6);
                } else  {
                    audio.playSoundClipOneShot(7);
                }
                
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
                // print("Mob spawned, chance was " + RandomNum);
                obj = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
                obj.GetComponent<AIDestinationSetter>().target = player;
            }
        }

        yield return new WaitForSeconds(secPerSpawningTry);

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
                score.addToScore(1);
                collision.GetComponent<ItemObject>().SelfDestruction();
                FindObjectOfType<DifficultyController>().SpawnNewWall(transform);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetCounterTo(float f)
    {
        stageCounter = f;
    }
}

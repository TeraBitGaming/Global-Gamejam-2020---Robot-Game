using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour
{

    private int fixedWalls;
    private string protectedFixedWalls = "0";

    private int secondsSurvived;
    private string protectedSecondsSurvived = "0";

    private int enemiesKilled;
    private string protectedEnemiesKilled = "0";

    private int score;
    private int scoreCheck;
    private string protectedScore = "0";


    // Public method to add score to the game; 
    // We don't add score directly to prevent cheating!
    // Instead, we add to a growing list of "things" we got score for; holes patched, seconds survived, etc.
    public void addToScore(int scoreType){

        // This is where we decide what type of score we should add:
        // 1 is for walls.
        // 2 is for time.
        // 3 is for enemies killed (not implemented).

        switch(scoreType){
            case 1:
                {
                    fixedWalls++;
                    encrypt(protectedFixedWalls);
                    break;
                }
            case 2:
                {
                    secondsSurvived++;
                    encrypt(protectedSecondsSurvived);

                    break;
                }
            case 3:
                {   // Not currently implemented.
                    enemiesKilled++;
                    encrypt(protectedEnemiesKilled);
                    break;
                }
        }
    }

    // Initial score counter
    private void calculateScore(){
        score = (10 * fixedWalls) + secondsSurvived + (20 * enemiesKilled);
        // Debug.Log(score);
    }

    // Cheat Detection!
    private void reCalculateScore(){
        scoreCheck = 10 * fixedWalls + secondsSurvived + 20 * enemiesKilled;
        if(score == scoreCheck){
            Debug.Log(score);
        }
    }

    private string encrypt(string startingValue){
        
        int middle = Convert.ToInt32(startingValue);
        middle += 1;
        startingValue = middle.ToString();

        return startingValue;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        calculateScore();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    [SerializeField]
    private AIPath aiPath;


    // Update is called once per frame
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01){
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}

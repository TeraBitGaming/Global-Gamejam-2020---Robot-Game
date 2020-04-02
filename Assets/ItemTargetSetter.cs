using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTargetSetter : MonoBehaviour
{
    private PlayerController2D pC2D;
    private CapsuleCollider2D cC2D;
    private Rigidbody2D rb2D;

    public IEnumerator stopDetection(bool enable)
    {
        if(enable == true){
            yield return new WaitForSeconds(1);
            cC2D.enabled = true;
            rb2D.mass = 1;
        } else {
            cC2D.enabled = false;
            rb2D.mass = 0;
        }
        // Code to execute after the delay
    }

    void Start(){
        pC2D = GameObject.FindObjectOfType<PlayerController2D>();
        cC2D = gameObject.GetComponent<CapsuleCollider2D>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            pC2D.targetSetter(gameObject);
        }
    }

    // void OnTriggerStay2Dx(Collider2D collider){
    //     if (collider.gameObject.tag == "Player" && pC2D.getTempTarget() != gameObject){
    //         pC2D.targetSetter(gameObject);
    //     }
    // }

    void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            if (pC2D.getTempTarget() == gameObject){
                pC2D.targetSetter(null);
            } 
        }
    }
}
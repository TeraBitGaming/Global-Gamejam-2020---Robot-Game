using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{

    private bool found = false;
    
    [SerializeField]
    private int upOrDown = 0;
    // -1 is down, 1 is up.

    [SerializeField]
    private Transform[] positions;

    [SerializeField]
    private GameObject arrow;
    
    void Update(){
        found = false;
        for (int i = 0; i < positions.Length; i++){
            if (positions[i] != null && found != true){
                switch(upOrDown){
                    case 1:
                        if (gameObject.transform.position.y < positions[i].position.y){
                            found = true;
                        }
                        break;

                    case 0:
                        break;

                    case -1:
                        if (gameObject.transform.position.y > positions[i].position.y){
                            found = true;
                        }  
                        break;
                }
            }
            
        }
        if(found && !arrow.active){
            arrow.SetActive(true);
        } else if (!found){
            arrow.SetActive(false);
        }
    }
}

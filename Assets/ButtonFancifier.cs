using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFancifier : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer buttonA;

    [SerializeField]
    private SpriteRenderer buttonD;

    [SerializeField]
    private SpriteRenderer buttonJ;

    [SerializeField]
    private SpriteRenderer buttonSpace;

    [SerializeField]
    private Sprite[] sprites;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0){
            buttonA.sprite = sprites[2];
            buttonD.sprite = sprites[4];

        } else if(Input.GetAxis("Horizontal") > 0.001){
            buttonA.sprite = sprites[2];
            buttonD.sprite = sprites[5];

        } else if (Input.GetAxis("Horizontal") < 0.001){
            buttonA.sprite = sprites[3];
            buttonD.sprite = sprites[4];
    
        }

        if (Input.GetAxis("Jump") == 0){
            buttonSpace.sprite = sprites[8];
        } else if(Input.GetAxis("Jump") > 0.001){
            buttonSpace.sprite = sprites[9];
        }

        if (Input.GetAxis("Fire1") == 0){
            buttonJ.sprite = sprites[6];

        } else if(Input.GetAxis("Fire1") > 0.001){
            buttonJ.sprite = sprites[7];
        }
    }
}

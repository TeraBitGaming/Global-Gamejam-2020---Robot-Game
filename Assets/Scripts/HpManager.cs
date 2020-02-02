using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour
{
    [SerializeField]
    private int HP;

    [SerializeField]
    private SpriteRenderer pCSprite;

    [SerializeField]
    private HeartPieceShatterer[] HPS;
    
    private float invincibility = 59f;

    public void takeDamage(){
        if(invincibility >= 59f){
            invincibility = 0f;
            HP--;
            HPS[HP].shatter();
           
        }

        if(HP == 0){
            // die
            SceneManager.LoadScene(2);
        }
    }

    public bool getInvincible(){
        if(invincibility >= 59f){
            return false;
        } else{
            return true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        invincibility++;
        invincibility = Mathf.Clamp(invincibility, 0f, 59f);

        pCSprite.color = new Color (1,1,1, Mathf.PingPong(invincibility, 1));
    }
}

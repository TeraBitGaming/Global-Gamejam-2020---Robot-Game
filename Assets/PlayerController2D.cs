using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 10.0f;

    private float jumpTimer = 1;

    private CapsuleCollider2D CC2d;

    private int lastDir = 0;
    // -1 == left
    // 1 == right.

    private RaycastHit2D hit;

    private GameObject target;

    private bool grabLock = false;

    [SerializeField]
    private float yeetSpeed = 5;

    [SerializeField]
    private bool grounded = true;

    [SerializeField]
    private float fallSpeed = -5;
    
    [SerializeField]
    private AnimationCurve jumpCurve;

    [SerializeField]
    private float jumpTimerSpeed = 0.001f;

    bool vector2Mask(Vector2 varToMask, float minMax){
        if(varToMask.x < minMax && varToMask.x > -minMax && varToMask.y < minMax && varToMask.y > -minMax){
            return true;
        } else {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        CC2d = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Jump") != 0 && grounded == true){
            jumpTimer = 0.0f;  
        }

        jumpTimer += jumpTimerSpeed;
        jumpTimer = Mathf.Clamp(jumpTimer, 0f, 1f);

        fallSpeed = jumpCurve.Evaluate(jumpTimer);
        
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, fallSpeed);        
    }

    void Update(){
        
        if (Input.GetAxis("Horizontal") > 0){
            lastDir = 1;

        } else if (Input.GetAxis("Horizontal") < 0){
            lastDir = -1;
        
        }
        
        if(target == null){
            switch(lastDir){
                case -1:
                    if(Input.GetAxis("Fire1") != 0){
                        hit = Physics2D.Raycast(transform.position, Vector2.left);
                        Debug.DrawRay(transform.position, Vector2.left, Color.green);
                    }

                    break;
                
                case 0:
                    break;

                case 1:
                    if(Input.GetAxis("Fire1") != 0){
                        hit = Physics2D.Raycast(transform.position, Vector2.right);
                        Debug.DrawRay(transform.position, Vector2.right, Color.green);
                    }

                    break;
            }

            if(hit.collider != null){
                if (hit.collider.tag == "Grabbable Object"){
                    target = hit.transform.gameObject;
                    target.GetComponent<ItemObject>().SetHolding(true);
                    grabLock = true;
                }
            }
        }

        if(target != null){
            target.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);

            if(Input.GetAxis("Fire1") != 0 && grabLock == false){
                
                target.GetComponent<Rigidbody2D>().velocity = new Vector2(lastDir * yeetSpeed, 3.0f);
                
                target = null;
            }
        }
        if(Input.GetAxis("Fire1") == 0){
            grabLock = false;
        }

        gameObject.GetComponent<Animator>().SetFloat("Direction", Input.GetAxis("Horizontal"));

        if(lastDir < 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if(vector2Mask(rb.velocity, 1.5f)){
            gameObject.GetComponent<Animator>().SetBool("Idle", true);
        } else {
            gameObject.GetComponent<Animator>().SetBool("Idle", false);
        }
    }

    void OnTriggerStay2D(Collider2D col){
        grounded = true;
    }
    
    void OnTriggerExit2D(Collider2D col){
        grounded = false;
    }
}

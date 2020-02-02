using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 10.0f;

    public float jumpForce = 10;

    private CapsuleCollider2D CC2d;

    private int lastDir = 0;
    // -1 == left
    // 1 == right.

    private RaycastHit2D hit;
    private RaycastHit2D interact;

    private GameObject target;

    private bool grabLock = false;

    private int mask = 1 << 8;

    [SerializeField]
    private HpManager hpManager;

    [SerializeField]
    private float yeetSpeed = 5;
    
    [SerializeField]
    private Animator animationController;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private bool grounded = true;

    // This is to check if the vector varToMask is between minMax & -minMax.
    bool vector2Mask(Vector2 varToMask, float minMax){
        if(varToMask.x < minMax && varToMask.x > -minMax && varToMask.y < minMax && varToMask.y > -minMax){
            return true;
        } else {
            return false;
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        CC2d = gameObject.GetComponent<CapsuleCollider2D>();
        mask = ~mask;
        // Debug.Log(LayerMask.GetMask("Player"));
    }

    void FixedUpdate()
    {
        // This part is for jumping & moving.
        if(Input.GetAxis("Jump") != 0 && grounded == true){
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        // if(!controlSuppressed) {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        // }
    }

    void Update(){

        //Debug.Log(rb.velocity.y);

        // if(!hpManager.getInvincible() && controlSuppressed){
        //     controlSuppressed = false;
        // }

        // For foot-step tippy taps.

        if (Input.GetAxis("Horizontal") != 0 && !GetComponent<AudioSource>().isPlaying && grounded){
            GetComponent<AudioSource>().Play();

        } else if (Input.GetAxis("Horizontal") == 0 || !grounded){
            GetComponent<AudioSource>().Pause();

        }
            
        if (Input.GetAxis("Horizontal") > 0){
            lastDir = 1;

        } else if (Input.GetAxis("Horizontal") < 0){
            lastDir = -1;
        
        }
        
        // This is dedicated to getting and grabbing any object with the "Grabbable Object" tag.
        if(target == null){
            switch(lastDir){
                case -1:
                    if(Input.GetAxis("Fire1") != 0){
                        hit = Physics2D.Raycast(transform.position, Vector2.left, 1f);
                        Debug.DrawRay(transform.position, Vector2.left, Color.green);
                    }

                    break;
                
                case 0:
                    break;

                case 1:
                    if(Input.GetAxis("Fire1") != 0){
                        hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
                        Debug.DrawRay(transform.position, Vector2.right, Color.green);
                    }
                    break;
            }

            if(hit.collider != null){
                if (hit.collider.tag == "Grabbable Object"){
                    target = hit.transform.gameObject;
                    target.GetComponent<ItemObject>().GetPickedUp();
                    grabLock = true;
                }
            }
        }

        // This is dedicated to throwing the target (object with "Grabbable Object" tag).
        if(target != null){
            target.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);

            if(Input.GetAxis("Fire1") != 0 && grabLock == false){
                
                target.GetComponent<Rigidbody2D>().velocity = new Vector2(lastDir * yeetSpeed, 3.0f) + rb.velocity;
                target.GetComponent<ItemObject>().GetThrowedAway();
                target = null;
            }
        }

        // This is to make sure you don't immediately throw the item if you keep the button pressed.
        if(Input.GetAxis("Fire1") == 0){
            grabLock = false;
        }

        // Here we handle "interact" button inputs.
        if(Input.GetAxis("Fire2") != 0){
            
            // Get "interact through raycasts"
            switch(lastDir){
                case -1:
                        interact = Physics2D.Raycast(transform.position, Vector2.left, 1f, mask);
                        Debug.DrawRay(transform.position, Vector2.left, Color.green);
                    break;
                
                case 0:
                    break;

                case 1:
                        interact = Physics2D.Raycast(transform.position, Vector2.right, 1f, mask);
                        Debug.DrawRay(transform.position, Vector2.right, Color.green);
                    break;
            }

            // Use "interact" to call functions within a script that corresponds with the item used.
            // interact.collider.GetComponent( SCRIPT GOES HERE).FUNCTION TO USE;
        }

        // This is for the animations.
        animationController.SetFloat("Direction", Input.GetAxis("Horizontal"));

        if(lastDir < 0){
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        if(vector2Mask(rb.velocity, 1.5f)){
            animationController.SetBool("Idle", true);
        } else {
            animationController.SetBool("Idle", false);
        }
        
        if(rb.velocity.x != 0 && grounded == true){
            animationController.SetBool("Walking", true);
        }

        if (rb.velocity.x < 0.1 && rb.velocity.x > -0.1){
            animationController.SetBool("Walking", false);
        } else if (grounded == false){
            animationController.SetBool("Walking", false);
        }

        if(rb.velocity.y > 0){
            animationController.SetInteger("VerticalMomentum", 1);
            animationController.SetBool("Idle", false);
        } else if (rb.velocity.y < 0){
            animationController.SetInteger("VerticalMomentum", -1);
            animationController.SetBool("Idle", false);
        }
        
        if (rb.velocity.y < 0.1 && rb.velocity.y > -0.1){
            animationController.SetInteger("VerticalMomentum", 0);
        }

        if (target != null){
            gameObject.GetComponent<Animator>().SetBool("Carrying", true);
        } else {
            gameObject.GetComponent<Animator>().SetBool("Carrying", false);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if (-rb.velocity.y > 11){
            rb.AddForce(transform.up * 50, ForceMode2D.Impulse);
        }
    }

    // Grounded detection.
    void OnTriggerStay2D(Collider2D col){
            if(col.gameObject.tag != "JumpIgnore"){
                grounded = true;
            }
        if(col.GetComponent<Container>())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (target == null)
                {
                    target = col.GetComponent<Container>().GetItemFromContainer();
                    target.GetComponent<ItemObject>().GetPickedUp();
                }
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.tag == "Enemy" & !hpManager.getInvincible()){
            hpManager.takeDamage();
        }
    }
}
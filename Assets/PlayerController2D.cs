using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 10.0f;

    private float jumpTimer = 1;

    private CapsuleCollider2D CC2d;

    [SerializeField]
    private bool grounded = true;

    [SerializeField]
    private float fallSpeed = -5;
    
    [SerializeField]
    private AnimationCurve jumpCurve;

    [SerializeField]
    private float jumpTimerSpeed = 0.001f;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        CC2d = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Jump") != 0 && grounded == true){
            jumpTimer = 0.0f;  
        }

        jumpTimer += jumpTimerSpeed;
        jumpTimer = Mathf.Clamp(jumpTimer, 0f, 1f);

        Debug.Log(jumpTimer);

        fallSpeed = jumpCurve.Evaluate(jumpTimer);
        
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, fallSpeed); 
    }

    void OnTriggerStay2D(Collider2D col){
        grounded = true;
    }
    
    void OnTriggerExit2D(Collider2D col){
        grounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // SerializeField: Can be modified within the editor, while being kept private
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float leftBound = -11.8f;
    [SerializeField] private float jumpingVelocity = 1.0f;
    private float h;
    private float v;

    private bool facingRight = true;

    // Initial instance variables
    private Rigidbody2D rigid;
    private Animator playerAnimator;

    private BoxCollider2D contact;

    private SpriteRenderer sprite;

    // Method: Called when script is initally loaded
    void Start()
    {
        // Retrieve the RigidBody2D component associated with the GO
        rigid = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        contact = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Method: Called once per frame
    void Update()
    {
        processMovement();
        processJumping();
        processDirection();
        onLanding();
    }

    // Method: Checked on each frame, processes when player presses any movement key
    private void processMovement()
    {
        // Retrieve input from user
        h = Input.GetAxis("Horizontal");
  
        // Applies velocity to player based on player input
        rigid.velocity = new Vector2(h * speed,rigid.velocity.y);

        // Prevents the player from going out-of-bounds to the left
        float bH = Mathf.Clamp(rigid.position.x,leftBound,100);

        // Initialised to true if there is any movement along the x axis
        bool running = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;

        // Set condition in running animation based on if the player is moving/idle
        playerAnimator.SetBool("Running",running);

        // Set position of player based on this
        rigid.position = new Vector2(bH,rigid.position.y);
    
    }

    private void processDirection()
    {
        // Had issues with flipping the attackpoint as well due to original implementation of flipping the player (transform.localScale)
        // SOLUTION: https://pastebin.com/kYYMvDd2
       if((h < 0 && facingRight) || (h > 0 && !facingRight))
       {
           	facingRight = !facingRight;
            transform.Rotate(new Vector3(0,180,0));
       }   
    }

    // Method: Checked on each frame, processes when player presses the 'Jump' key
    private void processJumping()
    {
        
        if(!contact.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
          
            return;
        }
        
        if(Input.GetButtonDown("Jump"))
        { 
            Debug.Log("jumping");
            // Create movement on the y axis
            Vector2 jump = new Vector2(0f,jumpingVelocity);

            // Adds to the current velocity of the GameObject (avoids changing x velocity)
            rigid.velocity+= jump;
        }

        bool jumping = rigid.velocity.y > 0.1;

        // Set condition in jumping animation based on if the player is moving/idle
        playerAnimator.SetBool("Jumping",jumping);
    }

    public void onLanding()
    {
        // if(contact.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        // {
        //         Debug.Log("not jumping");
        //         playerAnimator.SetBool("Jumping",false);
        // }
    }
}

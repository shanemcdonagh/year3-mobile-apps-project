using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // SerializeField: Can be modified within the editor, while being kept private
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float leftBound = -11.8f;
    [SerializeField] private float jumpingVelocity = 1.0f;
    private float h;
    private float v;

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
    }

    // Method: Checked on each frame, processes when player presses any movement key
    private void processMovement()
    {
        // Retrieve input from user
        h = Input.GetAxis("Horizontal");
        
        // Flips the sprite based on direction of movement
        if(h > 0)
        {
            sprite.flipX = false;
        }
        else if(h < 0)
        {
            sprite.flipX = true;
        }

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

    // Method: Checked on each frame, processes when player presses the 'Jump' key
    private void processJumping()
    {
        
        //bool falling = false;
        
        if(!contact.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Debug.Log("Bruh not touching fam");
            return;
        }
        
        if(Input.GetButtonDown("Jump"))
        { 
            // Create movement on the y axis
            Vector2 jump = new Vector2(0f,jumpingVelocity);

            // Adds to the current velocity of the GameObject (avoids changing x velocity)
            rigid.velocity+= jump;

            bool jumping = Mathf.Abs(rigid.velocity.y) > Mathf.Epsilon;

            // Set condition in jumping animation based on if the player is moving/idle
            //playerAnimator.SetBool("Jumping",jumping);
        }
    }
}

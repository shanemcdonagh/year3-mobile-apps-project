using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Variables
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float jumpingVelocity = 1.0f;
    private float h;
    private float v;
    private bool onGround;
    private bool facingRight = true;
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
    }

    // Method: Checked on each frame, processes when player presses any movement key
    private void processMovement()
    {
        // Retrieve input from user
        h = Input.GetAxis("Horizontal");

        // Applies velocity to player based on player input
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

        // Initialised to true if there is any movement along the x axis
        bool running = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;

        // Set condition in running animation based on if the player is moving/idle
        playerAnimator.SetBool("Running", running);       
    }

    // Method: Rotates the player based on movement on the x axis and current sprite direction
    private void processDirection()
    {
        if ((h < 0 && facingRight) || (h > 0 && !facingRight))
        {
            // Switch the boolean value and rotate the gameObject
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    // Method: Checked on each frame, processes when player presses the 'Jump' key
    private void processJumping()
    {
        // If: The player pressed to jump and they are currently on the ground
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            // Create movement on the y axis
            Vector2 jump = new Vector2(0f, jumpingVelocity);

            // Adds to the current velocity of the GameObject (avoids changing x velocity)
            rigid.velocity += jump;
        }

        // Set jumping animation if the player is not on the ground
        playerAnimator.SetBool("Jumping", !onGround);

    }

    // Method: Executed while another collider is in contact 
    private void OnTriggerStay2D(Collider2D other)
    {
        // Set condition in jumping animation based on if the player is currently grounded
        onGround = true;
    }

    // Method: Executed while the collider is no longer in contact
    void OnTriggerExit2D(Collider2D other)
    {
        onGround = false;
    }
}

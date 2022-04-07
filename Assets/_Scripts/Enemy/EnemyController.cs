using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxTimer = 3.0f;
    Rigidbody2D rb;
    private Animator enemyAnimator;
    private Vector3 initialScale;
    float timer;
    int direction = -1;

    // Method: Invoked on object instantiation (before Start())
    private void Awake()
    {
        initialScale = transform.localScale;
    }

    // Method: Invoked after the Awake() method
    private void Start()
    {
        // Retrieve the relevant components and set the timer to max timer
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease timer on each frame
        timer -= Time.deltaTime;

        // If: Timer reaches 0
        if (timer < 0)
        {
            // Change the direction value, update the enemies direction and reset timer
            direction = -direction;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            timer = maxTimer;
        }

        // Move the enemy
        moveEnemy();
    }

    // Method: Moves the enemy along the scene
    private void moveEnemy()
    {
        // BUG: When the player is in range of the enemy, and the enemy changes directions
        // The player also gets moved

        // Retrieve the current position
        Vector2 position = rb.position;
        
        // Increase the enemies x position 
        position.x = position.x + Time.deltaTime * speed * direction;

        // Initialised to true if there is any movement along the x axis
        bool running = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Set condition in running animation based on if the player is moving/idle
        enemyAnimator.SetBool("Running",true);
        
        // Move the rigidBody to the new position
        rb.MovePosition(position);
    }
}

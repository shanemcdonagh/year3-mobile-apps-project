using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://youtu.be/sPiVz1k-fEs
// Reference: https://youtu.be/d002CljR-KU
// This video was used to understand and apply attack damage and how to play the necessary animations

public class Enemy : MonoBehaviour
{

    // Variables
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int points = 10;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRate = 0.6f;
    [SerializeField] private float range;
    [SerializeField] private float colliderRange;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private BoxCollider2D boxCollider;
    private int currHealth;
    private float lastAttack;
    private PlayerHealth playerHealth;
    private Animator enemyAnimator;

    // Creates a variable that's viewable by all based on the value of the private variable "points"
    public int EnemyPoints { get { return points; } }

    // Acts as a function container 
    public delegate void KilledEnemy(Enemy em);

    // A listener method, which is triggered when an enemy is killed by the player
    // Can hold any function that matches the KilledEnemy signature 
    public static KilledEnemy KilledEnemyEvent;

    void Start()
    {
        // Retrieve component and initialise variable
        enemyAnimator = GetComponent<Animator>();
        currHealth = maxHealth;
        lastAttack = -999f;
    }

    void Update()
    {
        // Update attack time each frame
        

        // If: The player is in the line of sight
        if (PlayerDetected())
        {
            // If: The time passed is greater than the attack rate
            if (Time.time > attackRate + lastAttack)
            {
                // Attack the player
                processAttack();

                // Play sound
                SoundManager.Instance.PlayClip("Enemy Swing");

                lastAttack = Time.time;
            }
        }
    }

    // Method: When called, deals damage to the Enemy
    public void TakeDamage(int damage)
    {
        // Decrease health, set animation
        currHealth -= damage;
        enemyAnimator.SetTrigger("Hurting");

        // If: The current health is equal or less than 0
        if (currHealth <= 0)
        {
            // Process enemy death
            Death();
        }
    }

    // Method: Invoked when the enemies health is depleted
    private void Death()
    {
        // Disable the current script and controller on the GameObject
        this.enabled = false;
        GetComponent<EnemyController>().enabled = false;

        // Set animation boolean
        enemyAnimator.SetBool("Dying", true);

        // If: The current game object has the tag...
        if (gameObject.tag == "Boss")
        {
            // Stop the boss music
            SoundManager.Instance.StopClip("Boss Music");
            SoundManager.Instance.PlayClip("Victory");

            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }

        // Play audio
        SoundManager.Instance.PlayClip("Enemy Death");

        // Set the enemy to a layer that the player does not collide with
        gameObject.layer = 12;

        // Send a call to the event
        SendKilledEnemyEvent();
    }

    // Method: Used to send a call to any event which is listening (used in GameController)
    private void SendKilledEnemyEvent()
    {
        if (KilledEnemyEvent != null)
        {
            KilledEnemyEvent(this);
        }
    }

    // Method: Invoked when the player is in sight and attack rate is reset
    private void processAttack()
    {
        // Set attack animation
        enemyAnimator.SetTrigger("Attacking");
    }

    // Method: Determines if player is in the enemies line of sight 
    private bool PlayerDetected()
    {
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

        // Creates a boxcast based on enemy position (collider range and boxSize allows to customize the size of the raycast field through the inspector)
        RaycastHit2D detect = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderRange,
        boxSize, 0, Vector2.left, 0, playerLayer);

        // Retrieve the health component associated with the detected gameobject
        if (detect.collider != null)
        {
            playerHealth = detect.transform.GetComponent<PlayerHealth>();
        }

        // Return to where called
        return detect.collider;
    }

    // Method: Draws a wireframe on the box collider of the enemy (Visual purposes only when testing in scene view)
    private void OnDrawGizmos()
    {
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderRange, boxSize);
    }

    // Method: Damage the player
    // To note: This method is called on the attack animation of the enemy, set as an animation event
    public void HurtPlayer()
    {
        if (PlayerDetected())
        {
            // Decrease player health by calling the public method associated with it
            playerHealth.TakeDamage();
        }
    }
}

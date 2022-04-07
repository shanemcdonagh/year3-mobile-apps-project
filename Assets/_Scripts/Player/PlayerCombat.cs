using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float firstAttackRate = 4f;
    [SerializeField] private float secondAttackRate = 2f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float range = 0.5f;
    private Animator playerAnimator;
    private float lastAttack;
    private int attackDamage = 0;
    private BoxCollider2D contact;

    // Method: Called when script is initally loaded
    void Start()
    {
        // Receive the animator associated with the current GameObject
        playerAnimator = GetComponent<Animator>();
        contact = GetComponent<BoxCollider2D>();

        // Initially set to negative value so when player first attacks
        // It happens instantly
        lastAttack = -999f; 
    }

    // Method: Called on every frame
    void Update()
    {
        processAttacks();
    }

    // Method: Handles player attacks
    private void processAttacks()
    {
        // If: The player is midair while attacking...
        if (!contact.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            return;
        }

        // If: Primary attack
        if (Input.GetButtonDown("Fire1"))
        {
             // The time at the beginning of the frame is greater than
            // the attack rate and the time of the last attack...
            if(Time.time > firstAttackRate + lastAttack)
            {
                // Use the primary attack
                attack("primary");

                // Set time of last attack to current time at this frame
                lastAttack = Time.time;
            }
        }

        // If: Secondary attack
        if (Input.GetButtonDown("Fire2"))
        {
            // The time at the beginning of the frame is greater than
            // the attack rate and the time of the last attack...
            if(Time.time > secondAttackRate + lastAttack)
            {
                // Use the secondary attack
                attack("secondary");

                // Set time of last attack to current time at this frame
                lastAttack = Time.time;
            }
        }
    }

    // Method: Processes players attack options
    private void attack(string attackType)
    {
        string trigger = "";

        // Play audio
        SoundManager.Instance.PlayClip("Sword Swing");

        // If: Primary Attack
        if (attackType == "primary")
        {
            // Set animation type and attack damage
            trigger = "Attack1";
            attackDamage = 1;
        }
        else if (attackType == "secondary") // Secondary attack
        {
            // Set animation type and attack damage
            trigger = "Attack2";
            attackDamage = 2;
        }

        // Set player animation
        playerAnimator.SetTrigger(trigger);

        // Detect which enemies are in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);

        // For-each: Enemy that was struck...
        foreach (Collider2D enemy in hitEnemies)
        {
            // Retrieve the component attached to the enemy and apply damage
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Draws a wireframe around the attackpoint for adjusting purposes (only within the Editor)
    private void OnDrawGizmosSelected()
    {
        // If: There is no attackPoint specified
        if (attackPoint == null)
        {
            return;
        }

        // Draw wiresphere on attackpoint location, with a radius of the attack range
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}

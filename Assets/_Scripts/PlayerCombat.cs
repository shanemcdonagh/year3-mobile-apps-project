using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://youtu.be/sPiVz1k-fEs
// This video was used to understand and apply attack damage and how to play the necessary animations

public class PlayerCombat : MonoBehaviour

{
    [SerializeField] private Transform attackPoint;

    [SerializeField] private float attackRate = 2f;
     [SerializeField] private float attackTime = 0f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float range = 0.5f;
    private Animator playerAnimator;
    private int attackDamage = 0;

    // Method: Called when script is initally loaded
    void Start()
    {
        // Receive the animator associated with the current GameObject
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       processAttacks();
    }

    private void processAttacks()
    {
        if(Time.time >= attackTime)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                attack("primary");
                attackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // Method: Processes players attack options
    private void attack(string attackType)
    {
        // If: Primary Attack
        if(attackType == "primary")
        {
            // Play animation
            playerAnimator.SetTrigger("Attack1");
            attackDamage = 30;
        }
        
        // Detect which enemies are in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
        
        // For-each: Enemy that was struck...
        foreach(Collider2D enemy in hitEnemies)
        {
            // Retrieve the component attached to the enemy and apply damage
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Draws a wireframe around the attackpoint for adjusting purposes (only within the Editor)
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}

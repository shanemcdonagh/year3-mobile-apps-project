using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://youtu.be/sPiVz1k-fEs
// This video was used to understand and apply attack damage and how to play the necessary animations

public class PlayerCombat : MonoBehaviour

{
    [SerializeField] private Transform attackPoint;

    [SerializeField] private float firstAttackRate = 4f;
    [SerializeField] private float secondAttackRate = 2f;
    [SerializeField] private float attackTime = 0f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float range = 0.5f;
    private Animator playerAnimator;
    private int attackDamage = 0;
    private BoxCollider2D contact;

    // Method: Called when script is initally loaded
    void Start()
    {
        // Receive the animator associated with the current GameObject
        playerAnimator = GetComponent<Animator>();
        contact = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       processAttacks();
    }

    private void processAttacks()
    {
        // If: The player is midair while attacking...
        if(!contact.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {   
            return;
        }

        if(Time.time >= attackTime)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                attack("primary");
                attackTime = Time.time + 1f / firstAttackRate;
            }

            if(Input.GetButtonDown("Fire2"))
            {
                attack("secondary");
                attackTime = Time.time + 1f / secondAttackRate;
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
        if(attackType == "primary")
        {
            // Play animation
            trigger = "Attack1";
            attackDamage = 1;
        }
        else if(attackType == "secondary")
        {
            trigger = "Attack2";
            attackDamage = 2;
        }
        
        playerAnimator.SetTrigger(trigger);

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

    public void IncreaseAttackSpeed()
    {
        firstAttackRate--;
        secondAttackRate--;
    }
}

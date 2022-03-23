using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://youtu.be/sPiVz1k-fEs
// This video was used to understand and apply attack damage and how to play the necessary animations

public class Enemy : MonoBehaviour
{

    [SerializeField] private int maxHealth = 90;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float attackTime = 0f;
    private int currHealth;
    private Animator enemyAnimator;
    private BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -=damage;

        enemyAnimator.SetTrigger("Hurting");

        if(currHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        enemyAnimator.SetBool("Dying",true);

        // Set the enemy to a layer that the player does not collide with
        gameObject.layer = 12;
        
        // Disable the current script on the GameObject
        this.enabled = false;

    }

    private void processAttack()
    {
        if(Time.time >= attackTime)
        {
            attackTime = Time.time + 1f / attackRate;
        }
    }

    // Determines if player is in the enemies line of sight 
    private bool PlayerDetected()
    {
        RaycastHit2D detect = Physics2D.BoxCast(boxCollider.bounds.center,
        boxCollider.bounds.size,0,Vector2.left,0,playerLayer);
        
        return detect.collider;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(boxCollider.bounds.center,boxCollider.bounds.size);
    }
}

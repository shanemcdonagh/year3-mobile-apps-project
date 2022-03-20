using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://youtu.be/sPiVz1k-fEs
// This video was used to understand and apply attack damage and how to play the necessary animations

public class Enemy : MonoBehaviour
{

    [SerializeField] private int maxHealth = 90;
    private int currHealth;
    private Animator enemyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
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
}

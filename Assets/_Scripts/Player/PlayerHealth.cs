using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Variables
    [SerializeField] private float maxHealth = 3;
    private float playerHealth;
    private Animator playerAnimator;
    public float CurrentHealth() { return playerHealth; }
    public float MaxHealth() { return maxHealth; }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
        playerAnimator = GetComponent<Animator>();
    }

    // Method: Called by Enemy script to process player damage
    public void TakeDamage()
    {
        // Decrease health, and update UI values
        playerHealth--;
        FindObjectOfType<GameController>().uiUpdate();

        // Set hurting animation
        playerAnimator.SetTrigger("Hurting");

        // If: The player lives is 0 or less
        if (playerHealth <= 0)
        {
            // Disable current script and prevent player from moving
            this.enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Change layer of player so enemies won't attack mid death animation
            gameObject.layer = 12;

            // Set animation and reset player health
            playerAnimator.SetBool("Dying",true);
            playerHealth = maxHealth;
            
            // Call the GameController to handle death
            FindObjectOfType<GameController>().ProcessDeath();
        }
    }

    // Method: Called by HealthUp script
    public void GetHealth()
    {
        // Increase players health and update the UI
        playerHealth = maxHealth;
        FindObjectOfType<GameController>().uiUpdate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

    public void TakeDamage()
    {
        playerHealth--;
        FindObjectOfType<GameController>().uiUpdate();
        Debug.Log("Damaged");

        // Play player taking damage
        playerAnimator.SetTrigger("Hurting");

        if (playerHealth <= 0)
        {
            // Disable current script and prevent player from moving
            this.enabled = false;
            GetComponent<PlayerController>().enabled = false;

            // Set animation and reset player health
            playerAnimator.SetBool("Dying",true);
            playerHealth = maxHealth;
            
            // Call the GameController to handle death
            FindObjectOfType<GameController>().ProcessDeath();
        }
    }

    public void GetHealth()
    {
        // Increase players health and update the UI
        playerHealth++;
        FindObjectOfType<GameController>().uiUpdate();
    }

    // Determines if a player was hit by an arrow
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var arrow = collision.GetComponent<Arrow>();

        if(arrow)
        {
            TakeDamage();
        }
    }
}

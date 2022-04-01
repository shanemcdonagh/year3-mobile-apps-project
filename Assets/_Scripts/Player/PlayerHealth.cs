using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    public float playerLives { get; private set; } = 1;
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
            playerLives--;
            Debug.Log("Life lost");
            FindObjectOfType<GameController>().ProcessDeath();
        }
    }

    public void GetHealth()
    {
        // Increase players health and update the UI
        playerHealth++;
        FindObjectOfType<GameController>().uiUpdate();
    }
}

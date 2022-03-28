using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     public float playerLives { get; private set; } = 1;
     [SerializeField] public float playerHealth { get; private set; } = 5;
     private Animator playerAnimator;
   
    // Start is called before the first frame update
    void Start()
    {
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
}

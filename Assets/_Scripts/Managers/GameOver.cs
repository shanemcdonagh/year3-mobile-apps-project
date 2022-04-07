using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Method: Resets the game time back to normal and loads in menu
    public void LoadMenu()
    {
        // Reset time back to normal and load the menu
        FindObjectOfType<GameController>().ResetLives();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}

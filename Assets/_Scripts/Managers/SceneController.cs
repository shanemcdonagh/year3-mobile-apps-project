using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const string HighScore = "highScore";

    public void PlayGame()
    {
        // If: the level being loaded is the menu
        if (SceneManager.GetActiveScene().buildIndex + 1 == 1)
        {
            //If a player pref was set for the score
            if (PlayerPrefs.HasKey(HighScore))
            {
                // Reset the player score
                PlayerPrefs.SetFloat(HighScore, 0);
            }
        }
        // Load the next level in the index
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Exit the game
        Debug.Log("Quitting game");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Used to set the high score in the Player preferences
    public const string HighScore = "highScore";
    public const string Health = "health";

    private void Awake() 
    {
        // Set as singleton
        SingletonSetup();    
    }

    // Called after Awake()
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
      // Changes music theme where applicable
      if(scene.buildIndex < 6)
      {
        SoundManager.Instance.StopClip("Boss Music");
        SoundManager.Instance.PlayClip("Level Music");
      }

      if(scene.buildIndex == 6)
      {
        SoundManager.Instance.StopClip("Level Music");
        SoundManager.Instance.PlayClip("Boss Music");
      }
    }

    // Method: Loads the next level in the build index
    public void PlayGame()
    {
        // If: the level being loaded is the menu
        if (SceneManager.GetActiveScene().buildIndex + 1 == 1)
        {
            //If: a player pref was set for the score
            if (PlayerPrefs.HasKey(HighScore))
            {
                // Reset the player score
                PlayerPrefs.SetFloat(HighScore, 0);
            }

            // Reset the player lives on game start
            PlayerPrefs.SetFloat(Health, GameController.maxLives);
        }

        // Load the next level in the index
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method: Used to ensure that only one instance of a SceneController exists in a given scene
    private void SingletonSetup()
    {
        // If: More than one GameObject contains the tag "SceneController"
        if (GameObject.FindGameObjectsWithTag("SceneController").Length > 1)
        {
            // Destroy current gameObject
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Method: Quits the game (when in a build mode, doesn't function in editor)
    public void QuitGame()
    {
        // Exit the game
        Debug.Log("Quitting game");
        Application.Quit();
    }

    // Method: Resets the game time back to normal and loads in menu
    public void LoadMenu()
    {
        // Reset time back to normal and load the menu
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}

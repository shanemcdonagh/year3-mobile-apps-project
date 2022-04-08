using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Allows for other scripts to determine if the
    // game is currently paused
    public static bool GamePaused = false;

    // Used to set the pause menu to active/inactive
    [SerializeField] private GameObject PauseMenuUI;

    // Method: Called every frame
    void Update()
    {
        // If: The user pressed the Escape key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // If: The game is already paused..
            if(GamePaused)
            {
                // Resume the game
                ResumeGame();
            }
            else
            {
                // Pause the game
                PauseGame();
            }
        }
    }

    // Method: Closes the UI and resets game-time to normal
    public void ResumeGame()
    {
        // Set menu to false and restore time
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // Method: Closes the UI and resets game-time to normal
    public void ToggleMute()
    {
        FindObjectOfType<SoundManager>().ToggleMute();
    }

    // Method: Opens the UI and sets game time to zero
    private void PauseGame()
    {
        // Set menu to true and slow down time
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    // Method: Resets the game time back to normal and loads in menu
    public void LoadMenu()
    {
        // Deactive the pause menu when menu is loaded
        transform.GetChild(3).gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    // Method: Quits the game
    public void QuitGame()
    {
        // Quit the game (Only works in build, not in editor)
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

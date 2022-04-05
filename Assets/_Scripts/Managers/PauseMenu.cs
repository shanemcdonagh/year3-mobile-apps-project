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

    void Update()
    {
        // If: The user pressed the Escape key
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
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

    public void ResumeGame()
    {
        // Set menu to false and restore time
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    private void PauseGame()
    {
        // Set menu to true and slow down time
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        // Reset time back to normal and load the menu
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        // Quit the game (Only works in build, not in editor)
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

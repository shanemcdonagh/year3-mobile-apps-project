using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // Private variables: Can be modified in the editor
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SoundManager soundManager;

    // Variables
    private float playerScore = 00000;
    public static float maxLives { get; private set; } = 3; // To be read in for other scripts to set the playerprefs lives variable
    private float currentLives;
    private Animator playerAnimator;
    public const string HighScore = "highScore";
    public const string Health = "health";
    private PlayerHealth playerHealth;

    // Method: Called before Start (on object instantiation)
    void Awake()
    {
        SingletonSetup();

        //If a player pref was set for the score
        if (PlayerPrefs.HasKey(HighScore))
        {
            // Set player score to the current saved score
            playerScore = PlayerPrefs.GetFloat(HighScore);
        }

        if (PlayerPrefs.HasKey(Health))
        {
            currentLives = PlayerPrefs.GetFloat(Health);
        }
        else
        {
            currentLives = maxLives;
        }
    }

    // Method: Called after Awake(), retrieves player object based on PlayerHealth
    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Method: Called every frame
    private void Update()
    {
        // Invoke ui update 
        uiUpdate();
    }
    
    // Method: Reset the players lives
    public void ResetLives()
    {
        currentLives = maxLives;
    }

    // Method: Executed when object becomes active
    private void OnEnable()
    {
        // Begins listening for any calls made to the event, which then executes the specified Method
        Enemy.KilledEnemyEvent += OnKilledEnemyEvent;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called after OnEnable
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the timer back to normal time (set to slowmo by exit point)
        Time.timeScale = 1.0f;

        // If: The level loaded is the menu..
        if (scene.buildIndex == 0)
        {
            if (gameObject != null)
            {
                // Destroy the GameController
                Destroy(gameObject);
            }
        }
    }

    // Method: Used to ensure that only one instance of a GameController exists in a given scene
    private void SingletonSetup()
    {
        // If: More than one GameObject contains this script...
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // Destroy current gameObject
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Method: Updates the Player UI based on the current values
    public void uiUpdate()
    {
        scoreText.text = playerScore.ToString();
        livesText.text = PlayerPrefs.GetFloat(Health).ToString();
        healthText.text = FindObjectOfType<PlayerHealth>().CurrentHealth().ToString();

        // Set player preference
        PlayerPrefs.SetFloat(HighScore, playerScore);
    }

    // Method: Process the players death
    public IEnumerator OnPlayerDeath()
    {
        // Yield the code for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // Decrement current lives and update the player preferences
        currentLives--;
        PlayerPrefs.SetFloat(Health, currentLives);
 
        // Reload to a new game
        if (currentLives <= 0)
        {
            // Reset the player values
            PlayerPrefs.SetFloat(HighScore, 0);

            PlayerPrefs.SetFloat(Health, maxLives);

            // Pause game time
            Time.timeScale = 0f;

            // Display the Game Over menu
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
        }
        else
        {
            // Play sound and load the current level again
            SoundManager.Instance.PlayClip("Player Death");
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }

    // Method: Begins coroutine which processes player death
    public void ProcessDeath()
    {
        StartCoroutine(OnPlayerDeath());
    }

    // Method: When event listener is triggered, updates player score based on enemy value
    public void OnKilledEnemyEvent(Enemy e)
    {
        playerScore += e.EnemyPoints;
    }

    // Method: Executed when object becomes inactive
    private void OnDisable()
    {
        Enemy.KilledEnemyEvent -= OnKilledEnemyEvent;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private SoundManager soundManager;

    private float playerScore = 00000;
    private float maxLives = 3;
    private float currentLives;
    private Animator playerAnimator;
    public const string HighScore = "highScore";
    public const string Health = "health";
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
        else{
             SingletonSetup();
        }
       
        //If a player pref was set for the score
        if (PlayerPrefs.HasKey(HighScore))
        {
            // Set player score to the current saved score
            playerScore = PlayerPrefs.GetFloat(HighScore);
        }

        if(PlayerPrefs.HasKey(Health))
        {
            currentLives = PlayerPrefs.GetFloat(Health);
        }
        else
        {
            currentLives = maxLives;
        }
    }

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {

        // var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Debug.Log(sceneIndex);
        // if(SceneManager.GetActiveScene().buildIndex == 0)
        // {
        //     Destroy(gameObject);
        // }

        uiUpdate();
    }

    // Function: Executed when object becomes active
    private void OnEnable()
    {
        // Begins listening for any calls made to the event, which then executes the specified function
        Enemy.KilledEnemyEvent += OnKilledEnemyEvent;
        SceneManager.sceneLoaded +=OnSceneLoaded;
    }

   // called after OnEnable
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            if(gameObject !=null)
            {
                Destroy(gameObject);
            }
        }
    }

    // Used to ensure that only one instance of a GameController exists in a given scene
    private void SingletonSetup()
    {
        // If: More than one GameObject contains this script...
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            // Destroy current gameObject
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void uiUpdate()
    {
        scoreText.text = playerScore.ToString();
        livesText.text = currentLives.ToString();
        healthText.text = FindObjectOfType<PlayerHealth>().CurrentHealth().ToString();

        // Set player preference
        PlayerPrefs.SetFloat(HighScore, playerScore);
    }

    public IEnumerator OnPlayerDeath()
    {
        yield return new WaitForSeconds(2.0f);

        currentLives--;
        PlayerPrefs.SetFloat(Health, currentLives);

        // Reload to a new game
        if(currentLives <=0)
        {
            // Reset the player score
            PlayerPrefs.SetFloat(HighScore, 0);

            PlayerPrefs.SetFloat(Health, maxLives);

            SceneManager.LoadSceneAsync(0);

            // Reset the players lives
            currentLives = maxLives;
        }
        else
        {
            SoundManager.Instance.PlayClip("Player Death");
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }

    public void ProcessDeath()
    {
        StartCoroutine(OnPlayerDeath());
    }

    public void OnKilledEnemyEvent(Enemy e)
    {
        playerScore+= e.EnemyPoints;
    }

    // Function: Executed when object becomes inactive
    private void OnDisable()
    {
        Enemy.KilledEnemyEvent -= OnKilledEnemyEvent;
    }


}

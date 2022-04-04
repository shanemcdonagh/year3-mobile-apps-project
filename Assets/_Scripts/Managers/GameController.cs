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

    private int playerScore = 00000;
    private float maxLives = 3;
    private float currentLives;
    private Animator playerAnimator;
    
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        SingletonSetup();
    }

    private void Start()
    {
        currentLives = maxLives;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        uiUpdate();
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
            // Persist the gameObject
            DontDestroyOnLoad(gameObject);
        }
    }

    public void uiUpdate()
    {
        scoreText.text = playerScore.ToString();
        livesText.text = currentLives.ToString();
        healthText.text = FindObjectOfType<PlayerHealth>().CurrentHealth().ToString();
    }

    public IEnumerator OnPlayerDeath()
    {
        yield return new WaitForSeconds(2.0f);

        currentLives--;

        // Reload to a new game
        if(currentLives <=0)
        {
            SceneManager.LoadSceneAsync(0);
            Destroy(gameObject);
        }
        else
        {
            //soundManager.PlayClip("Player Death");
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

    // Function: Executed when object becomes active
    private void OnEnable()
    {
        // Begins listening for any calls made to the event, which then executes the specified function
        Enemy.KilledEnemyEvent += OnKilledEnemyEvent;
    }

    // Function: Executed when object becomes inactive
    private void OnDisable()
    {
        Enemy.KilledEnemyEvent -= OnKilledEnemyEvent;
    }


}

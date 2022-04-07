using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public const string Health = "health";

   // Method: Invoked when another collider comes into contact
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If: What came on contact was the player..
        if (collision.gameObject.tag == "Player")
        {
            // Start the coroutine
            StartCoroutine(nextScene());
        }
    }

   // Method: Invoked when the player comes in contact with the next level point
   private IEnumerator nextScene()
   {
       // Slow down time, wait a second, reset max lives and load the next level
       Time.timeScale = 0.5f;
       yield return new WaitForSeconds(1.0f);
       PlayerPrefs.SetFloat(Health,GameController.maxLives);
       var currIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadSceneAsync(currIndex + 1);
   }
}

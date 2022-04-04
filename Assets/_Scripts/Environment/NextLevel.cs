using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.gameObject.tag == "Player")
       {
           StartCoroutine(nextScene());
       }
   }

   private IEnumerator nextScene()
   {
       yield return new WaitForSeconds(2.0f);
       var currIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadSceneAsync(currIndex + 1);
   }
}

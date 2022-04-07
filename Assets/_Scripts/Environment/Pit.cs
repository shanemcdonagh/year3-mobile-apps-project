using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pit : MonoBehaviour
{
   // Method: Invoked when another collider comes into contact
   private void OnTriggerEnter2D(Collider2D collision)
   {
       // If: The collider in question was the player..
       if(collision.gameObject.tag == "Player")
       {
           // Process the players death
           FindObjectOfType<GameController>().ProcessDeath();
       }
   }
}

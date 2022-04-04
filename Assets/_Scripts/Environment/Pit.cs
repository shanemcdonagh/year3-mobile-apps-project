using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Pit : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.gameObject.tag == "Player")
       {
           FindObjectOfType<GameController>().ProcessDeath();
       }
   }
}

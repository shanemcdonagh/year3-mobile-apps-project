using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // SerializeField: Can be modified within the editor, while being kept private
    [SerializeField] private float speed = 15.0f;
    private float h;
    private float v;

    // Initial instance variables
    private Rigidbody2D rigid;

    // Function: Called when script is initally loaded
    void Start()
    {
        // Retrieve the RigidBody2D component associated with the GO
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Retrieve input from user
        h = Input.GetAxis("Horizontal");
        Debug.Log("Input was: " + h);

        // Applies velocity to player based on player input
        rigid.velocity = new Vector2(h * speed,0);
    
    }
}

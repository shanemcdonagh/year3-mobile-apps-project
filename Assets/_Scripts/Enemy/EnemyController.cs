using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxTimer = 3.0f;
    [SerializeField] private bool facingRight = false;
    Rigidbody2D rb;
    private Animator enemyAnimator;

    private Vector3 initialScale;

    float timer;
    int direction = -1;

    // Start is called before the first frame update
    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            timer = maxTimer;
        }
        moveEnemy();
    }

    private void moveEnemy()
    {
        Vector2 position = rb.position;
        
        position.x = position.x + Time.deltaTime * speed * direction;

        // Initialised to true if there is any movement along the x axis
        bool running = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        // Set condition in running animation based on if the player is moving/idle
        enemyAnimator.SetBool("Running",true);
        
        rb.MovePosition(position);
    }
}

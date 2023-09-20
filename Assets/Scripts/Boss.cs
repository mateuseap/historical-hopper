using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss:MonoBehaviour {
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    private Rigidbody2D rig;
    private Animator animation;
    private Transform player;
    private bool playerDestroyed = false;

    public float moveSpeed = 15f; // Adjust this value to control boss speed
    public float attackRange = 1.5f; // Distance at which the boss will start attacking

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (playerDestroyed) {
            Idle();
            return;
        }

        Vector2 spriteSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = spriteSize;

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        float verticalDistanceToPlayer = Mathf.Abs(player.position.y - transform.position.y);
    
        if (verticalDistanceToPlayer > attackRange) {
            Idle();
        } else if (distanceToPlayer > attackRange) {
            MoveTowardsPlayer();
        } else {
            AttackPlayer();
        }
    }

    private void Idle() {
        animation.SetBool("attack", false);
        animation.SetBool("run", false);
        rig.velocity = Vector2.zero;
        rig.MovePosition(rig.position); // Keep the boss in its current position
    }

    private void MoveTowardsPlayer() {
        Vector2 direction = player.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        // Flip the boss sprite based on the direction
        if (direction.x > 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if (direction.x < 0) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        animation.SetBool("attack", false);
        animation.SetBool("run", true);

        rig.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime)); // Use Time.fixedDeltaTime here
    }

    private void AttackPlayer() {
        animation.SetBool("attack", true);
        rig.velocity = Vector2.zero;
    }

    private void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerDestroyed = true;
            GameController.instance.ShowGameOver();
            Destroy(collision.gameObject);
        }
    }
}

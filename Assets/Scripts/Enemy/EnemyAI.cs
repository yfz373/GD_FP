using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player
    // Used so the enemy can chase the player
    public Transform player;

    // Enemy movement speed
    public float moveSpeed = 2f;

    // Force applied during knockback
    public float knockbackForce = 5f;

    // Duration of the knockback effect
    public float knockbackDuration = 0.2f;

    // Reference to the enemy's Rigidbody2D
    private Rigidbody2D rb;

    // Prevents the enemy from moving while being knocked back
    private bool isKnockedBack = false;

    // Enemy health value (currently unused)
    public int health = 100;

    // Prevents the enemy from damaging the player multiple times
    // while inside the same trigger collision
    private bool hasHitPlayer = false;

    // Reference to the player's health UI script
    public PlayerUI playerUI;

    void Start()
    {
        // Get the Rigidbody2D attached to this enemy
        rb = GetComponent<Rigidbody2D>();

        // Find the player using the Player tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // Stop movement if the enemy is being knocked back
        if (isKnockedBack)
            return;

        // Calculate direction from enemy to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move toward the player
        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prevent repeated damage while touching the player
        if (hasHitPlayer)
            return;

        // Check if the object touched is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get player's Rigidbody2D
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Get player's movement script
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            // If both components exist
            if (playerRb != null && playerMovement != null)
            {
                // Start knockback effect
                StartCoroutine(Knockback(playerRb, playerMovement));
            }

            // Deal damage to the player
            playerUI.TakeDamage(10);

            // Mark player as already hit
            hasHitPlayer = true;
        }
    }

    IEnumerator Knockback(Rigidbody2D playerRb, PlayerMovement playerMovement)
    {
        // Prevent enemy movement during knockback
        isKnockedBack = true;

        // Calculate direction away from the player
        Vector2 direction = (transform.position - player.position).normalized;

        // Stop current movement
        rb.linearVelocity = Vector2.zero;
        playerRb.linearVelocity = Vector2.zero;

        // Push enemy away from player
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        // Wait for knockback duration
        yield return new WaitForSeconds(knockbackDuration);

        // Allow enemy to move again
        isKnockedBack = false;

        // Apply knockback to the player
        StartCoroutine(playerMovement.Knockback(-direction, knockbackForce, knockbackDuration));
    }
}
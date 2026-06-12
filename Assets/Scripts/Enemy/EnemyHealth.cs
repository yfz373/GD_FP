using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Maximum health the enemy can have
    public int maxHealth = 30;

    // Current health of the enemy
    private int currentHealth;

    // Reference to the enemy's Rigidbody2D
    // Used for applying knockback force
    private Rigidbody2D rb;

    // Reference to the red health bar fill object
    // This object shrinks as the enemy loses health
    public Transform healthFill;

    private void Start()
    {
        // Set enemy health to full when the game starts
        currentHealth = maxHealth;

        // Get the Rigidbody2D attached to this enemy
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        // Reduce enemy health by the damage amount
        currentHealth -= damage;

        // Stop the enemy's current movement before knockback
        rb.linearVelocity = Vector2.zero;

        // Calculate remaining health percentage
        float healthPercent = (float)currentHealth / maxHealth;

        // Update the health bar size
        // The bar shrinks horizontally as health decreases
        healthFill.localScale =
            new Vector3(healthPercent, 1f, 1f);

        // Apply knockback force to the enemy
        // Direction is provided by the PlayerAttack script
        rb.AddForce(
            knockbackDirection * knockbackForce,
            ForceMode2D.Impulse);

        // Check if the enemy has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Destroy the enemy GameObject
        // This removes it from the scene
        Destroy(gameObject);

        // Increment the player's points for defeating this enemy
        GameManager.Instance.points++;
        GameManager.Instance.keyAppear();
        Debug.Log("Points: " + GameManager.Instance.points);
    }
}
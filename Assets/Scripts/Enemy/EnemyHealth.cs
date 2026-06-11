using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;

    private Rigidbody2D rb;

    public Transform healthFill;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection, float knockbackForce)
    {
        currentHealth -= damage;
        rb.linearVelocity = Vector2.zero;

        float healthPercent = (float)currentHealth / maxHealth;

        healthFill.localScale =
            new Vector3(healthPercent, 1f, 1f);

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}

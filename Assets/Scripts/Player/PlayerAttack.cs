using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    // Position where the attack is checked
    // Usually placed slightly in front of the player
    public Transform attackPoint;

    // Reference to the sword GameObject
    // The sword is activated during attacks and hidden afterwards
    public GameObject sword;

    // Radius of the attack hitbox
    public float attackRange = 1f;

    // Damage dealt to enemies
    public int attackDamage = 10;

    // Total angle of the sword swing animation
    public float swingAngle = 120f;

    // How long the sword swing takes
    public float swingDuration = 0.15f;

    // Layer mask used to detect enemies
    public LayerMask enemyLayers;

    // Prevents the player from attacking multiple times at once
    private bool isAttacking = false;

    // Strength of the knockback applied to enemies
    public float knockbackForce = 5f;

    public void Update()
    {
        // When the player presses Space and is not already attacking
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isAttacking)
        {
            // Start the attack animation and damage sequence
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        // Mark player as currently attacking
        isAttacking = true;

        // Show the sword sprite
        sword.SetActive(true);

        // Starting and ending rotation angles for the sword swing
        float startAngle = 30f;
        float endAngle = -65f;

        // Timer used to animate the swing
        float timer = 0f;

        // Set sword to starting position
        sword.transform.localRotation =
            Quaternion.Euler(0, 0, startAngle);

        // Animate the sword rotation over time
        while (timer < swingDuration)
        {
            // Smoothly interpolate between start and end angles
            float angle = Mathf.Lerp(
                startAngle,
                endAngle,
                timer / swingDuration);

            sword.transform.localRotation =
                Quaternion.Euler(0, 0, angle);

            timer += Time.deltaTime;

            yield return null;
        }

        // Ensure the sword ends exactly at the final angle
        sword.transform.localRotation =
            Quaternion.Euler(0, 0, endAngle);

        Debug.Log("Player attacked!");

        // Detect all enemies inside the attack radius
        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayers);

        // Loop through every enemy hit
        foreach (Collider2D enemy in hitEnemies)
        {
            // Get the EnemyHealth component
            EnemyHealth enemyHealth =
                enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Calculate knockback direction
                // Enemy is pushed away from the player
                Vector2 knockbackDirection =
                    (enemy.transform.position - transform.position)
                    .normalized;

                // Deal damage and apply knockback
                enemyHealth.TakeDamage(
                    attackDamage,
                    knockbackDirection,
                    knockbackForce);
            }
        }

        // Keep sword visible briefly after the swing
        yield return new WaitForSeconds(0.05f);

        // Hide the sword
        sword.SetActive(false);

        // Allow the player to attack again
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Only draw if attack point exists
        if (attackPoint == null)
            return;

        // Draw attack range in Scene View
        // Helps visualize the attack hitbox
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
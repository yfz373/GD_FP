using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player movement speed
    [SerializeField] private float moveSpeed = 5f;

    // Reference to player visuals (currently unused)
    [SerializeField] private Transform visuals;

    // Prevents movement while the player is being knocked back
    public bool isKnockedBack = false;

    // Reference to the player's Rigidbody2D
    private Rigidbody2D rb;

    // Stores movement input direction
    private Vector2 movement;

    private void Awake()
    {
        // Get the Rigidbody2D attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Reset movement every frame
        movement = Vector2.zero;

        // Move up when W is pressed
        if (Keyboard.current.wKey.isPressed)
            movement.y = 1;

        // Move down when S is pressed
        if (Keyboard.current.sKey.isPressed)
            movement.y = -1;

        // Move left when A is pressed
        if (Keyboard.current.aKey.isPressed)
            movement.x = -1;

        // Move right when D is pressed
        if (Keyboard.current.dKey.isPressed)
            movement.x = 1;

        // Normalize movement so diagonal movement
        // is not faster than horizontal/vertical movement
        movement.Normalize();

        // Flip the player sprite when moving left
        if (movement.x < 0)
        {
            gameObject.transform.localScale =
                new Vector3(-1f, 1f, 1f);
        }
        // Flip back when moving right
        else if (movement.x > 0)
        {
            gameObject.transform.localScale =
                new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        // Only allow movement if not being knocked back
        if (!isKnockedBack)
        {
            rb.linearVelocity =
                movement * moveSpeed;
        }

        // Stop movement code while knocked back
        if (isKnockedBack)
            return;
    }

    public IEnumerator Knockback(
        Vector2 direction,
        float force,
        float duration)
    {
        // Disable movement during knockback
        isKnockedBack = true;

        // Stop current movement
        rb.linearVelocity = Vector2.zero;

        // Apply knockback force
        rb.AddForce(
            direction * force,
            ForceMode2D.Impulse);

        // Wait for knockback duration
        yield return new WaitForSeconds(duration);

        // Allow movement again
        isKnockedBack = false;
    }
}
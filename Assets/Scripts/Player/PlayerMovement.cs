using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform visuals;

    public bool isKnockedBack = false;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y = 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y = -1;

        if (Keyboard.current.aKey.isPressed)
            movement.x = -1;

        if (Keyboard.current.dKey.isPressed)
            movement.x = 1;

        movement.Normalize();

        // Face left/right
        if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (movement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            rb.linearVelocity = movement * moveSpeed;
        }

        if (isKnockedBack)
            return;
    }

    public IEnumerator Knockback(Vector2 direction, float force, float duration)
    {
        isKnockedBack = true;

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(direction * force,
                    ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        isKnockedBack = false;
    }
}

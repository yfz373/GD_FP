using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;

    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D rb;
    private bool isKnockedBack = false;

    public int health = 100;

    public PlayerUI playerUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (isKnockedBack)
            return;

        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb =
                collision.gameObject.GetComponent<Rigidbody2D>();

            PlayerMovement playerMovement =
                collision.gameObject.GetComponent<PlayerMovement>();

            if (playerRb != null && playerMovement != null)
            {
                StartCoroutine(Knockback(playerRb, playerMovement));
            }

            playerUI.TakeDamage(10);
        }
    }

    IEnumerator Knockback(Rigidbody2D playerRb, PlayerMovement playerMovement)
    {
        isKnockedBack = true;
        playerMovement.isKnockedBack = true;

        Vector2 direction =
            (transform.position - player.position).normalized;

        rb.linearVelocity = Vector2.zero;
        playerRb.linearVelocity = Vector2.zero;

        // Enemy pushed away
        rb.AddForce(direction * knockbackForce,
                    ForceMode2D.Impulse);

        // Player pushed away
        playerRb.AddForce(-direction * knockbackForce,
                          ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockedBack = false;
        playerMovement.isKnockedBack = false;
    }
}
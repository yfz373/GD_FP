using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject sword;

    public float attackRange = 1f;
    public int attackDamage = 10;

    public float swingAngle = 120f;
    public float swingDuration = 0.15f;

    public LayerMask enemyLayers;

    private bool isAttacking = false;
    public float knockbackForce = 5f;

    public void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        isAttacking = true;

        sword.SetActive(true);

        float startAngle = 30f;
        float endAngle = -65f;

        float timer = 0f;

        sword.transform.localRotation =
            Quaternion.Euler(0, 0, startAngle);

        while (timer < swingDuration)
        {
            float angle = Mathf.Lerp(
                startAngle,
                endAngle,
                timer / swingDuration);

            sword.transform.localRotation = Quaternion.Euler(0, 0, angle);

            timer += Time.deltaTime;

            yield return null;
        }

        sword.transform.localRotation =
            Quaternion.Euler(0, 0, endAngle);

        Debug.Log("Player attacked!");

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth =
                enemy.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;

                enemyHealth.TakeDamage(
                    attackDamage,
                    knockbackDirection,
                    knockbackForce);
            }
        }

        yield return new WaitForSeconds(0.05f);

        sword.SetActive(false);

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange);
    }

}

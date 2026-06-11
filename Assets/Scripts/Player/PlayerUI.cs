using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthText;

    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateUI();
        
    }

    private void UpdateUI()
    {
        healthBar.value = currentHealth;
        healthText.text = currentHealth + " / " + maxHealth;
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.PlayerDied();
        }
    }
}

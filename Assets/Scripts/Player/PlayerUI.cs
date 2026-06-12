using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Reference to the UI Slider used as the health bar
    public Slider healthBar;

    // Reference to the text that displays HP values
    public TMP_Text healthText;

    // Maximum amount of health the player can have
    public int maxHealth = 100;

    // Current health of the player
    public int currentHealth;

    void Start()
    {
        // When the game starts, set current health to maximum health
        currentHealth = maxHealth;

        // Set the slider's maximum value
        healthBar.maxValue = maxHealth;

        // Fill the slider completely since player starts with full health
        healthBar.value = currentHealth;

        // Update the health bar and text display
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;

        // Check if health has reached zero or below
        if (currentHealth <= 0)
        {
            // Prevent health from going into negative values
            currentHealth = 0;

            // Trigger player death
            Die();
        }

        // Refresh the UI after taking damage
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update the health bar value
        healthBar.value = currentHealth;

        // Update the text display
        // Example: "75 / 100"
        healthText.text = currentHealth + " / " + maxHealth;
    }

    public void Die()
    {
        // Double-check that health is zero before triggering death
        if (currentHealth <= 0)
        {
            // Notify the GameManager that the player has died
            // GameManager can then show a Game Over screen,
            // stop the game, restart the level, etc.
            GameManager.Instance.PlayerDied();
        }
    }
}
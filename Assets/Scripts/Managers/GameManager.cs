using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance so other scripts can access GameManager
    // Example:
    // GameManager.Instance.WinGame();
    public static GameManager Instance;

    // UI shown when player wins
    public GameObject WinUI;

    // UI shown when player loses
    public GameObject LoseUI;

    // Player HUD (health bar, etc.)
    public GameObject PlayerUI;

    public int pointsToWin = 5;
    public int points = 0;

    public GameObject key;

    public void keyAppear()
    {
        if (points >= pointsToWin)
        {
            key.SetActive(true);
        }
    }

    private void Awake()
    {
        // Store this GameManager in the static Instance variable
        // so other scripts can access it from anywhere
        Instance = this;
    }

    public void WinGame()
    {
        // Show the win screen
        WinUI.SetActive(true);

        // Hide the player's HUD
        PlayerUI.SetActive(false);

        // Pause the game
        // Time.timeScale = 0 means everything stops
        Time.timeScale = 0f;
    }

    public void PlayerDied()
    {
        // Show the lose/game over screen
        LoseUI.SetActive(true);

        // Hide the player's HUD
        PlayerUI.SetActive(false);

        // Pause the game
        Time.timeScale = 0f;
    }
}
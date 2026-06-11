using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject WinUI;
    public GameObject LoseUI;
    public GameObject PlayerUI;

    private void Awake()
    {
        Instance = this;
    }

    public void WinGame()
    {
        WinUI.SetActive(true);
        PlayerUI.SetActive(false);

        Time.timeScale = 0f;
    }

    public void PlayerDied()
    {
        LoseUI.SetActive(true);
        PlayerUI.SetActive(false);

        Time.timeScale = 0f;
    }

}

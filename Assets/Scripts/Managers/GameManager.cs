using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject WinUI;

    private void Awake()
    {
        Instance = this;
    }

    public void WinGame()
    {
        WinUI.SetActive(true);

        Time.timeScale = 0f;
    }

}

using UnityEngine;

public class openDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door trigger.");
            GameManager.Instance.WinGame();
        }
    }
}

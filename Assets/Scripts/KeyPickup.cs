using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Door door; // Reference to the door

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            door.OpenDoor();

            Destroy(gameObject);
        }
    }
}

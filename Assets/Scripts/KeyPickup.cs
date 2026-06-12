using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    // Reference to the door that will be opened
    // Assign this in the Inspector
    public Door door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object touching the key is the player
        if (other.CompareTag("Player"))
        {
            // Open the door when the player picks up the key
            door.OpenDoor();

            // Remove the key from the scene
            // so it cannot be collected again
            Destroy(gameObject);
        }
    }
}
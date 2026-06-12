using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject DoorOpen;
    public GameObject DoorClosed;

    public void OpenDoor()
    {
        DoorOpen.SetActive(true);
        DoorClosed.SetActive(false);
    }
}
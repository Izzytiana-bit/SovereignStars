using UnityEngine;
using UnityEngine.Events;

public class RoomEnterAction : MonoBehaviour
{
    bool hasEntered;
    public UnityEvent OnRoomEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasEntered)
            {
                hasEntered = true;
                OnRoomEntered.Invoke();
            }
        }
    }
}

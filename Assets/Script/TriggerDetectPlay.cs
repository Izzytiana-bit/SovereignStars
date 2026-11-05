using UnityEngine;
using UnityEngine.Events;

public class TriggerDetectPlay : MonoBehaviour
{
    public UnityEvent onPlayerDectect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayerDectect.Invoke();
        }
    }
}

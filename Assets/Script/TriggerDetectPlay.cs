using UnityEngine;
using UnityEngine.Events;

public class TriggerDetectPlay : MonoBehaviour
{
    public UnityEvent onPlayerDectect;
    public UnityEvent onPlayerGone;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("you're in");
            onPlayerDectect.Invoke();
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayerGone.Invoke();
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TriggerDetectPlay : MonoBehaviour
{
    public UnityEvent onPlayerDectect;
    public UnityEvent onPlayerGone;
    public UnityEvent onInput;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("you're in");
            onPlayerDectect.Invoke();
        }
        if (Input.GetKey(KeyCode.E))
        {
            onInput.Invoke();
            Debug.Log("Teleport");
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onPlayerGone.Invoke();
        }
    }

    public void EnemyTest()
    {
        SceneManager.LoadScene("EnemyTest");
    }

    public void GridTest()
    {
        SceneManager.LoadScene("GridTest");
    }

    public void RoomGenTest()
    {
        SceneManager.LoadScene("RoomGenTest");
    }

}

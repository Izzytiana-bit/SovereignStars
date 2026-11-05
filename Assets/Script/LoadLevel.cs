using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public UnityEvent OnWin;

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
  
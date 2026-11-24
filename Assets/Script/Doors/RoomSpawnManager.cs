using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class RoomSpawnManager : MonoBehaviour
{

    public EnemySpawner[] spawners;
    List<GameObject> enemies = new List<GameObject>();

    public UnityEvent onRoomCleared;

    bool hasSpawned;
    bool hasDefeated;

    public void SpawnEnemies()
    {
        hasSpawned = true;
        foreach (EnemySpawner e in spawners)
        {
            enemies.Add(e.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned && !IsEnemiesRemaining() && !hasDefeated)
        {
            hasDefeated = true;
            onRoomCleared.Invoke();
        }
    }

    bool IsEnemiesRemaining()
    {
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                return true;
            }
        }
        return false;
    }

}

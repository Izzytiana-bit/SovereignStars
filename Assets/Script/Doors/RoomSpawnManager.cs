using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class RoomSpawnManager : MonoBehaviour
{

    public EnemySpawner[] spawners;
    public List<GameObject> enemies = new List<GameObject>();

    public UnityEvent onRoomCleared;

    bool hasSpawned;
    bool hasDefeated;

    public void SpawnEnemies()
    {
        Debug.Log("Enemies Spawned");
        Debug.Log(spawners.Length + "Spawners Number");
        hasSpawned = true;
        foreach (EnemySpawner e in spawners)
        {
            enemies.Add(e.gameObject);
            e.SpawnEnemy();

        }
    }


    public void RemoveEnemyFromList()
    {
        enemies.RemoveAt(enemies.Count - 1);
        Debug.Log("enemy count is" + enemies.Count);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is Enemies Remaining?" + " " + IsEnemiesRemaining());
        if (hasSpawned && !IsEnemiesRemaining() && !hasDefeated)
        {
            hasDefeated = true;
            onRoomCleared.Invoke();
            Debug.Log("You did it!");
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

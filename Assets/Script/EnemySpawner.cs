using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float respawnDelay = 3.0f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SpawnEnemy();
    }

    public void NotifyDeath()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Please assing an Enemy Prefab to Spawner");
            return;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        Enemy enemyHealth = newEnemy.GetComponent<Enemy>();
        if (enemyHealth != null)
        {
            enemyHealth.Initialize(this);
        }
    }
}


using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
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

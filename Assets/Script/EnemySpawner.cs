
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;


    public GameObject SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Please assign an Enemy Prefab to Spawner");
            return null;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        Enemy enemyHealth = newEnemy.GetComponent<Enemy>();
        if (enemyHealth != null)
        {
            enemyHealth.Initialize(this);
        }

        return newEnemy;
    }
}

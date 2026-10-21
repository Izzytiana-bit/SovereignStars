using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float respawnDelay = 3.0f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SpawnPlayer();
    }

    public void NotifyDeath()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Please assing an Player Prefab to Spawner");
            return;
        }

        GameObject newPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);

        Player playerHealth = newPlayer.GetComponent<Player>();
        if (playerHealth != null)
        {
            playerHealth.Initialize(this);
        }
    }
}

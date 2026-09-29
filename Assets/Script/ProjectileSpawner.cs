using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private KeyCode FireKeyCode = KeyCode.Space;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FireKeyCode))
        {
            FireProjectile();
        } 
    }

    private void FireProjectile()
    {
        if (projectilePrefab && aimTransform)
        {
            Projectile createdProjectile = Instantiate<Projectile>(projectilePrefab, aimTransform.position, aimTransform.rotation);
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float time;
    public float damageAmount;
    public Enemy enemy;

    private void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("taken Damage");
            enemy.TakeDamage(damageAmount);
        }
    }

    private void MoveProjectile()
    {
        transform.position = transform.position + (transform.forward * Time.deltaTime * speed);
    }
}

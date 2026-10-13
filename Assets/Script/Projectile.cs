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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("bonk");
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("taken Damage");
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }

    private void MoveProjectile()
    {
        transform.position = transform.position + (transform.forward * Time.deltaTime * speed);
    }
}

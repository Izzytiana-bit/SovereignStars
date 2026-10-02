using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private EnemySpawner spawner;

    Rigidbody rb;
    Transform target;

    [SerializeField] FloatingHealthBar healthBar;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) 
            return;

        currentHealth -= damageAmount;
        Debug.Log($"Enemy took {damageAmount} damage. Health: {currentHealth}");
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Initialize(EnemySpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    private void Die()
    {
        if (spawner != null)
        {
            spawner.NotifyDeath();
        }

        Destroy(gameObject);
    }

    internal void TakeDamage()
    {
        throw new NotImplementedException();
    }
}

using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100; // Maximum health
    private int currentHealth;

    [Header("Death Effects")]
    public GameObject deathEffect; // Optional effect to play on death
    public bool destroyOnDeath = true; // Should the GameObject be destroyed on death?

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the damage amount
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {currentHealth}");

        // Check if health has dropped to or below zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        // Increase current health but do not exceed max health
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"{gameObject.name} healed by {amount}. Current health: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        // Play death effect if assigned
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Destroy the object if destroyOnDeath is true
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
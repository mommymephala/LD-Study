using UnityEngine;

public class Fireball : MonoBehaviour
{
    public ParticleSystem fireballParticles;
    public int damage = 10;

    void Start()
    {
        if (fireballParticles == null)
        {
            fireballParticles = GetComponent<ParticleSystem>();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        // Handle what happens when the fireball hits another object
        Debug.Log($"Fireball hit: {other.name}");

        // Example: Apply damage if the target has a health component
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Destroy the fireball (optional, or handle visual effects)
        // Destroy(gameObject);
    }
}
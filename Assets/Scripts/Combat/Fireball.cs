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
        Debug.Log($"Fireball hit: {other.name}");

        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // Destroy the fireball (optional, or handle visual effects)
        // Destroy(gameObject);
    }
}
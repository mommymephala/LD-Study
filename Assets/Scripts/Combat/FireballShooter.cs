using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    [Header("Fireball Settings")]
    public GameObject fireballPrefab; // The fireball prefab
    public Transform firePoint; // The point where fireballs are spawned
    public float fireRate = 1f; // Time between shots
    public float fireballLifetime = 5f; // Fireball lifetime

    [Header("Audio")]
    public AudioSource fireSound; // Optional: Add a sound effect for firing

    private float nextFireTime; // Tracks when the player can fire again

    void Update()
    {
        // Check if the player presses the Fire1 button and if enough time has passed since the last fire
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        // Set the next available fire time
        nextFireTime = Time.time + 1f / fireRate;

        // Spawn the fireball at the firePoint position and rotation
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        // Set the fireball's forward velocity (using the particle system for visual speed)
        ParticleSystem fireballParticles = fireball.GetComponent<ParticleSystem>();
        if (fireballParticles != null)
        {
            // Adjust the particle system's velocity (if needed)
            var mainModule = fireballParticles.main;
            mainModule.startLifetime = fireballLifetime;
        }

        // Play fire sound if assigned
        if (fireSound != null)
        {
            fireSound.Play();
        }

        // Destroy the fireball after its lifetime expires
        Destroy(fireball, fireballLifetime);
    }
}
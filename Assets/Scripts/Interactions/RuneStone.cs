using UnityEngine;

public class RuneStone : MonoBehaviour
{
    public bool isActivated = false;
    public ParticleSystem activationEffect; // Optional: particle effect for activation

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the fireball hit the rune stone
        if (collision.gameObject.CompareTag("Fireball") && !isActivated)
        {
            ActivateRune();
        }
    }

    private void ActivateRune()
    {
        isActivated = true;

        // Optional: Play activation effect
        if (activationEffect != null)
        {
            activationEffect.Play();
        }

        // Optional: Change the rune's appearance (e.g., glowing or color change)
        // GetComponent<Renderer>().material.color = Color.green;

        // Notify the puzzle manager
        RunicPuzzleManager.Instance.RuneActivated();
    }
}
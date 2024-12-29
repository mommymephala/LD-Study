using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorOpening[] doorOpenings;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Vector3 playerPosition = other.transform.position;

        foreach (DoorOpening doorOpening in doorOpenings)
        {
            if (!doorOpening.isOpen)
            {
                doorOpening.Open(playerPosition);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        foreach (DoorOpening doorOpening in doorOpenings)
        {
            if (doorOpening.isOpen)
            {
                doorOpening.Close();
            }
        }
    }
}
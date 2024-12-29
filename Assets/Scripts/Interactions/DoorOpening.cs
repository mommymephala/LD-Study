using System.Collections;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] private float speed = 1f;

    [Header("Door Type Configs")]
    [SerializeField] private bool isRotatingDoor = true;  // Set to true for rotating door, false for sliding
    
    [Header("Sliding Configs")]
    [SerializeField] private Vector3 slideDirection = Vector3.right;
    [SerializeField] private float slideAmount = 3f;

    [Header("Rotation Configs")]
    [SerializeField] private float rotationAmount = 90f;  // Rotation amount in degrees
    [SerializeField] private float forwardDirection = 0f; // Forward direction threshold for rotation

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _startRotation;
    private Vector3 _forward;
    private Coroutine _animationCoroutine;

    private void Awake()
    {
        _startPosition = transform.position;
        _endPosition = _startPosition + slideAmount * slideDirection;
        _startRotation = transform.rotation.eulerAngles;
        _forward = transform.right; // Assuming "right" is the forward-facing direction
    }

    public void Open(Vector3 userPosition)
    {
        if (isOpen) return;
        AudioManager.instance.PlaySound(AudioManager.instance.doorOpenSound);

        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        if (isRotatingDoor)
        {
            // Handle rotation based on user's position
            float dot = Vector3.Dot(_forward, (userPosition - transform.position).normalized);
            _animationCoroutine = StartCoroutine(RotateDoorOpen(dot));
        }
        else
        {
            // Handle sliding door
            _animationCoroutine = StartCoroutine(SlideDoor(_endPosition));
        }
    }

    public void Close()
    {
        if (!isOpen) return;

        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        if (isRotatingDoor)
        {
            _animationCoroutine = StartCoroutine(RotateDoorClose());
        }
        else
        {
            _animationCoroutine = StartCoroutine(SlideDoor(_startPosition));
        }
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;
        isOpen = targetPosition == _endPosition;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        _animationCoroutine = null;
    }

    private IEnumerator RotateDoorOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        // Rotate the door based on the user's position relative to the door's forward direction
        if (forwardAmount >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y + rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y - rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            time += Time.deltaTime * speed;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    private IEnumerator RotateDoorClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRotation);

        isOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            time += Time.deltaTime * speed;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UdonElevatorController : UdonSharpBehaviour
{
    public Transform[] floorPositions;
    public float transitionDuration = 1f; // Duration of the transition
    public float doorOpenDuration = 10f; // Duration to keep the doors open
    public Transform leftDoor;
    public Transform rightDoor;
    public AudioSource doorAudioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private int currentFloor = 0;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float transitionProgress = 0f; // Transition progress from 0 to 1
    private float transitionSpeed = 0f; // Speed of the transition

    private bool doorsOpen = false;
    private float doorTimer = 0f;

    private void Start()
    {
        startPosition = floorPositions[currentFloor].position;
        targetPosition = startPosition;
        transform.position = targetPosition;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (transitionProgress < 1f)
            {
                transitionProgress += Time.deltaTime / transitionDuration;
                float easedProgress = EaseInOut(transitionProgress);
                transform.position = Vector3.Lerp(startPosition, targetPosition, easedProgress);
            }
            else
            {
                isMoving = false;
                OpenDoors(); // Open the doors when the elevator arrives
                doorTimer = 0f; // Reset the door timer
            }
        }

        // Keep track of door open duration
        if (doorsOpen)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer >= doorOpenDuration)
            {
                CloseDoors(); // Close the doors after the specified duration
            }
        }
    }

    public void GoToFloor(int targetFloor)
    {
        if (!isMoving && targetFloor != currentFloor && targetFloor >= 0 && targetFloor < floorPositions.Length)
        {
            startPosition = targetPosition;
            targetPosition = floorPositions[targetFloor].position;
            isMoving = true;
            transitionProgress = 0f;
            CalculateTransitionSpeed();
        }
    }

    public bool IsElevatorMoving()
    {
        return isMoving;
    }

    private void CalculateTransitionSpeed()
    {
        float distance = Vector3.Distance(startPosition, targetPosition);
        transitionSpeed = distance / transitionDuration;
    }

    // Easing function: ease-in-out curve
    private float EaseInOut(float t)
    {
        // Apply ease-in-out curve equation
        return 0.5f * (Mathf.Sin((t - 0.5f) * Mathf.PI) + 1f);
    }

    private void OpenDoors()
    {
        leftDoor.gameObject.SetActive(true); // Enable left door
        rightDoor.gameObject.SetActive(true); // Enable right door
        doorsOpen = true;
        PlayDoorSound(doorOpenSound); // Play door open sound
    }

    private void CloseDoors()
    {
        leftDoor.gameObject.SetActive(false); // Disable left door
        rightDoor.gameObject.SetActive(false); // Disable right door
        doorsOpen = false;
        PlayDoorSound(doorCloseSound); // Play door close sound
        MoveToNextFloor();
    }

    private void MoveToNextFloor()
    {
        currentFloor++;
        if (currentFloor >= floorPositions.Length)
        {
            currentFloor = 0;
        }

        targetPosition = floorPositions[currentFloor].position;
        isMoving = true;
        transitionProgress = 0f;
        CalculateTransitionSpeed();
    }

    private void PlayDoorSound(AudioClip clip)
    {
        if (doorAudioSource != null && clip != null)
        {
            doorAudioSource.PlayOneShot(clip);
        }
    }
}

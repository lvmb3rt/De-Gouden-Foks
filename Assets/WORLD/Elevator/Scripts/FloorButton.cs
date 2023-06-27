using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections;

public class FloorButton : UdonSharpBehaviour
{
    public UdonElevatorController elevatorController;
    public int targetFloor;

    private bool isWaitingForElevator = false;

    private void Start()
    {
        if (elevatorController == null)
        {
            Debug.LogError("Elevator Controller is not assigned to the FloorButton!");
        }
    }

    public override void Interact()
    {
        base.Interact();

        if (elevatorController != null)
        {
            if (elevatorController.IsElevatorMoving())
            {
                isWaitingForElevator = true;
            }
            else
            {
                elevatorController.GoToFloor(targetFloor);
            }
        }
    }

    private void Update()
    {
        if (isWaitingForElevator && !elevatorController.IsElevatorMoving())
        {
            elevatorController.GoToFloor(targetFloor);
            isWaitingForElevator = false;
        }
    }
}

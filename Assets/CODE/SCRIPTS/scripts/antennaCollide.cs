// // using System.Diagnostics;
// // using System.Drawing;
// using UdonSharp;
// using UnityEngine;
// using UnityEngine.UI;
// using VRC.SDKBase;
// using VRC.Udon;

// public class antennaCollide : UdonSharpBehaviour
// {
//     [Header("floors 0 - 5")]
//     public LayerMask[] interactableLayerAnt;
//     public elevatorBrains elevatorBrains;
//     [SerializeField] private Color theColor;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (interactableLayerAnt[0] == (interactableLayerAnt[0] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 0");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[0].Play("0FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 0;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//         else if (interactableLayerAnt[1] == (interactableLayerAnt[1] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 1");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[1].Play("1FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 1;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//         else if (interactableLayerAnt[2] == (interactableLayerAnt[2] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 2");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[2].Play("2FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 2;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//         else if (interactableLayerAnt[3] == (interactableLayerAnt[3] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 3");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[3].Play("3FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 3;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//         else if (interactableLayerAnt[4] == (interactableLayerAnt[4] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 4");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[4].Play("4FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 4;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//         else if (interactableLayerAnt[5] == (interactableLayerAnt[5] | (1 << other.gameObject.layer)))
//         {
//             Debug.Log("Collided with an interactable object. 5");
//             elevatorBrains.MovementCheck = false;
//             elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
//             elevatorBrains.closeOutsideDoors[5].Play("5FloorDoorsOpen");
//             elevatorBrains.alreadyMoving = false;
//             elevatorBrains.floorCurrent = 5;
//             SendCustomEventDelayedSeconds("netSendB", 0.2f);
//             SendCustomEventDelayedSeconds("clearArrowColor", 0.1f);
//         }
//     }

//     private void clearArrowColor()
//     {
//         elevatorBrains.blinkAnt = false;
//         Debug.Log("clearArrowColorSent");
//         elevatorBrains.floorsText[0].color = theColor;
//         elevatorBrains.floorsText[1].color = theColor;
//         elevatorBrains.floorsText[2].color = theColor;
//         elevatorBrains.floorsText[3].color = theColor;
//         elevatorBrains.floorsText[4].color = theColor;
//         elevatorBrains.floorsText[5].color = theColor;
//         elevatorBrains.floorsText[6].color = theColor;
//         elevatorBrains.buttonMaterials[0].color = Color.green;
//         elevatorBrains.buttonMaterials[1].color = Color.green;
//         elevatorBrains.buttonMaterials[2].color = Color.green;
//         elevatorBrains.buttonMaterials[3].color = Color.green;
//         elevatorBrains.buttonMaterials[4].color = Color.green;
//         elevatorBrains.buttonMaterials[5].color = Color.green;
//     }

//     private void netSendB()
//     {
//         elevatorBrains.closeOutsideDoors[0].Play("ElevatorDoorsOpen");
//         elevatorBrains.resetColorArrows();
//         Debug.Log("doors OPENED");
//     }


// }

// using System.Diagnostics;
// using System.Drawing;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class antennaCollide : UdonSharpBehaviour
{
    [Header("floors 0 - 5")]
    public LayerMask[] interactableLayerAnt;
    public elevatorBrains elevatorBrains;
    [SerializeField] private Color theColor;
    [SerializeField] private string theText;
    [SerializeField] private bool itEquals;
    private bool goOnce;

    private void Start()
    {
        theText = "F 1";
    }

    private void Update()
    {
        if (goOnce == true)
        {
            Debug.Log("setFloorCurrent");
            elevatorBrains.floorCurrent = 1;
        }
        elevatorBrains.floorsText[0].text = theText;
        elevatorBrains.floorsText[1].text = theText;
        elevatorBrains.floorsText[2].text = theText;
        elevatorBrains.floorsText[3].text = theText;
        elevatorBrains.floorsText[4].text = theText;
        elevatorBrains.floorsText[5].text = theText;
        elevatorBrains.floorsText[6].text = theText;
        if (elevatorBrains.buttonPress == elevatorBrains.floorCurrent)
        {
            itEquals = true;
        }
        else
        {
            itEquals = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactableLayerAnt[0] == (interactableLayerAnt[0] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 0");
            elevatorBrains.floorCurrent = 0;
            theText = "F 0";
            SendCustomEventDelayedSeconds("FL0", 0.3f);
        }
        else if (interactableLayerAnt[1] == (interactableLayerAnt[1] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 1");
            elevatorBrains.floorCurrent = 1;
            theText = "F 1";
            SendCustomEventDelayedSeconds("FL1", 0.3f);
        }
        else if (interactableLayerAnt[2] == (interactableLayerAnt[2] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 2");
            elevatorBrains.floorCurrent = 2;
            theText = "F 2";
            SendCustomEventDelayedSeconds("FL2", 0.3f);
        }
        else if (interactableLayerAnt[3] == (interactableLayerAnt[3] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 3");
            elevatorBrains.floorCurrent = 3;
            theText = "F 3";
            SendCustomEventDelayedSeconds("FL3", 0.3f);
        }
        else if (interactableLayerAnt[4] == (interactableLayerAnt[4] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 4");
            elevatorBrains.floorCurrent = 4;
            theText = "F 4";
            SendCustomEventDelayedSeconds("FL4", 0.3f);
        }
        else if (interactableLayerAnt[5] == (interactableLayerAnt[5] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 5");
            elevatorBrains.floorCurrent = 5;
            theText = "F 5";
            SendCustomEventDelayedSeconds("FL5", 0.3f);
        }
    }

    public void FL0()
    {
        Debug.Log("exec0");
        if (itEquals == true)
        {
            Debug.Log("exec0DONE");
            Debug.Log("Collided with an interactable object. 0");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[0].Play("0FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 0;
            netSendB();
            clearArrowColor();
        }
    }

    public void FL1()
    {
        Debug.Log("exec1");
        if (itEquals == true)
        {
            Debug.Log("exec1DONE");
            Debug.Log("Collided with an interactable object. 1");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[1].Play("1FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 1;
            netSendB();
            clearArrowColor();
        }
    }

    public void FL2()
    {
        Debug.Log("exec2");
        if (itEquals == true)
        {
            Debug.Log("exec2DONE");
            Debug.Log("Collided with an interactable object. 2");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[2].Play("2FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 2;
            netSendB();
            clearArrowColor();
        }
    }

    public void FL3()
    {
        Debug.Log("exec3");
        if (itEquals == true)
        {
            Debug.Log("exec3DONE");
            Debug.Log("Collided with an interactable object. 3");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[3].Play("3FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 3;
            netSendB();
            clearArrowColor();
        }
    }

    public void FL4()
    {
        Debug.Log("exec4");
        if (itEquals == true)
        {
            Debug.Log("exec4DONE");
            Debug.Log("Collided with an interactable object. 4");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[4].Play("4FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 4;
            netSendB();
            clearArrowColor();
        }
    }

    public void FL5()
    {
        Debug.Log("exec5");
        if (itEquals == true)
        {
            Debug.Log("exec5DONE");
            Debug.Log("Collided with an interactable object. 5");
            elevatorBrains.MovementCheck = false;
            elevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            elevatorBrains.closeOutsideDoors[5].Play("5FloorDoorsOpen");
            elevatorBrains.alreadyMoving = false;
            elevatorBrains.floorCurrent = 5;
            netSendB();
            clearArrowColor();
        }
    }

    private void clearArrowColor()
    {
        elevatorBrains.blinkAnt = false;
        Debug.Log("clearArrowColorSent");
        elevatorBrains.floorsText[0].color = theColor;
        elevatorBrains.floorsText[1].color = theColor;
        elevatorBrains.floorsText[2].color = theColor;
        elevatorBrains.floorsText[3].color = theColor;
        elevatorBrains.floorsText[4].color = theColor;
        elevatorBrains.floorsText[5].color = theColor;
        elevatorBrains.floorsText[6].color = theColor;
        elevatorBrains.buttonMaterials[0].color = Color.green;
        elevatorBrains.buttonMaterials[1].color = Color.green;
        elevatorBrains.buttonMaterials[2].color = Color.green;
        elevatorBrains.buttonMaterials[3].color = Color.green;
        elevatorBrains.buttonMaterials[4].color = Color.green;
        elevatorBrains.buttonMaterials[5].color = Color.green;
        elevatorBrains.loadEvevatorFloor = elevatorBrains.floorCurrent;
    }

    private void netSendB()
    {
        elevatorBrains.closeOutsideDoors[0].Play("ElevatorDoorsOpen");
        elevatorBrains.resetColorArrows();
        Debug.Log("doors OPENED");
    }


}

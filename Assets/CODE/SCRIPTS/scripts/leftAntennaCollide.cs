using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class leftAntennaCollide : UdonSharpBehaviour
{
    [Header("floors 0 - 5")]
    public LayerMask[] interactableLayerAnt;
    [SerializeField] private Color theColor;
    [SerializeField] private string theText;
    public leftElevatorBrains leftElevatorBrains;
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
            leftElevatorBrains.floorCurrent = 1;
        }
        leftElevatorBrains.floorsText[0].text = theText;
        leftElevatorBrains.floorsText[1].text = theText;
        leftElevatorBrains.floorsText[2].text = theText;
        leftElevatorBrains.floorsText[3].text = theText;
        leftElevatorBrains.floorsText[4].text = theText;
        leftElevatorBrains.floorsText[5].text = theText;
        leftElevatorBrains.floorsText[6].text = theText;
        if (leftElevatorBrains.buttonPress == leftElevatorBrains.floorCurrent)
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
        leftElevatorBrains.goOnceFunc();
        leftElevatorBrains.isON = true;
        if (interactableLayerAnt[0] == (interactableLayerAnt[0] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 0");
            leftElevatorBrains.floorCurrent = 0;
            theText = "F 0";
            SendCustomEventDelayedSeconds("FL0", 0.3f);
        }
        else if (interactableLayerAnt[1] == (interactableLayerAnt[1] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 1");
            leftElevatorBrains.floorCurrent = 1;
            theText = "F 1";
            SendCustomEventDelayedSeconds("FL1", 0.3f);
        }
        else if (interactableLayerAnt[2] == (interactableLayerAnt[2] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 2");
            leftElevatorBrains.floorCurrent = 2;
            theText = "F 2";
            SendCustomEventDelayedSeconds("FL2", 0.3f);
        }
        else if (interactableLayerAnt[3] == (interactableLayerAnt[3] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 3");
            leftElevatorBrains.floorCurrent = 3;
            theText = "F 3";
            SendCustomEventDelayedSeconds("FL3", 0.3f);
        }
        else if (interactableLayerAnt[4] == (interactableLayerAnt[4] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 4");
            leftElevatorBrains.floorCurrent = 4;
            theText = "F 4";
            SendCustomEventDelayedSeconds("FL4", 0.3f);
        }
        else if (interactableLayerAnt[5] == (interactableLayerAnt[5] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 5");
            leftElevatorBrains.floorCurrent = 5;
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
            Debug.Log("hit floor 0");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[0].Play("0FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 0;
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
            Debug.Log("hit floor 1");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[1].Play("1FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 1;
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
            Debug.Log("hit floor 2");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[2].Play("2FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 2;
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
            Debug.Log("hit floor 3");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[3].Play("3FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 3;
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
            Debug.Log("hit floor 4");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[4].Play("4FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 4;
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
            Debug.Log("hit floor 5");
            leftElevatorBrains.MovementCheck = false;
            leftElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            leftElevatorBrains.closeOutsideDoors[5].Play("5FloorDoorsOpen");
            leftElevatorBrains.alreadyMoving = false;
            leftElevatorBrains.floorCurrent = 5;
            netSendB();
            clearArrowColor();
        }
    }

    private void clearArrowColor()
    {
        leftElevatorBrains.blinkAnt = false;
        Debug.Log("clearArrowColorSent");
        leftElevatorBrains.floorsText[0].color = theColor;
        leftElevatorBrains.floorsText[1].color = theColor;
        leftElevatorBrains.floorsText[2].color = theColor;
        leftElevatorBrains.floorsText[3].color = theColor;
        leftElevatorBrains.floorsText[4].color = theColor;
        leftElevatorBrains.floorsText[5].color = theColor;
        leftElevatorBrains.floorsText[6].color = theColor;
        leftElevatorBrains.buttonMaterials[0].color = Color.green;
        leftElevatorBrains.buttonMaterials[1].color = Color.green;
        leftElevatorBrains.buttonMaterials[2].color = Color.green;
        leftElevatorBrains.buttonMaterials[3].color = Color.green;
        leftElevatorBrains.buttonMaterials[4].color = Color.green;
        leftElevatorBrains.buttonMaterials[5].color = Color.green;
        leftElevatorBrains.loadElevatorFloor = leftElevatorBrains.floorCurrent;
    }

    private void netSendB()
    {
        leftElevatorBrains.closeOutsideDoors[0].Play("ElevatorDoorsOpen");
        leftElevatorBrains.resetColorArrows();
        Debug.Log("doors OPENED");
    }


}

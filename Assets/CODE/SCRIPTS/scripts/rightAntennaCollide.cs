using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class rightAntennaCollide : UdonSharpBehaviour
{
    [Header("floors 0 - 5")]
    public LayerMask[] interactableLayerAnt;
    [SerializeField] private Color theColor;
    [SerializeField] private string theText;
    public rightElevatorBrains rightElevatorBrains;
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
            rightElevatorBrains.floorCurrent = 1;
        }
        rightElevatorBrains.floorsText[0].text = theText;
        rightElevatorBrains.floorsText[1].text = theText;
        rightElevatorBrains.floorsText[2].text = theText;
        rightElevatorBrains.floorsText[3].text = theText;
        rightElevatorBrains.floorsText[4].text = theText;
        rightElevatorBrains.floorsText[5].text = theText;
        rightElevatorBrains.floorsText[6].text = theText;
        if (rightElevatorBrains.buttonPress == rightElevatorBrains.floorCurrent)
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
        rightElevatorBrains.goOnceFunc();
        rightElevatorBrains.isON = true;
        if (interactableLayerAnt[0] == (interactableLayerAnt[0] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 0");
            rightElevatorBrains.floorCurrent = 0;
            theText = "F 0";
            SendCustomEventDelayedSeconds("FL0", 0.3f);
        }
        else if (interactableLayerAnt[1] == (interactableLayerAnt[1] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 1");
            rightElevatorBrains.floorCurrent = 1;
            theText = "F 1";
            SendCustomEventDelayedSeconds("FL1", 0.3f);
        }
        else if (interactableLayerAnt[2] == (interactableLayerAnt[2] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 2");
            rightElevatorBrains.floorCurrent = 2;
            theText = "F 2";
            SendCustomEventDelayedSeconds("FL2", 0.3f);
        }
        else if (interactableLayerAnt[3] == (interactableLayerAnt[3] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 3");
            rightElevatorBrains.floorCurrent = 3;
            theText = "F 3";
            SendCustomEventDelayedSeconds("FL3", 0.3f);
        }
        else if (interactableLayerAnt[4] == (interactableLayerAnt[4] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 4");
            rightElevatorBrains.floorCurrent = 4;
            theText = "F 4";
            SendCustomEventDelayedSeconds("FL4", 0.3f);
        }
        else if (interactableLayerAnt[5] == (interactableLayerAnt[5] | (1 << other.gameObject.layer)))
        {
            Debug.Log("got Floor 5");
            rightElevatorBrains.floorCurrent = 5;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[0].Play("0FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 0;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[1].Play("1FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 1;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[2].Play("2FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 2;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[3].Play("3FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 3;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[4].Play("4FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 4;
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
            rightElevatorBrains.MovementCheck = false;
            rightElevatorBrains.EleDoors.Play("ElevatorDoorsOpen");
            rightElevatorBrains.closeOutsideDoors[5].Play("5FloorDoorsOpen");
            rightElevatorBrains.alreadyMoving = false;
            rightElevatorBrains.floorCurrent = 5;
            netSendB();
            clearArrowColor();
        }
    }

    private void clearArrowColor()
    {
        rightElevatorBrains.blinkAnt = false;
        Debug.Log("clearArrowColorSent");
        rightElevatorBrains.floorsText[0].color = theColor;
        rightElevatorBrains.floorsText[1].color = theColor;
        rightElevatorBrains.floorsText[2].color = theColor;
        rightElevatorBrains.floorsText[3].color = theColor;
        rightElevatorBrains.floorsText[4].color = theColor;
        rightElevatorBrains.floorsText[5].color = theColor;
        rightElevatorBrains.floorsText[6].color = theColor;
        rightElevatorBrains.buttonMaterials[0].color = Color.green;
        rightElevatorBrains.buttonMaterials[1].color = Color.green;
        rightElevatorBrains.buttonMaterials[2].color = Color.green;
        rightElevatorBrains.buttonMaterials[3].color = Color.green;
        rightElevatorBrains.buttonMaterials[4].color = Color.green;
        rightElevatorBrains.buttonMaterials[5].color = Color.green;
        rightElevatorBrains.loadElevatorFloor = rightElevatorBrains.floorCurrent;
    }

    private void netSendB()
    {
        rightElevatorBrains.closeOutsideDoors[0].Play("ElevatorDoorsOpen");
        rightElevatorBrains.resetColorArrows();
        Debug.Log("doors OPENED");
    }


}

using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class callElevator : UdonSharpBehaviour
{
    [SerializeField] private leftElevatorBrains elevatorBrainsLeft;
    [SerializeField] private rightElevatorBrains elevatorBrainsRight;
    [SerializeField] private int EBL;
    [SerializeField] private int EBR;
    [SerializeField] private int currentFloorLeft;
    [SerializeField] private int currentFloorRight;
    [SerializeField] private bool isActive = false;
    [SerializeField] private int buttonPress = 1;
    [SerializeField] private string callString;

    private void Update()
    {
        currentFloorLeft = elevatorBrainsLeft.floorCurrent;
        currentFloorRight = elevatorBrainsRight.floorCurrent;
        EBL = buttonPress - currentFloorLeft;
        EBR = buttonPress - currentFloorRight;
    }

    public void onInteractinz()
    {
        if (isActive == true)
        {
            Debug.Log("calledElevator");
            if (EBL > EBR)
            {
                Debug.Log("leftElevator");

                if (buttonPress == 0)
                {
                    elevatorBrainsLeft.F0();
                    isActive = false;
                }
                else if (buttonPress == 1)
                {
                    elevatorBrainsLeft.F1();
                    isActive = false;
                }
                else if (buttonPress == 2)
                {
                    elevatorBrainsLeft.F2();
                    isActive = false;
                }
                else if (buttonPress == 3)
                {
                    elevatorBrainsLeft.F3();
                    isActive = false;
                }
                else if (buttonPress == 4)
                {
                    elevatorBrainsLeft.F4();
                    isActive = false;
                }
                else if (buttonPress == 5)
                {
                    elevatorBrainsLeft.F5();
                    isActive = false;
                }
            }
            else if (EBL < EBR)
            {
                Debug.Log("rightElevator");
                if (buttonPress == 0)
                {
                    elevatorBrainsRight.F0();
                    isActive = false;
                }
                else if (buttonPress == 1)
                {
                    elevatorBrainsRight.F1();
                    isActive = false;
                }
                else if (buttonPress == 2)
                {
                    elevatorBrainsRight.F2();
                    isActive = false;
                }
                else if (buttonPress == 3)
                {
                    elevatorBrainsRight.F3();
                    isActive = false;
                }
                else if (buttonPress == 4)
                {
                    elevatorBrainsRight.F4();
                    isActive = false;
                }
                else if (buttonPress == 5)
                {
                    elevatorBrainsRight.F5();
                    isActive = false;
                }
            }
            else
            {
                Debug.Log("lastlyRightElevator");
                if (buttonPress == 0)
                {
                    elevatorBrainsRight.F0();
                    isActive = false;
                }
                else if (buttonPress == 1)
                {
                    elevatorBrainsRight.F1();
                    isActive = false;
                }
                else if (buttonPress == 2)
                {
                    elevatorBrainsRight.F2();
                    isActive = false;
                }
                else if (buttonPress == 3)
                {
                    elevatorBrainsRight.F3();
                    isActive = false;
                }
                else if (buttonPress == 4)
                {
                    elevatorBrainsRight.F4();
                    isActive = false;
                }
                else if (buttonPress == 5)
                {
                    elevatorBrainsRight.F5();
                    isActive = false;
                }
            }
        }
    }

    public void F0()
    {
        isActive = true;
        buttonPress = 0;
        // isActive = true;
        callString = "F0";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }

    public void F1()
    {
        isActive = true;
        buttonPress = 1;
        // isActive = true;
        callString = "F1";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }

    public void F2()
    {
        isActive = true;
        buttonPress = 2;
        // isActive = true;
        callString = "F2";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }

    public void F3()
    {
        isActive = true;
        buttonPress = 3;
        // isActive = true;
        callString = "F3";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }

    public void F4()
    {
        isActive = true;
        buttonPress = 4;
        // isActive = true;
        callString = "F4";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }

    public void F5()
    {
        isActive = true;
        buttonPress = 5;
        // isActive = true;
        callString = "F5";
        SendCustomEventDelayedSeconds("onInteractinz", 2f);
    }
}
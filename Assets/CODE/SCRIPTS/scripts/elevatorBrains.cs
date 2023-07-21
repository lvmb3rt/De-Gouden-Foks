using System.Threading;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections;

public class elevatorBrains : UdonSharpBehaviour
{
    [Header("Cooldown so you can't spam the buttons")]
    public int elevatorCooldown;
    public int ButtonCooldown;
    [Header("Counts when up or down is needed to flash")]
    public int flashUP;
    public int flashDOWN;
    [Header("current floor of elevator")]
    public int elevatorFloorCurrent;
    [Header("floor you want the elevator to go to")]
    public int targetFloor;
    [Header("Choose your floor values")]
    public int FZero;
    public int FOne;
    public int FTwo;
    public int FThree;
    public int FFour;
    public int FFive;
    [Header("Get the elevators transform to move it within script")]
    public Transform elevator;
    [Header("The Text above the elevators entrance")]
    public Text canvasFloorZero;
    public Text canvasFloorOne;
    public Text canvasFloorTwo;
    public Text canvasFloorThree;
    public Text canvasFloorFour;
    public Text canvasFloorFive;
    [Header("Animations")]
    public Animator ElevatorAnim;
    public Animator ZeroAnim;
    public Animator OneAnim;
    public Animator TwoAnim;
    public Animator ThreeAnim;
    public Animator FourAnim;
    public Animator FiveAnim;
    [Header("Elevator home positions")]
    public Vector3 ZeroHome;
    public Vector3 OneHome;
    public Vector3 TwoHome;
    public Vector3 ThreeHome;
    public Vector3 FourHome;
    public Vector3 FiveHome;
    [Header("These images will be used to show when going up or down")]
    public RawImage UpF0;
    public RawImage UpF1;
    public RawImage UpF2;
    public RawImage UpF3;
    public RawImage UpF4;
    public RawImage UpF5;
    public RawImage DownF0;
    public RawImage DownF1;
    public RawImage DownF2;
    public RawImage DownF3;
    public RawImage DownF4;
    public RawImage DownF5;
    [Header("Gets the color for the back of the button")]
    public Material ButtonBACK;

    public void Start()
    {
        FZero = 7;
        FOne = 7;
        FTwo = 7;
        FThree = 7;
        FFour = 7;
        FFive = 7;
        targetFloor = 1;
        elevatorFloorCurrent = 1;
        elevatorCooldown = 0;
        ElevatorAnim.Play("ElevatorDoorsOpen");
        OneAnim.Play("1FloorDoorsOpen");
    }

    public void Update()
    {
        if (ButtonCooldown > 0)
        {
            ButtonBACK.color = Color.red;
        }
        else
        {
            ButtonBACK.color = Color.green;
        }
        FlashingUP();
        FlashingDOWN();
        if (FZero == 1)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ZeroHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FZDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC0", 10);
        }
        if (FZero == 2)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ZeroHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FZDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC0", 20);
        }
        if(FZero == 3)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ZeroHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FZDO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC0", 30);
        }
        if(FZero == 4)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ZeroHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 40);
            SendCustomEventDelayedSeconds("openDoors", 40);
            SendCustomEventDelayedSeconds("FZDO", 40.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 40);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 40);
            SendCustomEventDelayedSeconds("EFC0", 40);
        }
        if(FZero == 5)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ZeroHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 50);
            SendCustomEventDelayedSeconds("openDoors", 50);
            SendCustomEventDelayedSeconds("FZDO", 50.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 50);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 50);
            SendCustomEventDelayedSeconds("EFC0", 50);
        }
        //--------------------------------------
        if (FOne == 0)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, OneHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FODO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC1", 10);
        }
        if(FOne == 2)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, OneHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FODO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC1", 10);
        }
        if(FOne == 3)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, OneHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FODO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC1", 20);
        }
        if(FOne == 4)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, OneHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FODO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC1", 30);
        }
        if(FOne == 5)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, OneHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 40);
            SendCustomEventDelayedSeconds("openDoors", 40);
            SendCustomEventDelayedSeconds("FODO", 40.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 40);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 40);
            SendCustomEventDelayedSeconds("EFC1", 40);
        }
        //--------------------------------------
        if (FTwo == 0)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, TwoHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FTDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC2", 20);
        }
        if(FTwo == 1)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, TwoHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FTDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC2", 10);
        }
        if(FTwo == 3)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, TwoHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FTDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC2", 10);
        }
        if(FTwo == 4)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, TwoHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FTDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC2", 20);
        }
        if(FTwo == 5)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, TwoHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FTDO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC2", 30);
        }
        //--------------------------------------
        if (FThree == 0)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ThreeHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FTHDO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC3", 30);
        }
        if(FThree == 1)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ThreeHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FTHDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC3", 20);
        }
        if(FThree == 2)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ThreeHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FTHDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC3", 10);
        }
        if(FThree == 4)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ThreeHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FTHDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC3", 10);
        }
        if(FThree == 5)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, ThreeHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FTHDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC3", 20);
        }
        //--------------------------------------
        if (FFour == 0)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FourHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 40);
            SendCustomEventDelayedSeconds("openDoors", 40);
            SendCustomEventDelayedSeconds("FFDO", 40.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 40);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 40);
            SendCustomEventDelayedSeconds("EFC4", 40);
        }
        if(FFour == 1)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FourHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FFDO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC4", 30);
        }
        if(FFour == 2)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FourHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FFDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC4", 20);
        }
        if(FFour == 3)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FourHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FFDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC4", 10);
        }
        if(FFour == 5)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FourHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FFDO", 10.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC4", 10);
        }
        //--------------------------------------
        if (FFive == 0)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FiveHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 50);
            SendCustomEventDelayedSeconds("openDoors", 50);
            SendCustomEventDelayedSeconds("FFIDO", 50.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 50);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 50);
            SendCustomEventDelayedSeconds("EFC5", 50);
        }
        if(FFive == 1)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FiveHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 40);
            SendCustomEventDelayedSeconds("openDoors", 40);
            SendCustomEventDelayedSeconds("FFIDO", 40.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 40);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 40);
            SendCustomEventDelayedSeconds("EFC5", 40);
        }
        if(FFive == 2)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FiveHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 30);
            SendCustomEventDelayedSeconds("openDoors", 30);
            SendCustomEventDelayedSeconds("FFIDO", 30.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 30);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 30);
            SendCustomEventDelayedSeconds("EFC5", 30);
        }
        if(FFive == 3)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FiveHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 20);
            SendCustomEventDelayedSeconds("openDoors", 20);
            SendCustomEventDelayedSeconds("FFIDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 20);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 20);
            SendCustomEventDelayedSeconds("EFC5", 20);
        }
        if(FFive == 4)
        {
            elevator.position = Vector3.MoveTowards(elevator.position, FiveHome, 1 * Time.deltaTime);
            SendCustomEventDelayedSeconds("ResetAllFloorMovements", 10);
            SendCustomEventDelayedSeconds("openDoors", 10);
            SendCustomEventDelayedSeconds("FFIDO", 20.5f);
            SendCustomEventDelayedSeconds("FlashingDOWNSTOP", 10);
            SendCustomEventDelayedSeconds("FlashingUPSTOP", 10);
            SendCustomEventDelayedSeconds("EFC5", 10);
        }

    }

    public void EFC0()
    {
        elevatorFloorCurrent = 0;
    }
    public void EFC1()
    {
        elevatorFloorCurrent = 1;
    }
    public void EFC2()
    {
        elevatorFloorCurrent = 2;
    }
    public void EFC3()
    {
        elevatorFloorCurrent = 3;
    }
    public void EFC4()
    {
        elevatorFloorCurrent = 4;
    }
    public void EFC5()
    {
        elevatorFloorCurrent = 5;
    }

    public void FlashingDOWNSTOP()
    {
        flashDOWN = 0;
    }

    public void FlashingUPSTOP()
    {
        flashUP = 0;
    }

    public void FlashingUP()
    {
        if (flashUP == 1)
        {
            UpF0.color = Color.red;
            UpF1.color = Color.red;
            UpF2.color = Color.red;
            UpF3.color = Color.red;
            UpF4.color = Color.red;
            UpF5.color = Color.red;
            // UpF1.SetActive(false);
            // UpF2.SetActive(false);
            // UpF3.SetActive(false);
            // SendCustomEventDelayedSeconds("toggleTrue", 1);
        }
        else
        {
            UpF0.color = Color.black;
            UpF1.color = Color.black;
            UpF2.color = Color.black;
            UpF3.color = Color.black;
            UpF4.color = Color.black;
            UpF5.color = Color.black;
        }
    }

    // public void toggleTrue()
    // {
    //     if (flashUP == 1)
    //     {
    //         UpF1.SetActive(true);
    //         UpF2.SetActive(true);
    //         UpF3.SetActive(true);
    //         SendCustomEventDelayedSeconds("FlashingUP", 2);
    //     }
    // }

    // public void toggleFalse()
    // {
    //     if (flashDOWN == 1)
    //     {
    //         DownF1.SetActive(true);
    //         DownF2.SetActive(true);
    //         DownF3.SetActive(true);
    //         SendCustomEventDelayedSeconds("FlashingDOWN", 2);
    //     }
    // }

    public void FlashingDOWN()
    {
        if (flashDOWN == 1)
        {
            DownF0.color = Color.red;
            DownF1.color = Color.red;
            DownF2.color = Color.red;
            DownF3.color = Color.red;
            DownF4.color = Color.red;
            DownF5.color = Color.red;
            // DownF1.SetActive(false);
            // DownF2.SetActive(false);
            // DownF3.SetActive(false);
            // SendCustomEventDelayedSeconds("toggleFalse", 1);
        }
        else
        {
            DownF0.color = Color.black;
            DownF1.color = Color.black;
            DownF2.color = Color.black;
            DownF3.color = Color.black;
            DownF4.color = Color.black;
            DownF5.color = Color.black;
        }
    }

    public void ResetAllFloorMovements()
    {
        FZero = 7;
        FOne = 7;
        FTwo = 7;
        FThree = 7;
        FFour = 7;
        FFive = 7;
    }

    public void cooldownCountDown()
    {
        if (elevatorCooldown >= 1)
        {
            elevatorCooldown -= 1;
            SendCustomEventDelayedSeconds("cooldownCountDown", 1);
        }
        // if (elevatorCooldown == 4) // NOW BEING CONTROLLED BY THE UPDATE FUNCTION;
        // {
        //     Debug.Log("MOVED TO YOUR POSITION");
        //             ElevatorAnim.Play("ElevatorDoorsOpen");
        // }
        else
        {
            Debug.Log("Elevator Ready For Operation");
        }
    }

    public void buttonCoolDown()
    {
        if (ButtonCooldown >= 1)
        {
            ButtonCooldown -= 1;
            SendCustomEventDelayedSeconds("buttonCoolDown", 1);
        }
        // if (elevatorCooldown == 4) // NOW BEING CONTROLLED BY THE UPDATE FUNCTION;
        // {
        //     Debug.Log("MOVED TO YOUR POSITION");
        //             ElevatorAnim.Play("ElevatorDoorsOpen");
        // }
        else
        {
            Debug.Log("Button Ready For Operation");
        }
    }

    public void FZDO() // Floor zero door open
    {
        ZeroAnim.Play("0FloorDoorsOpen");
    }
    public void FODO() // Floor one door open
    {
        OneAnim.Play("1FloorDoorsOpen");
    }
    public void FTDO() // Floor two door open
    {
        TwoAnim.Play("2FloorDoorsOpen");
    }
    public void FTHDO() // Floor three door open
    {
        ThreeAnim.Play("3FloorDoorsOpen");
    }
    public void FFDO() // Floor four door open
    {
        FourAnim.Play("4FloorDoorsOpen");
    }
    public void FFIDO() // Floor five door open
    {
        FiveAnim.Play("5FloorDoorsOpen");
    }
    // -------------------------------------
    public void FZDC() // Floor zero door close
    {
        ZeroAnim.Play("0FloorDoorsClose");
    }
    public void FODC() // Floor one door close
    {
        OneAnim.Play("1FloorDoorsClose");
    }
    public void FTDC() // Floor two door close
    {
        TwoAnim.Play("2FloorDoorsClose");
    }
    public void FTHDC() // Floor three door close
    {
        ThreeAnim.Play("3FloorDoorsClose");
    }
    public void FFDC() // Floor four door close
    {
        FourAnim.Play("4FloorDoorsClose");
    }
    public void FFIDC() // Floor five door close
    {
        FiveAnim.Play("5FloorDoorsClose");
    }

    public void openDoors()
    {
        ElevatorAnim.Play("ElevatorDoorsOpen");
    }

    public void ZeroOneOne()
    {
        flashDOWN = 1;
        FZero = 1;
        Debug.Log("Called ZeroOneOne");
    }
    public void ZeroOneTwo()
    {
        flashDOWN = 1;
        FZero = 2;
        Debug.Log("Called ZeroOneTwo");
    }
    public void ZeroOneThree()
    {
        flashDOWN = 1;
        FZero = 3;
        Debug.Log("Called ZeroOneThree");
    }
    public void ZeroOneFour()
    {
        flashDOWN = 1;
        FZero = 4;
        Debug.Log("Called ZeroOneFour");
    }
    public void ZeroOneFive()
    {
        flashDOWN = 1;
        FZero = 5;
        Debug.Log("Called ZeroOneFive");
    }
    //-------------------------------
    public void OneOneOne()
    {
        flashUP = 1;
        FOne = 0;
        Debug.Log("Called OneOneOne");
    }
    public void OneOneTwo()
    {
        flashDOWN = 1;
        FOne = 2;
        Debug.Log("Called OneOneTwo");
    }
    public void OneOneThree()
    {
        flashDOWN = 1;
        FOne = 3;
        Debug.Log("Called OneOneThree");
    }
    public void OneOneFour()
    {
        flashDOWN = 1;
        FOne = 4;
        Debug.Log("Called OneOneFour");
    }
    public void OneOneFive()
    {
        flashDOWN = 1;
        FOne = 5;
        Debug.Log("Called OneOneFive");
    }
    //-------------------------------
    public void TwoOneOne()
    {
        flashUP = 1;
        FTwo = 0;
        Debug.Log("Called TwoOneOne");
    }
    public void TwoOneTwo()
    {
        flashUP = 1;
        FTwo = 1;
        Debug.Log("Called TwoOneTwo");
    }
    public void TwoOneThree()
    {
        flashDOWN = 1;
        FTwo = 3;
        Debug.Log("Called TwoOneThree");
    }
    public void TwoOneFour()
    {
        flashDOWN = 1;
        FTwo = 4;
        Debug.Log("Called TwoOneFour");
    }
    public void TwoOneFive()
    {
        flashUP = 1;
        FTwo = 0;
        Debug.Log("Called TwoOneFive");
    }
    //-------------------------------
    public void ThreeOneOne()
    {
        flashUP = 1;
        FThree = 0;
        Debug.Log("Called ThreeOneOne");
    }
    public void ThreeOneTwo()
    {
        flashUP = 1;
        FThree = 1;
        Debug.Log("Called ThreeOneTwo");
    }
    public void ThreeOneThree()
    {
        flashUP = 1;
        FThree = 2;
        Debug.Log("Called ThreeOneThree");
    }
    public void ThreeOneFour()
    {
        flashDOWN = 1;
        FThree = 4;
        Debug.Log("Called ThreeOneFour");
    }
    public void ThreeOneFive()
    {
        flashDOWN = 1;
        FThree = 5;
        Debug.Log("Called ThreeOneFive");
    }
    public void FourOneOne()
    {
        flashUP = 1;
        FFour = 0;
        Debug.Log("Called FourOneOne");
    }
    public void FourOneTwo()
    {
        flashUP = 1;
        FFour = 1;
        Debug.Log("Called FourOneTwo");
    }
    public void FourOneThree()
    {
        flashUP = 1;
        FFour = 2;
        Debug.Log("Called FourOneThree");
    }
    public void FourOneFour()
    {
        flashUP = 1;
        FFour = 3;
        Debug.Log("Called FourOneFour");
    }
    public void FourOneFive()
    {
        flashDOWN = 1;
        FFour = 5;
        Debug.Log("Called FourOneFive");
    }

    public void FiveOneOne()
    {
        flashUP = 1;
        FFive = 0;
        Debug.Log("Called FiveOneOne");
    }
    public void FiveOneTwo()
    {
        flashUP = 1;
        FFive = 1;
        Debug.Log("Called FiveOneTwo");
    }
    public void FiveOneThree()
    {
        flashUP = 1;
        FFive = 2;
        Debug.Log("Called FiveOneThree");
    }
    public void FiveOneFour()
    {
        flashUP = 1;
        FFive = 3;
        Debug.Log("Called FiveOneFour");
    }
    public void FiveOneFive()
    {
        flashUP = 1;
        FFive = 4;
        Debug.Log("Called FiveOneFive");
    }

    public void FloorZero()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 1)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 0;
                Debug.Log("Requesting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ZeroOneOne", 0.5f);
                FODC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 2)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 0;
                flashDOWN = 1;
                FZero = 2;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ZeroOneTwo", 0.5f);
                FTDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 3)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 0;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ZeroOneThree", 0.5f);
                FTHDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 4)
            {
                elevatorCooldown = 40;
                ButtonCooldown = 42;
                targetFloor = 0;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ZeroOneFour", 0.5f);
                FFDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 5)
            {
                elevatorCooldown = 50;
                ButtonCooldown = 52;
                targetFloor = 0;
                Debug.Log("Requesting From Floor Five");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ZeroOneFive", 0.5f);
                FFIDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
        }
        else
        {
            Debug.Log("0 Button Not ready");
        }
    }

    public void FloorOne()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 0)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 1;
                Debug.Log("Requesting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("OneOneOne", 0.5f);
                FZDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 2)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 1;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("OneOneTwo", 0.5f);
                FTDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 3)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 1;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("OneOneThree", 0.5f);
                FTHDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 4)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 1;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("OneOneFour", 0.5f);
                FFDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 5)
            {
                elevatorCooldown = 40;
                ButtonCooldown = 42;
                targetFloor = 1;
                Debug.Log("Requesting From Floor Five");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("OneOneFive", 0.5f);
                FFIDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
        }
        else
        {
            Debug.Log("1 Button Not ready");
        }
    }

    public void FloorTwo()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 0)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 2;
                flashUP = 1;
                FTwo = 0;
                Debug.Log("Requesting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("TwoOneOne", 0.5f);
                FZDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 1)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 2;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("TwoOneTwo", 0.5f);
                FODC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 3)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 2;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("TwoOneThree", 0.5f);
                FTHDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 4)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 2;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("TwoOneFour", 0.5f);
                FFDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if(elevatorFloorCurrent == 5)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 2;
                flashDOWN = 1;
                FTwo = 5;
                Debug.Log("Requesting From Floor Five");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("TwoOneFive", 0.5f);
                FFIDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
        }
        else
        {
            Debug.Log("2 Button Not ready");
        }
    }

    public void FloorThree()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 0)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 3;
                Debug.Log("Requesting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ThreeOneOne", 0.5f);
                FZDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 1)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 3;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ThreeOneTwo", 0.5f);
                FODC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 2)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 3;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ThreeOneThree", 0.5f);
                FTDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 4)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 3;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("ThreeOneFour", 0.5f);
                FFDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 5)
            {
                elevatorCooldown = 20; // works
                ButtonCooldown = 22; // works
                targetFloor = 3;
                Debug.Log("Requesting From Floor Five"); // works
                ElevatorAnim.Play("ElevatorDoorsClose"); // works
                SendCustomEventDelayedSeconds("ThreeOneFive", 0.5f);
                FFIDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                buttonCoolDown(); // works
                cooldownCountDown(); // works
            }
        }
        else
        {
            Debug.Log("3 Button Not ready");
        }
    }

    public void FloorFour()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 0)
            {
                elevatorCooldown = 40;
                ButtonCooldown = 42;
                targetFloor = 4;
                Debug.Log("Requsting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FourOneOne", 0.5f);
                FZDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 1)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 4;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FourOneTwo", 0.5f);
                FODC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 2)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 4;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FourOneThree", 0.5f);
                FTDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 3)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 4;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FourOneFour", 0.5f);
                FTHDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 5)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 4;
                Debug.Log("Requesting From Floor Five");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FourOneFive", 0.5f);
                FFIDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
        }
        else
        {
            Debug.Log("4 Button Not ready");
        }
    }

    public void FloorFive()
    {
        if (ButtonCooldown == 0 && elevatorCooldown == 0)
        {
            if (elevatorFloorCurrent == 0)
            {
                elevatorCooldown = 50;
                ButtonCooldown = 52;
                targetFloor = 5;
                Debug.Log("Requesting From Floor One");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FiveOneOne", 0.5f);
                FZDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 1)
            {
                elevatorCooldown = 40;
                ButtonCooldown = 42;
                targetFloor = 5;
                Debug.Log("Requesting From Floor Two");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FiveOneTwo", 0.5f);
                FODC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 2)
            {
                elevatorCooldown = 30;
                ButtonCooldown = 32;
                targetFloor = 5;
                Debug.Log("Requesting From Floor Three");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FiveOneThree", 0.5f);
                FTDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 3)
            {
                elevatorCooldown = 20;
                ButtonCooldown = 22;
                targetFloor = 5;
                Debug.Log("Requesting From Floor Four");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FiveOneFour", 0.5f);
                FTHDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
            if (elevatorFloorCurrent == 4)
            {
                elevatorCooldown = 10;
                ButtonCooldown = 12;
                targetFloor = 0;
                Debug.Log("Requesting From Floor Five");
                ElevatorAnim.Play("ElevatorDoorsClose");
                SendCustomEventDelayedSeconds("FiveOneFive", 0.5f);
                FFDC();
                canvasFloorZero.text = "F  " + targetFloor.ToString();
                canvasFloorOne.text = "F  " + targetFloor.ToString();
                canvasFloorTwo.text = "F  " + targetFloor.ToString();
                canvasFloorThree.text = "F  " + targetFloor.ToString();
                canvasFloorFour.text = "F  " + targetFloor.ToString();
                canvasFloorFive.text = "F  " + targetFloor.ToString();
                cooldownCountDown();
                buttonCoolDown();
            }
        }
        else
        {
            Debug.Log("5 Button Not ready");
        }
    }
}

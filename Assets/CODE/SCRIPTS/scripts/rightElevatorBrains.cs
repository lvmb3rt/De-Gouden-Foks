// using System.Diagnostics;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.Components;


public class rightElevatorBrains : UdonSharpBehaviour
{

    // DEFAULT FLOOR IS 1

    [UdonSynced]
    public int loadElevatorFloor = 1;
    // [Header("Floor GameObjects 0-6")]
    // private float syncInterval = 5f; // Time in seconds between syncs
    // private float nextSyncTime;
    // [SerializeField] private GameObject[] floorsColliders;
    // [Header("Floor Homes 0-6")]
    // [SerializeField] private Transform[] floors;
    [Header("Button Materials 0-6")]
    public Material[] buttonMaterials;
    [Header("Animator for all floors doors")]
    public Animator[] closeOutsideDoors;
    [Header("Up/Down Arrows")]
    public RawImage[] upDowns;
    [Header("Get above elevator floors text")]
    public Text[] floorsText;
    [Header("constantFloorUpdates")]
    public Animator EleDoors;
    [SerializeField] private Transform elevatorObject;
    // [SerializeField] private GameObject antennaObject;
    [SerializeField] private Material antennaMaterial;
    // Get private variables ~~
    public int floorCurrent = 1;
    [SerializeField] private int goElevatorFloor;
    public int buttonPress;
    [SerializeField] private Color duringColor;
    public Color whiteArrowColor;
    public Color antColorWait;
    public Color antColorDuring;
    [SerializeField] private bool checkAlreadyGoing = false;
    public bool isON = false;
    public bool isGoing = false;
    public bool alreadyMoving = false;
    public bool MovementCheck = false;
    public bool blinkAnt;
    private bool enableBoth = false;
    public bool goOnce = false;

    // The basement is floor 0 due to not using "negative" numbers;
    // so floors are 0(basement), 1(firstfloor), 2(secondfloor), 3(thirdFloor), 4(fourthFloor), 5(fithFloor), 6(sixthFloor)

    private void Start()
    {
        isGoing = true;
        if (buttonPress != 1)
        {
            SendCustomEventDelayedSeconds("afterStart", 2);
        }

        antennaMaterial.SetColor("_EmissionColor", Color.yellow);
        buttonMaterials[0].color = Color.green;
        buttonMaterials[1].color = Color.green;
        buttonMaterials[2].color = Color.green;
        buttonMaterials[3].color = Color.green;
        buttonMaterials[4].color = Color.green;
        buttonMaterials[5].color = Color.green;
        closeOutsideDoors[0].Play("0FloorDoorsClose");
        closeOutsideDoors[2].Play("2FloorDoorsClose");
        closeOutsideDoors[3].Play("3FloorDoorsClose");
        closeOutsideDoors[4].Play("4FloorDoorsClose");
        closeOutsideDoors[5].Play("5FloorDoorsClose");
        floorsText[0].color = antColorDuring;
        floorsText[1].color = antColorDuring;
        floorsText[2].color = antColorDuring;
        floorsText[3].color = antColorDuring;
        floorsText[4].color = antColorDuring;
        floorsText[5].color = antColorDuring;
        floorsText[6].color = antColorDuring;
    }

    public void goOnceFunc()
    {
        if (goOnce == false)
        {
            goOnce = true;
            isGoing = true;
        }
    }

    public void afterStart()
    {
        Debug.Log("sentAfterStart");
        buttonPress = loadElevatorFloor;
        resetColorArrows();
        defaultizeText();
        closeOutsideDoors[0].Play("0FloorDoorsClose");
        closeOutsideDoors[1].Play("1FloorDoorsClose");
        closeOutsideDoors[2].Play("2FloorDoorsClose");
        closeOutsideDoors[3].Play("3FloorDoorsClose");
        closeOutsideDoors[4].Play("4FloorDoorsClose");
        closeOutsideDoors[5].Play("5FloorDoorsClose");
        blinkAnt = true;
        EleDoors.Play("ElevatorDoorsClose");
        checkAlreadyGoing = true;
        SendCustomEventDelayedSeconds("antennaCol", 0.3f);
        // antennaObject.SetActive(false);
        // floorsColliders[0].SetActive(true);
        // floorsColliders[1].SetActive(false);
        // floorsColliders[2].SetActive(false);
        // floorsColliders[3].SetActive(false);
        // floorsColliders[4].SetActive(false);
        // floorsColliders[5].SetActive(false);
    }

    private void Update()
    {
        isGoingCHECK();
        goElevatorFloor = buttonPress;
        if (buttonPress <= 6 && checkAlreadyGoing == true)
        {
            checkAlreadyGoing = false;
            moveToFloor();
        }
        float moveSpeed = 0.8f;
        float step = moveSpeed * Time.deltaTime;
        if (MovementCheck == true)
        {
            antennaMaterial.SetColor("_EmissionColor", Color.green);
            if (buttonPress > floorCurrent)
            {
                upDowns[0].color = Color.green;
                upDowns[1].color = Color.green;
                upDowns[2].color = Color.green;
                upDowns[3].color = Color.green;
                upDowns[4].color = Color.green;
                upDowns[5].color = Color.green;
                upDowns[6].color = Color.green;
                float speed = 1.0f;
                elevatorObject.position += Vector3.up * speed * Time.deltaTime;
            }
            else if (buttonPress < floorCurrent)
            {
                upDowns[7].color = Color.green;
                upDowns[8].color = Color.green;
                upDowns[9].color = Color.green;
                upDowns[10].color = Color.green;
                upDowns[11].color = Color.green;
                upDowns[12].color = Color.green;
                upDowns[13].color = Color.green;
                float speed = 1.0f;
                elevatorObject.position += Vector3.down * speed * Time.deltaTime;
            }
        }
        if (blinkAnt == true)
        {
            blinkAntNow();
        }
    }

    private void isGoingCHECK()
    {
        if (isON == true && isGoing == true)
        {
            Debug.Log("isGoingCheckSent");
            SendCustomEventDelayedSeconds("sendCurrentFloor", 2);
            isGoing = false;
        }
    }

    public void sendCurrentFloor()
    {
        if (loadElevatorFloor == 0)
        {
            F0();
            isGoing = true;
            Debug.Log("sendCurrentFloor0R");
        }
        else if (loadElevatorFloor == 1)
        {
            F1();
            isGoing = true;
            Debug.Log("sendCurrentFloor1R");
        }
        else if (loadElevatorFloor == 2)
        {
            F2();
            isGoing = true;
            Debug.Log("sendCurrentFloor2R");
        }
        else if (loadElevatorFloor == 3)
        {
            F3();
            isGoing = true;
            Debug.Log("sendCurrentFloor3R");
        }
        else if (loadElevatorFloor == 4)
        {
            F4();
            isGoing = true;
            Debug.Log("sendCurrentFloor4R");
        }
        else if (loadElevatorFloor == 5)
        {
            F5();
            isGoing = true;
            Debug.Log("sendCurrentFloor5R");
        }
    }

    private void blinkAntNow()
    {
        if (enableBoth == true && blinkAnt == true)
        {
            enableBoth = false;
            antennaMaterial.SetColor("_EmissionColor", antColorWait);
            SendCustomEventDelayedSeconds("blinkAntNow", 0.8f);
        }
        else if (enableBoth == false && blinkAnt == true)
        {
            enableBoth = true;
            antennaMaterial.SetColor("_EmissionColor", antColorDuring);
            SendCustomEventDelayedSeconds("blinkAntNow", 0.8f);
        }
    }

    // --------------------
    public void F0()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F0Net");
        // SendCustomEventDelayedSeconds("F0Net", 0.5f);
        isGoing = false;
    }

    public void F0Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 0;
            if (alreadyMoving == false && buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(true);
                // floorsColliders[1].SetActive(false);
                // floorsColliders[2].SetActive(false);
                // floorsColliders[3].SetActive(false);
                // floorsColliders[4].SetActive(false);
                // floorsColliders[5].SetActive(false);
            }
        }
    }
    // --------------------

    public void F1()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F1Net");
        // SendCustomEventDelayedSeconds("F1Net", 0.5f);
        isGoing = false;
    }

    public void F1Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 1;
            if (alreadyMoving == false && buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(false);
                // floorsColliders[1].SetActive(true);
                // floorsColliders[2].SetActive(false);
                // floorsColliders[3].SetActive(false);
                // floorsColliders[4].SetActive(false);
                // floorsColliders[5].SetActive(false);
            }
        }
    }
    // --------------------

    public void F2()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F2Net");
        // SendCustomEventDelayedSeconds("F2Net", 0.5f);
        isGoing = false;
    }

    public void F2Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 2;
            if (alreadyMoving == false && buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(false);
                // floorsColliders[1].SetActive(false);
                // floorsColliders[2].SetActive(true);
                // floorsColliders[3].SetActive(false);
                // floorsColliders[4].SetActive(false);
                // floorsColliders[5].SetActive(false);
            }
        }
    }
    // --------------------

    public void F3()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F3Net");
        // SendCustomEventDelayedSeconds("F3Net", 0.5f);
        isGoing = false;
    }

    public void F3Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 3;
            if (buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(false);
                // floorsColliders[1].SetActive(false);
                // floorsColliders[2].SetActive(false);
                // floorsColliders[3].SetActive(true);
                // floorsColliders[4].SetActive(false);
                // floorsColliders[5].SetActive(false);
            }
        }
    }
    // --------------------

    public void F4()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F4Net");
        // SendCustomEventDelayedSeconds("F4Net", 0.5f);
        isGoing = false;
    }

    public void F4Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 4;
            if (alreadyMoving == false && buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(false);
                // floorsColliders[1].SetActive(false);
                // floorsColliders[2].SetActive(false);
                // floorsColliders[3].SetActive(false);
                // floorsColliders[4].SetActive(true);
                // floorsColliders[5].SetActive(false);
            }
        }
    }
    // --------------------

    public void F5()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "F5Net");
        // SendCustomEventDelayedSeconds("F5Net", 0.5f);
        isGoing = false;
    }

    public void F5Net()
    {
        if (MovementCheck == false)
        {
            buttonPress = 5;
            if (alreadyMoving == false && buttonPress != floorCurrent)
            {
                resetColorArrows();
                defaultizeText();
                closeOutsideDoors[0].Play("0FloorDoorsClose");
                closeOutsideDoors[1].Play("1FloorDoorsClose");
                closeOutsideDoors[2].Play("2FloorDoorsClose");
                closeOutsideDoors[3].Play("3FloorDoorsClose");
                closeOutsideDoors[4].Play("4FloorDoorsClose");
                closeOutsideDoors[5].Play("5FloorDoorsClose");
                blinkAnt = true;
                EleDoors.Play("ElevatorDoorsClose");
                checkAlreadyGoing = true;
                SendCustomEventDelayedSeconds("antennaCol", 0.3f);
                // antennaObject.SetActive(false);
                // floorsColliders[0].SetActive(false);
                // floorsColliders[1].SetActive(false);
                // floorsColliders[2].SetActive(false);
                // floorsColliders[3].SetActive(false);
                // floorsColliders[4].SetActive(false);
                // floorsColliders[5].SetActive(true);
            }
        }
    }

    public void resetColorArrows()
    {
        upDowns[0].color = whiteArrowColor;
        upDowns[1].color = whiteArrowColor;
        upDowns[2].color = whiteArrowColor;
        upDowns[3].color = whiteArrowColor;
        upDowns[4].color = whiteArrowColor;
        upDowns[5].color = whiteArrowColor;
        upDowns[6].color = whiteArrowColor;
        upDowns[7].color = whiteArrowColor;
        upDowns[8].color = whiteArrowColor;
        upDowns[9].color = whiteArrowColor;
        upDowns[10].color = whiteArrowColor;
        upDowns[11].color = whiteArrowColor;
        upDowns[12].color = whiteArrowColor;
        upDowns[13].color = whiteArrowColor;
    }

    private void defaultizeText()
    {
        buttonMaterials[0].color = Color.red;
        buttonMaterials[1].color = Color.red;
        buttonMaterials[2].color = Color.red;
        buttonMaterials[3].color = Color.red;
        buttonMaterials[4].color = Color.red;
        buttonMaterials[5].color = Color.red;
        floorsText[0].color = duringColor;
        floorsText[1].color = duringColor;
        floorsText[2].color = duringColor;
        floorsText[3].color = duringColor;
        floorsText[4].color = duringColor;
        floorsText[5].color = duringColor;
        floorsText[6].color = duringColor;
    }

    private void antennaCol()
    {
        // antennaObject.SetActive(true);
        alreadyMoving = true;
        isON = true;
    }

    private void moveToFloor()
    {
        antennaMaterial.SetColor("_EmissionColor", Color.green);
        Debug.Log("moveToFloorCalled");
        if (alreadyMoving)
        {
            buttonMaterials[buttonPress].color = Color.red;
            SendCustomEventDelayedSeconds("materialReturnToGreen", 1);
        }
        else
        {
            antennaMaterial.SetColor("_EmissionColor", Color.blue);
            buttonMaterials[buttonPress].color = Color.red;
            SendCustomEventDelayedSeconds("materialReturnToGreen", 1);
            MovementCheck = true;
        }
    }

    private void materialReturnToRed()
    {
        buttonMaterials[buttonPress].color = Color.red;
    }

    private void materialReturnToGreen()
    {
        buttonMaterials[buttonPress].color = Color.green;
        loadElevatorFloor = floorCurrent;
    }
}

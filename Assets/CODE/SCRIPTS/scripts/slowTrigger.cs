using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.Components;

public class slowTrigger : UdonSharpBehaviour
{
    public string getTriggerName;
    public elevatorBrains elevatorBrains;

    public override void OnPlayerTriggerEnter(VRCPlayerApi LocalPlayer) // LocalPlayer, playerApi
    {
        elevatorBrains.SendCustomEventDelayedSeconds(getTriggerName, 1);
    }

}

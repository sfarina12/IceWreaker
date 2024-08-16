using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDPadUpdates : MonoBehaviour
{
    [TextArea(1, 10), Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\n" +
                          "For every update you need add a new <checkDPadUpdates> Component into <Player/quests> GameObject.\n" +
                          "This script will unlock the associated update in the d-pad's UI, by removeing the lock symble.";

    [Space,Space,Tooltip("The lock symble, i.e. the image associated with the UI d-pad update")]
    public GameObject lockedSimble;
    [HideInInspector] public bool isUpdate=false;

    private void Start() { if (lockedSimble == null) Debug.LogError("Qnable to unclock the d-pad UI. Please add the image associated with the lock symble"); }
    //the function that will be called by the <quest_pickUpUpdate> Component linked with the same update.
    public void updateUpgrades() { isUpdate = true; lockedSimble.active = false; }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_pickUpUpdate : MonoBehaviour
{
    [TextArea(1,10),Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\nFor every update that you want to add at the d-pad you need to add a new <quest_pickUpUpdate> Component at the <Player/quests> GameObject." +
                          "\nEvery update needs a <outline> Component linked to the update model and an update nema that will be displayed in the player's UI." +
                          "\nThe model linked with the <outline> Component must have the tag:interactable." +
                          "\nEverytime you add a new update you need to update the <checkDPadUpdates> Component located in <quests> GameObject that will unlock the update in the d-Pad's UI (see the notes in <checkDPadUpdates> Component for more info).";

    [Space,Space, Tooltip("The UI checked for unlock the update in the d-Pad's UI")]
    public checkDPadUpdates updateChecker;
    [Space, Tooltip("The outline component linked with the model that player will interact with")]
    public Outline outlineableObject;
    [Tooltip("The update name that will be displayer at the player's UI")]
    public string updateName;
    [Space,Tooltip("[OPTIONAL] sound when picking the update, it will always be located under <all_PlayerAudioSources/audio_dPad_Notification>")]
    public audioPlayer audio;
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    playerInteractHandler interacter;
    printMessage message;
    bool canAudio = false;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;

    private void Start()
    {
        if (!outlineableObject.tag.Equals("interactable")) Debug.LogError("The object with outline doesn't have interactable tag");
        if (updateChecker == null) Debug.LogError("No <updateChecker> Component found. Please link it or else the d-Pad will be unable to unlock the update in the d-pad's UI. The <updateChecker> Component is usually located under <Player/quests> GameObject");
        if (updateName.Equals("")) Debug.LogError("No name found for "+ outlineableObject.gameObject.name+ " update");
        if (audio != null) canAudio=true;

        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        message = GetComponentInParent<printMessage>();
    }

    private void Update()
    {
        if (interacter.isInteracting && outlineableObject.enabled) { performeUpdate(true); }
    }

    public void performeUpdate(bool showMessage)
    {
        if (showMessage) message.showMessage("d-Pad updated: " + updateName);
        updateChecker.updateUpgrades();

        if (showMessage) if (canAudio) audio.playAudio();
        outlineableObject.gameObject.SetActive(false);

        //GameObject.Destroy(outlineableObject.gameObject);
        GetComponent<quest_pickUpUpdate>().enabled = false;
    }
}

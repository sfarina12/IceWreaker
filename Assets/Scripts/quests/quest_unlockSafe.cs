using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_unlockSafe : MonoBehaviour
{
    [Header("The interactions")]
    [Tooltip("The object to interact with")]
    public Interact interactObject;
    public Interact otherInteractObject;
    public audioPlayer audioPlayer;
    
    public quest_MQ2 mq2;
    [Tooltip("Can be null. If not will call 'unlocked' animation")]
    public Animator lockAnimator = null;
    [Header("Custom message")]
    [Tooltip("If empty, will be printed: It's locked")]
    public string customMessage = "It's locked";
    public string customMessage2 = "It's locked";
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    printMessage message;
    playerInteractHandler interacter;
    [HideInInspector] public bool oneTime = false;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;

    private void Start()
    {
        if(customMessage == "") customMessage = "It's locked";
        if(customMessage2 == "") customMessage2 = "It's locked";

        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        interactObject.interactableFlag = false;
        otherInteractObject.interactableFlag = false;
        message = GameObject.Find("quests").GetComponent<printMessage>();
    }

    private void Update()
    {
        if (interacter.isInteracting && (interactObject.outlineableObject.enabled || otherInteractObject.outlineableObject.enabled) && !mq2.code) {
            message.showMessage(customMessage);
        }
        
        if(interacter.isInteracting && interactObject.outlineableObject.enabled && mq2.code && !oneTime) {
            lockAnimator.Play("unlocked");
            audioPlayer.playAudio();
            interactObject.interactableFlag = true;
            otherInteractObject.interactableFlag = true;
            interactObject.showPointer = false;
            oneTime = true;
        }
    }
}

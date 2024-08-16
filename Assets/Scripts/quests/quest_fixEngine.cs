using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_fixEngine : MonoBehaviour
{
    [Header("The interactions")]
    [Tooltip("The object to interact with")]
    public Interact interactObject;
    public quest_MQ2 mq2;
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    printMessage message;
    playerInteractHandler interacter;
    int count = 0;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;

    private void Start()
    {
        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        interactObject.interactableFlag = false;
        message = GameObject.Find("quests").GetComponent<printMessage>();
    }

    private void Update()
    {
        if (interacter.isInteracting && interactObject.outlineableObject.enabled && !mq2.belt)
        {
            message.showMessage("I don't have the replacement");
        }
        
        if(mq2.belt) {
            interactObject.interactableFlag = true;
        }

        // if(count>0) {interactObject.interactableFlag = false; mq2.forceState = true;}
        // if(interacter.isInteracting && mq2.belt) count++;
    }

    public void scriptOn() { 
        mq2.forceState = true;
    }

    public void scriptOff() {
        
    }

    public void isInteracting() {  
    }
}

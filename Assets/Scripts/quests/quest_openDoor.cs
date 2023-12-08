using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class quest_openDoor : MonoBehaviour
{
    [Header("The interactions")]
    [Tooltip("The object to interact with")]
    public Interact interactObject;
    public quest_useHammer hammerQuest;

    printMessage message;
    playerInteractHandler interacter;
    [HideInInspector]
    public bool questEnded = false; 


    private void Start()
    {
        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        interactObject.interactableFlag = false;
        message = GameObject.Find("quests").GetComponent<printMessage>();
    }

    private void Update()
    {
        if (interacter.isInteracting && interactObject.outlineableObject.enabled && !hammerQuest.questEnded)
        {
            message.showMessage("It's stuck");
        }
        
        if(hammerQuest.questEnded)
        {
            interactObject.interactableFlag = true;
            questEnded = true;
        }
    }
    public void instantiateFinalState()
    {
        interactObject.interactableFlag = true;
    }
}

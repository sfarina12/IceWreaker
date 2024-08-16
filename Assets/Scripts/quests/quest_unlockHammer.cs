using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_unlockHammer : MonoBehaviour
{
    public quest_pickUpItem itemPicker;
    public Outline outlineableObject;
    public Animator animator;
    public audioPlayer audioPlayer;
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    playerInteractHandler interacter;
    printMessage message;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;
    private void Start()
    {
        if (!outlineableObject.tag.Equals("interactable")) Debug.LogError("The object with outline is not interactable");

        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        message = GameObject.Find("quests").GetComponent<printMessage>();
    }

    void Update()
    {
        if (itemPicker.picked)
        {
            if (interacter.isInteracting && outlineableObject.enabled)
            {
                audioPlayer.playAudio();
                animator.Play("unlock");
                animator.SetBool("locked", false);
                GetComponent<quest_unlockHammer>().enabled = false;
            }
        }
        else
        {
            if (interacter.isInteracting && outlineableObject.enabled)
                message.showMessage("It's locked");
        }
    }
}

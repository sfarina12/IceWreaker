using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class quest_unlockHammer : MonoBehaviour
{
    public quest_pickUpItem itemPicker;
    public Outline outlineableObject;
    public Animator animator;
    public audioPlayer audioPlayer;
    public GameObject hammer;
    public GameObject lock1;
    public GameObject lock2;

    playerInteractHandler interacter;
    printMessage message;
    [HideInInspector]
    public bool questEnded = false;
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
                questEnded = true;
                GetComponent<quest_unlockHammer>().enabled = false;
            }
        }
        else
        {
            if (interacter.isInteracting && outlineableObject.enabled)
                message.showMessage("It's locked");
        }
    }
    public void instantiateFinalState()
    {
        GameObject.Destroy(hammer);
        GameObject.Destroy(lock1);
        GameObject.Destroy(lock2);
    }
}

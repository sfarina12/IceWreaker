using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_useHammer : MonoBehaviour
{
    public Animator lock1;
    public Animator lock2;
    public Animator hammerAnimator;
    public Animator hammerAnimator2;
    public GameObject hammer;
    public quest_pickUpItem pickUpItem;
    [Space]
    public GameObject pipe;
    public Outline pipeOutline;

    [HideInInspector]
    public bool questEnded = false;
    bool fall = false;
    playerInteractHandler interacter;
    [Space]
    public audioPlayer hammerHit;
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;
    private void Start()
    {
        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
    }
    private void Update()
    {
        if (questEnded) return;

        if (!lock1.GetBool("locked") && !lock2.GetBool("locked") && !fall)
        {
            hammerAnimator.Play("fall");
            fall = true;

            hammer.tag = "interactable";
            pipe.tag = "interactable";

            pickUpItem.enabled = true;
        }

        if (interacter.isInteracting && pipeOutline.enabled)
        {
            hammerAnimator2.Play("destroy_pipe");
            hammerHit.playAudio();
            questEnded = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quest_breakPipe : MonoBehaviour
{
    public Animator doorAnimator;
    bool end = false;
    public quest_callHelp call;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player") && !end && !call.questEnded) {
            doorAnimator.Play("breakPipe");
            end = true;
        }
        
    }
}

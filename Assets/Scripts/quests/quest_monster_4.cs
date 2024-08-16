using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quest_monster_4 : MonoBehaviour
{
    public Animator animator;

    bool oneTime = false;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            if(!oneTime) {
                animator.Play("furniture_1");
                oneTime = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_monster_1 : MonoBehaviour
{
    public Animator animator;
    
    [Space]
    public BoxCollider invisibleWall;
    public GameObject water;

    [HideInInspector]
    public bool oneTime = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            if(!oneTime) {
                water.SetActive(false);
                animator.Play("towelFall");
                oneTime = true;
            }
        }
    }
}

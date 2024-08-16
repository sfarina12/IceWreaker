using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_monster_1_1 : MonoBehaviour
{
    public quest_monster_1 _monster_1;


    private bool oneTime = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            //Debug.Log(!oneTime && _monster_1.oneTime);
            if(!oneTime && _monster_1.oneTime) {
                oneTime = true;
                _monster_1.animator.Play("New State");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quest_monster_2 : MonoBehaviour
{
    public quest_unlockHammer hammerQuest;
    public GameObject eyes;
    public float time = 10;
    public float minDistanceDisappear = 1;
    public GameObject player;
    
    private bool oneTime = false;
    private float act_time = 0;

    private void Start() {
        eyes.SetActive(false);
    }

    void Update() {
        if(act_time <= time) {
            act_time += Time.deltaTime;
        } else {
            if(hammerQuest.animator.GetBool("locked")) {
                eyes.SetActive(true);
            }

            if(eyes.activeSelf && (Vector3.Distance(player.transform.position,eyes.transform.position) < minDistanceDisappear)) {
                eyes.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        
    }
}

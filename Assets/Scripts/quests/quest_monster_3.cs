using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_monster_3 : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject lookCollider;
    public float maxLookDistance;
    public float maxTurnDegree;
    public float minTurnDegree;
    public Interact antaL;
    public GameObject text1;
    public GameObject text2;
    public GameObject update;
    public GameObject claws;
    public LayerMask ignoreLayers;

    bool looked = false;
    bool end = false;
    float start_rotation = 0;
    
    void Start() {
        update.SetActive(false);
        text2.SetActive(false);
        text1.SetActive(true);
        claws.SetActive(false);
    }

    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxLookDistance, ~ignoreLayers))
        {
            if(antaL.isOn) {
                //is looking at paper   
                if((hit.transform.gameObject == lookCollider) && !looked) { 
                    looked = true; 
                    start_rotation = playerCamera.transform.rotation.eulerAngles.y; 
                    maxTurnDegree += start_rotation;
                    minTurnDegree -= start_rotation;
                }

                if(looked) {
                    //is looking at back 
                    if((maxTurnDegree < playerCamera.transform.rotation.eulerAngles.y || minTurnDegree > playerCamera.transform.rotation.eulerAngles.y) && !end) {
                        antaL.interactByScript =  true;
                        text1.SetActive(false);
                        text2.SetActive(true);
                        update.SetActive(true);
                        claws.SetActive(true);
                        end = true;
                    }
                }
            }
        }
    }
}

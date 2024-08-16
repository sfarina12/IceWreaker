using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_MQ2 : MonoBehaviour
{
    public quest_pickUpItem pickCode;
    public quest_pickUpItem pickBelt;
    public List<lightSwitching> all_switches;
    public Interact engineInteract;
    public animationsStates state;
    public List<GameObject> lightPCs;
    [Space]
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;
    [Space]
    public GameObject radio;
    public GameObject radioPc;
    [Space]
    public quest_MQ1 mq1;
    public GameObject monster;
    public GameObject engineAudio;

    [HideInInspector] public bool code = false;
    [HideInInspector] public bool belt = false;
    [HideInInspector] public bool questEnded = false;
    [HideInInspector] public bool forceState = false;
    [HideInInspector] public bool devtool = false;

    private void Start() {
        foreach(GameObject l in objectsToEnable) l.SetActive(false);
        foreach(GameObject l in objectsToDisable) l.SetActive(true);
        radio.GetComponent<Interact>().enabled = false;
        radioPc.GetComponent<Interact>().enabled = false;
    }

    void Update()
    {
        if(pickCode.picked) code = true;
        if(pickBelt.picked) belt = true;

        if((!questEnded && forceState) || devtool) {
            engineInteract.enabled = false;
            //foreach(lightSwitching a in all_switches) { a.turnoOn(); }
            foreach(GameObject l in lightPCs) l.SetActive(true);

            //switch objects when turning on the engine
            foreach(GameObject l in objectsToEnable) l.SetActive(true);
            foreach(GameObject l in objectsToDisable) l.SetActive(false);

            radio.tag = "interactable";
            radioPc.tag = "interactable";
            radio.GetComponent<Interact>().enabled = true;
            radioPc.GetComponent<Interact>().enabled = true;
            
            engineAudio.SetActive(true);

            mq1.canZoom = true;
            questEnded = true;
            monster.SetActive(true);
        }
    }
}

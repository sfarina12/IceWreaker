using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_pickUpItem : MonoBehaviour
{
    [Tooltip("The outline object to interact with")]
    public Outline outlineableObject;
    public string description;
    [HideInInspector]
    public bool picked=false;

    playerInteractHandler interacter;
    printMessage message;

    private void Start()
    {
        if (!outlineableObject.tag.Equals("interactable")) Debug.LogError("The object with outline is not interactable");

        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        message = GameObject.Find("quests").GetComponent<printMessage>();
    }

    private void Update()
    {
        if (picked) return;

        if (interacter.isInteracting && outlineableObject.enabled)
        {
            message.showMessage("Picked: "+description);

            picked = true;
            GameObject.Destroy(outlineableObject.gameObject);
            //GetComponent<quest_pickUpItem>().enabled = false;
        }
    }
}

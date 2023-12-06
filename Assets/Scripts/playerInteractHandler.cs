using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteractHandler : MonoBehaviour
{
    public Camera playerCamera;
    [Space,Header("Settings")]
    [Tooltip("max distance witch player can interact with objects")]
    public float maxDistance;
    public KeyCode interactionKey;
    public GameObject interactableText;
    public LayerMask ignoreLayers;

    [HideInInspector]
    public bool isInteracting = false;
    [HideInInspector]
    public GameObject selectedDoor=null;
    [HideInInspector]
    public bool showInteractText = true;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDistance, ~ignoreLayers))
        {
            if (!showInteractText) interactableText.active = false;

            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);
            
            if (hit.transform.tag.Equals("interactable"))
            {
                selectedDoor = hit.transform.gameObject;
                selectedDoor.GetComponent<Outline>().enabled = true;

                interactableText.active = showInteractText?true:false;

                if (Input.GetKeyDown(interactionKey)) isInteracting = true;
                if (Input.GetKeyUp(interactionKey)) isInteracting = false;
            }
            else { if (selectedDoor != null) selectedDoor.GetComponent<Outline>().enabled = false; interactableText.active = false; }
        }
        else { if (selectedDoor != null) selectedDoor.GetComponent<Outline>().enabled = false; interactableText.active = false; }

        if (Input.GetKeyUp(interactionKey)) isInteracting = false;
    }
}

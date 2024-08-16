using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_dPad : MonoBehaviour
{
    [Tooltip("The outline object to interact with")]
    public Outline outlineableObject;
    public open_dPad dpad_controller;
    public open_dPad dpad_light_controller;
    [Space]
    public printMessage message;
    public GameObject messageDpad;
    [Space]
    public audioPlayer audio;
    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    playerInteractHandler interacter;
    bool destroyed = false;
    float time = 0;
    [HideInInspector]
    public bool questEnded = false;

    [HideInInspector] public bool devtool = false;

    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;

    private void Start()
    {
        if (!outlineableObject.tag.Equals("interactable")) Debug.LogError("The object with outline is not interactable");

        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();
        messageDpad.active = false;
    }

    private void LateUpdate()
    {
        if (!questEnded)
        {
            if (!destroyed)
            {
                if (outlineableObject.enabled)
                {
                    message.showMessage("A useful multi-kit machine");
                }

                if ((interacter.isInteracting && outlineableObject.enabled) || devtool)
                {
                    destroyed = true;
                    audio.playAudio();
                    destroyDPadProp();
                }
            }

            if (destroyed)
            {
                if (time <= 3f)
                    messageDpad.active = true;
                else
                { messageDpad.active = false; questEnded = true; }

                time += Time.deltaTime;
                dpad_controller.enabled = true;
                dpad_light_controller.enabled = true;
            }
        }
    }

    public void destroyDPadProp() { GameObject.Destroy(outlineableObject.gameObject.transform.parent.gameObject); }
}

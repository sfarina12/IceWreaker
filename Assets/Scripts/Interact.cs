using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public playerInteractHandler interacter;

    [Tooltip("the object that can get highlited"),SerializeField]
    public Outline outlineableObject;

    [Space,Header("Pointer"),Min(0),Tooltip("[can be 0] if 0 will take <interacter> maxDistance. Indicate the distance before showing the pointer")]
    public float pointerDistance = 0;

    [Space,Header("Settings"),Tooltip("[can be null] [can be with script]")]
    public Animator animator;
    [Tooltip("[can be null] [can be with script]")]
    public string name_animationOn;
    [Tooltip("[can be null] [can be with script]")]
    public string name_animationOff;
    [Tooltip("[can be null] [can be with animation] I will play a script: Similar to the animations, i will execute the function scriptOn() and scriptOff()")]
    public string scriptName;

    [Space,Header("Sound triggerer monster"),Tooltip("[can be null] the sound setter that will be trggered when player interact with something")]
    public objectSoundSetter sounds;
    public audioPlayer audioPlayer;

    [Space]
    [Tooltip("Starts as open?")]
    public bool isOn = false;
    
    //if you want to disable the interactable but show the pointer anyway, set this to true
    //else, if you don't want to show the pointer even if it can be interacted, set to false
    [HideInInspector] public bool showPointer = true;
    [HideInInspector] public bool interactableFlag = true;
    [HideInInspector] public bool forceAnimationStateUpdate = false;
    [HideInInspector]
    //allow to interact by a call from a script. 
    //When is TRUE will perform the interact and will return FALSE
    //To decide if is open or not use "isOn"
    public bool interactByScript = false;

    bool canAnimate = false;
    bool canSound = false;
    bool canScript = false;
    
    //pointer stuff
    [HideInInspector] public bool canPointer = false;
    [HideInInspector] public Collider pointerCollider;


    private void Start() {
        if (animator != null &&
            name_animationOn != "" &&
            name_animationOff != "")
        { canAnimate = true; }
        if (scriptName != "") { canScript = true; }

        if (sounds != null) { canSound = true; }

        if (interacter == null) { Debug.LogError("The object " + transform.name + " Doesn't have any <playerInteractHandler.cs>. Can't perform <interact.cs>"); }
        if (outlineableObject == null) { Debug.LogError("The object " + transform.name + " Doesn't have any <Outline.cs>. Can't perform <interact.cs>"); }
        if (!outlineableObject.tag.Equals("interactable")) { Debug.LogError("The object " + transform.name + " isn't tagged interactable. Can't perform <interact.cs>"); }
        
        if(canAnimate) {
            if (forceAnimationStateUpdate)
            {
                if (isOn) animator.Play(name_animationOn + "_forced", 0, 1);
                else animator.Play(name_animationOff + "_forced", 0, 1);           
            }
        }
        if(canScript) {
            if(forceAnimationStateUpdate) {
                if (isOn) CallScriptMethod(scriptName,"scriptOn");
                else CallScriptMethod(scriptName,"scriptOff");  
            }
        }

        forceAnimationStateUpdate = false;
    }

    void Update() {
        if (interactableFlag) {
            if ((interacter.isInteracting && outlineableObject.enabled) || interactByScript) {
                interactByScript = false;
                interacter.isInteracting = false;
                bool interacting_tmp = false;

                if(canAnimate) interacting_tmp = animator.GetBool("isInteracting");
                if (canScript) CallScriptMethod(scriptName, "isInteracting");

                if (!interacting_tmp) {
                    if (!isOn) {
                        if (canAnimate) {
                            animator.Play(name_animationOn);
                            animator.SetBool("isInteracting", true);
                        }

                        if (canScript) { CallScriptMethod(scriptName,"scriptOn"); }
                        
                        if (audioPlayer != null) { audioPlayer.playAudio(); }
                        if (canSound) { sounds.activateSound(); }

                        isOn = true;
                    } else {
                        if (canAnimate) { 
                            animator.Play(name_animationOff);
                            animator.SetBool("isInteracting", true);
                        }

                        if (canScript) { CallScriptMethod(scriptName,"scriptOff"); }

                        if (audioPlayer != null) { audioPlayer.playAudio(); }
                        if (canSound) { sounds.activateSound(); }

                        isOn = false;
                    }
                }
            }
        }
    }

    public object CallScriptMethod(string componentName, string methodName) {
        var component = gameObject.GetComponent(componentName);
        var componentType = component.GetType();
        var methodInfo = componentType.GetMethod(methodName);
        var parameters = new object[0]; // Set up parameters here, if needed.
        var result = methodInfo.Invoke(component, parameters);
        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public playerInteractHandler interacter;

    [Space,Tooltip("the object that can get highlited"),SerializeField]
    public Outline outlineableObject;

    [Space,Header("Settings"),Tooltip("[can be null] ")]
    public Animator animator;
    [Tooltip("[can be null] ")]
    public string name_animationOn;
    [Tooltip("[can be null] ")]
    public string name_animationOff;
    [Tooltip("[can be null] Instead of an animation, i will play a script: Similar to the animations, i will execute the function scriptOn() and scriptOff()")]
    public string scriptName;

    [Space,Header("Sound triggerer monster"),Tooltip("[can be null] the sound setter that will be trggered when player interact with something")]
    public objectSoundSetter sounds;
    public audioPlayer audioPlayer;

    [Space]
    [Tooltip("Starts as open?")]
    public bool isOn = false;
    
    [HideInInspector]
    public bool interactableFlag = true;
    [HideInInspector]
    public bool forceAnimationStateUpdate = false;

    bool canAnimate = false;
    bool canSound = false;
    bool canScript = false;

    private void Start()
    {
        if (animator != null &&
            name_animationOn != "" &&
            name_animationOff != "")
        { canAnimate = true; }
        if (scriptName != "") { canScript = true; }

        if (sounds != null) { canSound = true; }

        if (!outlineableObject.tag.Equals("interactable")) { Debug.LogError("The object " + transform.name + " isn't tagged interactable. Can't perform <interact.cs>"); }
        if (outlineableObject == null) { Debug.LogError("The object " + transform.name + " Doesn't have any <Outline.cs>. Can't perform <interact.cs>"); }
        if (interacter == null) { Debug.LogError("The object " + transform.name + " Doesn't have any <playerInteractHandler.cs>. Can't perform <interact.cs>"); }
        
        if(canAnimate)
            if (forceAnimationStateUpdate)
            {
                if (isOn) animator.Play(name_animationOn + "_forced", 0, 1);
                else animator.Play(name_animationOff + "_forced", 0, 1);           
            }
        if(canScript)
            if(forceAnimationStateUpdate) {
                if (isOn) CallScriptMethod(scriptName,"scriptOn");
                else CallScriptMethod(scriptName,"scriptOff");  
            }

        forceAnimationStateUpdate = false;
    }

    void Update()
    {
        if (interactableFlag)
        {
            if (interacter.isInteracting && outlineableObject.enabled)
            {
                interacter.isInteracting = false;
                bool interacting_tmp = false;

                if(canAnimate) interacting_tmp = animator.GetBool("isInteracting");
                if (canScript) CallScriptMethod(scriptName, "isInteracting");
                if (!interacting_tmp)
                {
                    if (!isOn)
                    {
                        if (canAnimate)
                        {
                            animator.Play(name_animationOn);
                            animator.SetBool("isInteracting", true);

                            if (audioPlayer != null)
                                audioPlayer.playAudio();
                        }

                        if (canScript)
                        {
                            CallScriptMethod(scriptName,"scriptOn");

                            if (audioPlayer != null)
                                audioPlayer.playAudio();
                        }
                        
                        if (canSound)
                        { sounds.activateSound(); }

                        isOn = true;
                    }
                    else
                    {
                        if (canAnimate)
                        { 
                            animator.Play(name_animationOff);
                            animator.SetBool("isInteracting", true);

                            if (audioPlayer != null)
                                audioPlayer.playAudio();
                        }

                        if (canScript)
                        {
                            CallScriptMethod(scriptName,"scriptOff");

                            if (audioPlayer != null)
                                audioPlayer.playAudio();
                        }

                        if (canSound)
                            sounds.activateSound();

                        isOn = false;
                    }
                }
            }
        }
    }

    public object CallScriptMethod(string componentName, string methodName)
    {
        var component = gameObject.GetComponent(componentName);
        var componentType = component.GetType();
        var methodInfo = componentType.GetMethod(methodName);
        var parameters = new object[0]; // Set up parameters here, if needed.
        var result = methodInfo.Invoke(component, parameters);
        return result;
    }
}

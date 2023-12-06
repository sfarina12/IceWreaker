using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneInitialization : MonoBehaviour
{
    [Header("Animators"),Tooltip("if not dpaf it must use <standardAnimator>")]
    public Animator canvasAnimator;
    public Animator dPadAnimator;
    [Tooltip("initial scene dpad animation name. Will not be readed if animator is empty")]
    public string dpadAnimationName;
    [Header("G_OBJ dipendents from animators"), Space, Tooltip("It will be used in case of <standardAnimator>")]
    public GameObject dpad;
    [Space, Tooltip("It will be used in case of <custom_animator>")]
    public GameObject blackScreen;
    [Space,Tooltip("this variable will be hidden when the memory system will be implemented")]
    public bool hasDpad = true;
    
    void Start()
    {
        //lo start dovrebbe leggere i dati di salvataggio in modo da capire se ha preso oppure non ha preso il dpad

        if (hasDpad)
        {
            dPadAnimator.Play(dpadAnimationName);
            blackScreen.active = false;
        }
        else
        {
            canvasAnimator.Play("standardIntro");
        }
    }

    
    
}

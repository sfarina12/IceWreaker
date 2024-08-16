using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class submenu : MonoBehaviour
{
    public Animator animator;
    [Tooltip("name of the animation that will be played when opening the menu")]
    public string animationName;
    public animationsStates state;
    [Space]
    public List<GameObject> titles;
    public GameObject myTitle;
    [Space]
    public GameObject menuContent;
    public bool enableMainMenu = false;
    public List<GameObject> mainMenuButtons;
    
    bool click = false;
    public void clicked() {
        click = true;
        animator.Play(animationName);        
    }

    void Update() {
        if(click) {
            if(state.isEnded) { 
                foreach(GameObject t in titles) { t.SetActive(false); }
                foreach(GameObject t in mainMenuButtons) { t.SetActive(enableMainMenu); }
                myTitle.SetActive(true); 
                click = false;
                menuContent.SetActive(!menuContent.activeSelf);
                state.isEnded = false;
            }
        }
    }
}

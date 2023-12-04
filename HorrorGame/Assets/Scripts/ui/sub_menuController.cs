using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sub_menuController : MonoBehaviour
{
    public GameObject menu;
    public audioPlayer audio;

    public void openMenu() { menu.SetActive(!menu.activeSelf); audio.playAudio(); closeMenu(); }

    public void closeMenu() 
    {
        foreach (Transform child in transform.parent)
        {
            if(child.name.Contains("sMenu") && !child.name.Equals(menu.name))
                child.gameObject.SetActive(false);
        }
        
    }
}

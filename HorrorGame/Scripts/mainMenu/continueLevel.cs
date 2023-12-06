using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class continueLevel : MonoBehaviour
{
    public Animator animator;
    public string animationName;
    public AudioSource sound;
    public loadMap loader;

    int counter = 0;

    public void continue_level()
    {
        string path = Application.persistentDataPath;

        if (!System.IO.File.Exists(path + "/wrecker.ice"))
        {
            counter++;

            if (counter < 10)
            { 
                animator.Play(animationName);
                sound.Play();
            }
            else
            { animator.Play(animationName + "_easter"); sound.Play(); counter = 0; }
        }
        else
        {
            loader.loadLevel();
        }
    }

}

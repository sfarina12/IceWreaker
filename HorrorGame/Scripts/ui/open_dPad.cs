using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_dPad : MonoBehaviour
{
    [HideInInspector]
    public bool enabled = false;

    public Animator animator;
    public SC_FPSController playerController;
    [Space]
    public checkDPadUpdates flashLightUpdateChecker;
    public bool isFlashLight = false;
    public GameObject lightBook;
    [Space]
    public audioPlayer audio;
    [Space]
    public audioPlayer audio_2;
    

    bool open = false;

    void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !isFlashLight)
            {
                if (!open)
                {
                    playerController.canMove = false;

                    performeAnimation("open_dPad");

                    audio.playAudio();

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    playerController.canMove = true;

                    performeAnimation("close_dPad");

                    audio.playAudio();

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                open = !open;
            }
            
            if (Input.GetKeyDown(KeyCode.L) && isFlashLight && flashLightUpdateChecker.isUpdate)
            {
                if (!open)
                { performeAnimation("open_light"); animator.SetBool("isD-PadVisible",true); audio.playAudio(); lightBook.active = true; }
                else
                { performeAnimation("close_light"); animator.SetBool("isD-PadVisible", false); audio_2.playAudio(); lightBook.active = false; }

                open = !open;
            }
        }
    }

    void performeAnimation(string nameAnim)
    {
        animator.Play(nameAnim);
    }
}

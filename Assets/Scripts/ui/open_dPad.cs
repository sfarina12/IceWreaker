using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class open_dPad : MonoBehaviour
{
    [HideInInspector] public bool enabled = false;

    public printMessage message;
    public Animator animator;
    [Space,Header("Camera Interface menu opening")]
    public Animator animatorPlayer;
    [Space,Header("PCs interfaces")]
    public PC_inputOutput PC_pokemon;
    public PC_inputOutput PC_engine;
    public PC_inputOutput PC_bridge;
    [Space]
    public SC_FPSController playerController;
    [Space]
    public checkDPadUpdates flashLightUpdateChecker;
    public checkDPadUpdates camerainterfaceUpdateChecker;
    public cameraInterface_update cameraInterfaceUpdate;
    public Interact radioInteract;
    public GameObject bookInteracters;
    public bool isFlashLight = false;
    public GameObject lightBook;
    [Space]
    public audioPlayer audio;
    [Space]
    public audioPlayer audio_2;
    
    [HideInInspector] public bool open = false;
    bool is_interacting_radio = false;
    pickUp_book[] books;

    void Start(){
        books = new pickUp_book[0];
        if(bookInteracters != null) { books = bookInteracters.GetComponents<pickUp_book>(); }
    }

    void Update()
    {
        if (enabled)
        {
            //mi calcolo se HO interagito con la radio o no
            if(radioInteract != null) { is_interacting_radio = radioInteract.isOn; }

            //gestione menu gioco
            if (Input.GetKeyDown(KeyCode.Tab) && !isFlashLight && (!PC_pokemon.is_open && !PC_engine.is_open && !PC_bridge.is_open) && !is_interacting_radio && !isBooksOpen()) {
                if (!open)
                {
                    playerController.canMove = false;
                    playerController.audioRunning.stopAudio();
                    playerController.audioWalking.stopAudio();

                    if(camerainterfaceUpdateChecker.isUpdate) {
                        if(cameraInterfaceUpdate.open) {
                            animatorPlayer.Play("open_dPad_cameraInterface");
                            animator.Play("open_dPad_canvasONLY");
                        } else {
                            animator.Play("open_dPad");
                        }
                    } else {
                        animator.Play("open_dPad");
                    }

                    audio.playAudio();

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    playerController.canMove = true;
                    if(camerainterfaceUpdateChecker.isUpdate) {
                        if(cameraInterfaceUpdate.open) {
                            animatorPlayer.Play("close_dPad_cameraInterface");
                            animator.Play("close_dPad_canvasONLY");
                        } else {
                            animator.Play("close_dPad");
                        }
                    } else {
                        animator.Play("close_dPad");
                    }
                    audio.playAudio();

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                open = !open;
            } else if(Input.GetKeyDown(KeyCode.Tab) && !isFlashLight && (PC_pokemon.is_open || PC_engine.is_open || PC_bridge.is_open || is_interacting_radio || isBooksOpen())) {
                message.showMessage("cannot open it if viewing device");
            }

            //gestione update luce
            if (Input.GetKeyDown(KeyCode.L) && isFlashLight && flashLightUpdateChecker.isUpdate && !isBooksOpen()) {   
                bool can_use = true;
                if(camerainterfaceUpdateChecker.isUpdate) {
                    if(!cameraInterfaceUpdate.open) can_use = true;
                    else can_use = false;
                }

                if(can_use) {
                    if (!open)
                    { performeAnimation("open_light",animator); animator.SetBool("isD-PadVisible",true); audio.playAudio(); /*lightBook.active = true;*/ }
                    else
                    { performeAnimation("close_light",animator); animator.SetBool("isD-PadVisible", false); audio_2.playAudio(); /*lightBook.active = false;*/ }

                    open = !open;
                } else {
                    //CAMERA ACCESA!
                    message.showMessage("cannot use it if camera is on");
                }
            } else if(Input.GetKeyDown(KeyCode.L) && isFlashLight && flashLightUpdateChecker.isUpdate && isBooksOpen()) {
                //LIBRO APERTO
                message.showMessage("cannot use it if reading book");
            }
        }
    }

    bool isBooksOpen() {
        foreach(pickUp_book book in books) {
            if(book.open) {
                return true;
            }
        }
        return false;
    }
    void performeAnimation(string nameAnim,Animator anim)
    {
        anim.Play(nameAnim);
    }

    public void closeFlashLight() {
        open = true;
        performeAnimation("close_light",animator); animator.SetBool("isD-PadVisible", false); audio_2.playAudio(); lightBook.active = false;
    }

    public void openFlashLight() {
        open = false;
        performeAnimation("open_light",animator); animator.SetBool("isD-PadVisible",true); audio.playAudio(); lightBook.active = true;
    }
}

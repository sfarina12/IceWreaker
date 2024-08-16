using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class cameraInterface_update : MonoBehaviour
{
    public KeyCode toggleKey;
    [Space]
    public TextMeshProUGUI timer;
    public printMessage message;
    [Space]
    public checkDPadUpdates cameraInterfaceUpdateChecker;
    public Animator openCloseAnimation;
    public GameObject cameraLight;
    [Space,Header("Checks on whenever can open this update")]
    public checkDPadUpdates flashLightUpdateChecker;
    public open_dPad flashLight;
    [Space]
    public List<PC_inputOutput> All_PC_interactors;
    [Space]
    public open_dPad dpad;
    public Interact radioInteract;


    [HideInInspector] public bool open = false;
    [HideInInspector] public bool wasOpen = false;
    [HideInInspector] public bool wasOpen_radio = false;

    void Update() {
        if(radioInteract.isOn && open) { closeCameraInterface(true,true); }
        if(!open && wasOpen_radio && !radioInteract.isOn) { openCameraInterface(); } 

        //controlla se ho l'uldate
        if(cameraInterfaceUpdateChecker.isUpdate) {
            //apri la camera?
            if(Input.GetKeyUp(toggleKey)) {
                //controlla se ho l'uldate della torcia
                int messageCode = 0;

                bool can_use = true;
                if(flashLightUpdateChecker.isUpdate && flashLight.isFlashLight) {
                    if(!flashLight.open) { can_use = true; }
                    else { can_use = false; messageCode = 1; }
                }

                bool notViewingPc = true;
                //controllre sono dentro un pc
                foreach(PC_inputOutput pc in All_PC_interactors) {
                    if(pc.is_open && notViewingPc) {
                        //non posso aprire
                        notViewingPc = false;

                        messageCode = 2;
                        can_use = false;
                    }
                }         

                //controllare se ho il dpad aperto
                if(dpad.open) { can_use = false; messageCode = 3; }
                if(radioInteract.isOn) { can_use = false; messageCode = 4; }


                if(can_use) { 
                    //apri la camera
                    open = !open;
                    openCloseAnimation.Play(open?"open_cameraInterface":"close_cameraInterface");
                    cameraLight.SetActive(open);
                } else {
                    switch(messageCode) {
                        case 1: message.showMessage("cannot use if torch is on"); break;
                        case 2: message.showMessage("cannot open it if viewing PC"); break;
                        case 3: message.showMessage("cannot open it if viewing DPAD"); break;
                        case 4: message.showMessage("cannot open it if viewing Radio"); break;
                    }
                }
            }
            int h = System.DateTime.Now.Hour;
            int m = System.DateTime.Now.Minute;
            int s = System.DateTime.Now.Second;
            timer.text = h+":"+m+":"+s;
        }
    }

    public void closeCameraInterface(bool rememberOpenState = false,bool rememberOpenState_radio = false) {
        open = false;
        openCloseAnimation.Play("close_cameraInterface");
        cameraLight.SetActive(open);

        if(rememberOpenState && !rememberOpenState_radio) { wasOpen = true; }
        else if(rememberOpenState_radio) { wasOpen_radio = true; }
    }

    public void openCameraInterface() {
        open = true;
        openCloseAnimation.Play("open_cameraInterface");
        cameraLight.SetActive(open);

        wasOpen = false;
        wasOpen_radio = false;
    }
}

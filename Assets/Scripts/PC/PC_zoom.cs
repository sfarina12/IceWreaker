using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC_zoom : MonoBehaviour
{
    public SC_FPSController playerController;
    public GameObject playerCamera;
    public GameObject cameraOrigin;
    public GameObject cameraDestination;
    [Tooltip("Used for checking the power")]
    public quest_MQ1 quest;
    public float smoothness;
    public float minEndDistance = 1f;
    public printMessage message; 


    private bool isCoroutineWorking = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    [HideInInspector]
    public bool zooming = false;
    
    private void Start() {
        originalPosition = cameraOrigin.transform.position;
        originalRotation = cameraOrigin.transform.rotation;
    }

    public void scriptOn() { 
        if(quest.canZoom) {
            if(!zooming) {
                playerController.audioRunning.stopAudio();
                playerController.audioWalking.stopAudio();

                //originalPosition = playerCamera.transform.position;
                originalRotation = playerCamera.transform.rotation;
                originalPosition = cameraOrigin.transform.position;
                //originalRotation = cameraOrigin.transform.rotation;

                isCoroutineWorking = true;
                IEnumerator coroutine = moveCamera(cameraDestination.transform.position,cameraDestination.transform.rotation,false);
                StartCoroutine(coroutine);
            }
        } else {
            message.showMessage("Out of power");
        }
    }
    public void scriptOff() { 
        if(quest.canZoom) {
            if(!zooming) {
                isCoroutineWorking = true;
                IEnumerator coroutine = moveCamera(originalPosition,originalRotation,true);
                StartCoroutine(coroutine);
            }
        } else {
            message.showMessage("Out of power");
        }
     }

    private IEnumerator moveCamera(Vector3 endP, Quaternion endR,bool isOn)
    {
        zooming = true;
        float distance = 100;
        while (distance > minEndDistance)
        {
            playerController.canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            isCoroutineWorking = true;
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position,endP,Time.deltaTime*smoothness);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation,endR,Time.deltaTime*smoothness);
            distance = Vector3.Distance(playerCamera.transform.position,endP);
            yield return null;
        }
        playerCamera.transform.position = endP;
        playerCamera.transform.rotation = endR;
        isCoroutineWorking = false;
        if (isOn)
        {
            playerController.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        zooming = false;
    }

    public bool isInteracting() { 
        return isCoroutineWorking; }
}

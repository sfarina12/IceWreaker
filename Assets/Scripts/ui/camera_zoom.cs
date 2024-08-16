using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_zoom : MonoBehaviour
{
    public SC_FPSController playerController;
    public GameObject playerCamera;
    public GameObject cameraDestination;
    public float smoothness;
    public float minEndDistance = 1f;
    [Space,Tooltip("when zommed, can use the mouse?")]
    public bool useMouse = false;
    [Space,Tooltip("can be null. If the zoomed object is electrically powered mast have this quest")]
    public quest_MQ1 quest;
    [Tooltip("can be null. If the zoomed object is electrically powered mast have this quest")]
    public printMessage message;


    bool isCoroutineWorking = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    
    public void scriptOn() {
        if(quest != null) {
            if(quest.canZoom) {
                originalPosition = playerCamera.transform.position;
                originalRotation = playerCamera.transform.rotation;

                isCoroutineWorking = true;
                IEnumerator coroutine = moveCamera(cameraDestination.transform.position,cameraDestination.transform.rotation,false);
                StartCoroutine(coroutine);
            } else {
                message.showMessage("Out of power");
            }
        } else {
            originalPosition = playerCamera.transform.position;
            originalRotation = playerCamera.transform.rotation;

            isCoroutineWorking = true;
            IEnumerator coroutine = moveCamera(cameraDestination.transform.position,cameraDestination.transform.rotation,false);
            StartCoroutine(coroutine);
        }
    }
    public void scriptOff() { 
        if(quest != null) {
            if(quest.canZoom) {
                isCoroutineWorking = true;
                IEnumerator coroutine = moveCamera(originalPosition,originalRotation,true);
                StartCoroutine(coroutine);
            } else {
                message.showMessage("Out of power");
            }
        } else {
            isCoroutineWorking = true;
            IEnumerator coroutine = moveCamera(originalPosition,originalRotation,true);
            StartCoroutine(coroutine);
        }
     }

    private IEnumerator moveCamera(Vector3 endP, Quaternion endR,bool isOn)
    {
        float distance = 100;
        while (distance > minEndDistance)
        {
            playerController.canMove = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = useMouse;
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
            Cursor.visible = useMouse;
        }

    }

    public bool isInteracting() { 
        return isCoroutineWorking; }
}

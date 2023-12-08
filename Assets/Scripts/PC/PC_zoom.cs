using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PC_zoom : MonoBehaviour
{
    public SC_FPSController playerController;
    public GameObject playerCamera;
    public GameObject cameraDestination;
    public float smoothness;
    public float minEndDistance = 1f;
    private bool isCoroutineWorking = false;

    Vector3 originalPosition;
    Quaternion originalRotation;
    
    public void scriptOn() { 
        originalPosition = playerCamera.transform.position;
        originalRotation = playerCamera.transform.rotation;

       
        isCoroutineWorking = true;
        IEnumerator coroutine = moveCamera(cameraDestination.transform.position,cameraDestination.transform.rotation,false);
        StartCoroutine(coroutine);
    }
    public void scriptOff() { 
      
        isCoroutineWorking = true;
        IEnumerator coroutine = moveCamera(originalPosition,originalRotation,true);
        StartCoroutine(coroutine);
     }

    private IEnumerator moveCamera(Vector3 endP, Quaternion endR,bool isOn)
    {
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

    }

    public bool isInteracting() { 
        return isCoroutineWorking; }
}

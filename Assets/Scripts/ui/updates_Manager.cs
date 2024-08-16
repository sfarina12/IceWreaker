using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updates_Manager : MonoBehaviour
{
    [Header("SOUND UPDATE")]
    public checkDPadUpdates soudDetector_update_checker;
    public soundDetector_update soudDetector_update;
    public GameObject soudDetector_update_MainObject;
    [Space,Header("CAMERA UPDATE")]
    public checkDPadUpdates cameraInterface_update_checker;
    public cameraInterface_update cameraInterface_update;
    public GameObject cameraInterface_update_MainObject;
    [Space,Header("TIMER UPDATE")]
    public checkDPadUpdates timer_update_checker;
    public timer_update timer_update;
    public GameObject timer_update_MainObject;
    public string somestuff="[don't fill]";
    //you can add as many update checker as needed

    //variabile inseribile in qualunque script, controllabile da DevTool
    [HideInInspector] public bool devtool_sound = false;
    [HideInInspector] public bool devtool_camera = false;
    [HideInInspector] public bool devtool_timer = false;

    private void Start()
    {
        //--------------------------------------------[SOUND]
        soudDetector_update_MainObject.SetActive(false);
        soudDetector_update.enabled = false;
        //--------------------------------------------[CAMERA]
        cameraInterface_update_MainObject.SetActive(false);
        cameraInterface_update.enabled = false;
        //--------------------------------------------[CAMERA]
        timer_update_MainObject.SetActive(false);
        timer_update.enabled = false;
        //--------------------------------------------
    }

    private void LateUpdate()
    {
        //--------------------------------------------[SOUND]
        if (soudDetector_update_checker.isUpdate || devtool_sound)      { soudDetector_update_MainObject.SetActive(true);       soudDetector_update.enabled=true; }
        //--------------------------------------------[CAMERA]
        if (cameraInterface_update_checker.isUpdate || devtool_camera)  { cameraInterface_update_MainObject.SetActive(true);    cameraInterface_update.enabled=true; }
        //--------------------------------------------[CAMERA]
        if (timer_update_checker.isUpdate || devtool_timer)             { timer_update_MainObject.SetActive(true);              timer_update.enabled=true; }
        //--------------------------------------------
    }
}

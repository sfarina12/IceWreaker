using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updates_Manager : MonoBehaviour
{
    public checkDPadUpdates soudDetector_update_ckecker;
    public soundDetector_update soudDetector_update;
    public GameObject soudDetector_update_MainObject;
    [Space]
    public string somestuff="[don't fill]";
    //you can add as many update checker as needed

    private void Start()
    {
        soudDetector_update_MainObject.SetActive(false);
        soudDetector_update.enabled = false;
    }

    private void LateUpdate()
    {

        if (soudDetector_update_ckecker.isUpdate) { soudDetector_update_MainObject.SetActive(true); soudDetector_update.enabled=true; }
    }
}

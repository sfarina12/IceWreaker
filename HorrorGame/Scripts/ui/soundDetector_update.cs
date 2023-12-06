using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundDetector_update : MonoBehaviour
{
    public Slider sliderVolume;
    public SphereCollider playerSoundCollider;

    public Animator animator;

    bool isOn = false;

    void Start() { sliderVolume.transform.parent.gameObject.SetActive(false); sliderVolume.value = 0; }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isOn = !isOn;
            Debug.Log("ciao");
            sliderVolume.transform.parent.gameObject.SetActive(isOn);
            animator.Play(isOn ? "open_screen" : "close_screen");

            /*bool isVisibe = animator.GetBool("isD-PadVisible");

            if(!isVisibe) animator.Play("open_light");*/
        }

        if(isOn)
            sliderVolume.value = playerSoundCollider.radius*10;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class soundDetector_update : MonoBehaviour
{
    public bool debug = false;
    [Space]
    public Slider sliderVolume;
    public SC_FPSController platerController;
    public float smoothness = 1;
    public GameObject monster;
    public GameObject testError;

    //Vector3 lastPosition = Vector3.zero;
    int act_sound=0;
    List<objectSoundSetter> sounds = new List<objectSoundSetter>();

    void Start() { 
        sliderVolume.value = 0;
        sounds = Resources.FindObjectsOfTypeAll<objectSoundSetter>().ToList<objectSoundSetter>();
        testError.SetActive(true);
    }

    void Update()
    {
        if(monster.activeSelf || debug) {          
            if(testError.activeSelf) { testError.SetActive(false);  }
            act_sound=0;
            int player_sound = 1;

            foreach(objectSoundSetter s in sounds) {
                if(Vector3.Distance(transform.position,s.transform.position) < 5) {
                    if(s.isActive) {
                        if(s.soundLoudness > act_sound) {
                            act_sound = (int) s.soundLoudness;
                        }
                    }
                }
            }

            if(platerController.isMoving) {
                if(platerController.isCrouch) { player_sound /= 20; }
                else if(platerController.isRunning) { player_sound *= 20; }
                else { player_sound = 10; }
            } else { player_sound = 1; }

            float noise = player_sound < act_sound ? act_sound : player_sound;
            sliderVolume.value = Mathf.Lerp(sliderVolume.value, noise,Time.deltaTime*smoothness);
        }
    }
}

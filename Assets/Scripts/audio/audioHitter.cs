using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class audioHitter : MonoBehaviour
{
    public audioPlayer audio;
    [Space,Tooltip("The tag containing the object that will hit")]
    public string hitterTag = "Player";

    void Start() {
        if(audio == null) { Debug.LogError("No <audioPlayer> component was found for object: "+gameObject.name); }
        if(hitterTag == "") { Debug.LogError("No tag set for object: "+gameObject.name); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == hitterTag) {
            audio.stopAudio();
            audio.playAudio();
        }
     }
}

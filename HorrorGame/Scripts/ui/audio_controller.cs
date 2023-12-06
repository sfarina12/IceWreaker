using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class audio_controller : MonoBehaviour
{
    [Tooltip("l'oggetto che contiene i rettangolini non un vero slider")]
    public List<GameObject> slider;
    [Tooltip("audio source per fare il suono del d-pad")]
    public audioPlayer audioPlayer;
    [HideInInspector]
    public float volume = 0.49f;

    List<AudioSource> audioSources;
    int index = 4;
    [SerializeField]
    private AudioMixer mainMixed;
    void Start()
    {
        audioSources = new List<AudioSource>();
        if(slider == null || slider.Count == 0) Debug.LogError("AUDIO SETTINGS: la variabile 'slider' è vuota");

        GameObject[] objs = GameObject.FindGameObjectsWithTag("audio");
        foreach (GameObject ret in objs) audioSources.Add(ret.GetComponent<AudioSource>());
    }

    public void minus() {
        audioPlayer.playAudio();
  
        if(index >= 0) {
            slider[index].SetActive(false);
            volume -= 0.09f; 
            index--;
        }

        mainMixed.SetFloat("MasterVolume",Mathf.Log10(volume)*20);
        // float v = (float) audio_masterValue/100;
        // foreach (AudioSource ret in audioSources) ret.volume = v;
    }

    public void plus() {
        audioPlayer.playAudio();

        if(index < 9) {
            index++;
            volume += 0.09f; 
            slider[index].SetActive(true);
        }

        mainMixed.SetFloat("MasterVolume",Mathf.Log10(volume)*20);
        //float v = (float) audio_masterValue/100;
        //foreach (AudioSource ret in audioSources) ret.volume = v;
    }
}

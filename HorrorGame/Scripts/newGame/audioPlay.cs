using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlay : MonoBehaviour
{
    public AudioClip soundName;

    public void Start() { transform.gameObject.GetComponent<AudioSource>().clip = soundName; transform.gameObject.GetComponent<AudioSource>().Play(); }

}

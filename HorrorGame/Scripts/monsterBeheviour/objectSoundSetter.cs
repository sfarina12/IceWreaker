using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSoundSetter : MonoBehaviour
{
    public m_patrolling patrolling;
    [Space]
    public CapsuleCollider soundCollider;
    [Min(0)]
    public float soundTime = 0;
    [Space]
    public bool activate = false;

    float soundSize;
    float act_time=0;

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("monster"))
        { if(patrolling.enabled) patrolling.soundTriggered(transform); }
    }

    void Start()
    {
        soundSize = soundCollider.radius;
        soundCollider.radius = 0;
    }


    void Update()
    {
        if (act_time >= 0f) { act_time -= Time.deltaTime; }
        else { soundCollider.radius = 0; }
    }

    public void activateSound()
    {
        soundCollider.radius = soundSize;
        act_time = soundTime;
    }
}

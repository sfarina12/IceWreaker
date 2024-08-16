using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSoundSetter : MonoBehaviour
{
    [Header("AI Settings")]
    public ai_controller ai;
    public float triggerDistance = 0f;
    [Space]
    public bool activate = false;
    [Space,Min(0),Header("Sound settings")]
    public float soundTime = 0;
    [Min(1),Tooltip("the value to display to the d-pad update")]
    public float soundLoudness = 0;

    bool can_collide = true;
    [HideInInspector] public bool isActive = false;
    float soundSize;
    float act_time=0;

    void Start() { can_collide = false; }

    void Update() {
        if (act_time >= 0f) { act_time -= Time.deltaTime; isActive = true; }
        else { isActive = false; }
    }

    public void activateSound() {     
        act_time = soundTime;
           
        if(ai.gameObject.activeSelf) {
            float distance = Vector3.Distance(transform.position, ai.transform.position);
            if(distance <= triggerDistance) { ai.trigger(transform.position); }
        }
    }

    //-------------------------[VISUAL DISTANCE]-------------------------
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, triggerDistance);
    }
    //END ---------------------[VISUAL DISTANCE]-------------------------
}

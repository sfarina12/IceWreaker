using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class quest_end : MonoBehaviour
{
    public Animator playerAnimator;
    public SC_FPSController player_controller;
    public cameraInterface_update camera_update;
    public ai_controller monster;
    public GameObject ambientAudio;

    bool stop = false;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && !stop) {
            monster.agent.speed = 0;
            monster.audioWalking.stopAudio();
            monster.audioRunning.stopAudio();
            monster.audioChasing.stopAudio();
            monster.audioNeutral.stopAudio();
            
            player_controller.canMove = false;
            player_controller.audioRunning.stopAudio();
            player_controller.audioWalking.stopAudio();

            ambientAudio.SetActive(false);

            playerAnimator.Play("end");
            stop = true;

            camera_update.closeCameraInterface();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class killPerformer : MonoBehaviour
{
    [Header("Kills settings")]
    [Header("Monster")]
    public float killDistance = 2f;
    [Space,Header("PC Animation")]
    public float minEndDistance = 0.1f;
    public float smoothness = 0.5f;
    [Space,Header("Death Counter Settings")]
    public int maxDeath = 3;

    [Space,Space]
    public GameObject player;
    public GameObject playerCamera;
    public RenderTexture cameraTexture;
    public SC_FPSController playerController;
    public Image pcScreen;
    [Space]
    public Vector3 death_position;
    public Vector3 death_rotation;
    public Animator screenAnimator;
    public Interact elevatorButton;
    [Space]
    public TextMeshPro elevatorDeathCounter;
    public GameObject deathText1;
    public GameObject deathText2;
    public GameObject deathText3;
    [Space]
    public GameObject ambienceAudio;
    

    bool kill = false;
    [HideInInspector] public int deathCount = 3;
    [HideInInspector] public bool godMode = false;

    void Start() {
        elevatorDeathCounter.text = maxDeath+"";
        deathCount = maxDeath;

        deathText1.SetActive(false);
        deathText2.SetActive(false);
        deathText3.SetActive(false);
    }

    void Update() {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < killDistance && !kill && !godMode) { kill = true; reached();}
    }

    public void reached() {
        ambienceAudio.SetActive(false);

        playerController.audioRunning.stopAudio();
        playerController.audioWalking.stopAudio();

        //creating texture snapshot of the death
        Texture2D freezeTexture = toTexture2D(cameraTexture);
        Sprite blankSprite = Sprite.Create(freezeTexture, new Rect(0, 0, freezeTexture.width, freezeTexture.height), new Vector2(0.5f, 0.5f));
        pcScreen.sprite = blankSprite;

        playerController.characterController.enabled = false;
        playerController.canMove = false;

        playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        player.transform.position = death_position;
        player.transform.rotation = Quaternion.Euler(death_rotation);

        screenAnimator.Play("screenColoring");

        //update death count and consequences
        deathCount--;
        elevatorDeathCounter.text = deathCount < 0 ? "†" : deathCount+" ▼";
        if(deathCount < 0) {
            elevatorButton.name_animationOn ="gameOver";
            elevatorButton.name_animationOff = "gameOver";
        }
        
        switch(deathCount) {
            case 1: deathText1.SetActive(true); break;
            case 0: deathText2.SetActive(true); break;
            case -1: deathText3.SetActive(true); break;
        }

        StartCoroutine(timer());
    }

    IEnumerator timer() {
        yield return new WaitForSeconds(5);
        StartCoroutine(zoomOut_pcDeath());
    }

    IEnumerator zoomOut_pcDeath() {
        float distance = 100;

        Vector3 end_pos = new Vector3(-47.54f,1.07f,14.73f);
        Quaternion end_rot = Quaternion.Euler(0,90,0);

        while (distance > minEndDistance) {
            player.transform.position = Vector3.Lerp(player.transform.position,end_pos,Time.deltaTime*smoothness);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation,end_rot,Time.deltaTime*smoothness);
            distance = Vector3.Distance(player.transform.position,end_pos);
            yield return null;
        }

        player.transform.position = end_pos;
        player.transform.rotation = end_rot;

        playerController.characterController.enabled = true;
        playerController.canMove = true;

        kill = false;
    }

    Texture2D toTexture2D(RenderTexture rTex) {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    //-------------------------[VISUAL PLAYER DISTANCE]-------------------------
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, killDistance);
    }
    //END ---------------------[VISUAL PLAYER DISTANCE]-------------------------
}

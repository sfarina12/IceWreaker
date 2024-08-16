using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTool : MonoBehaviour
{
    [TextArea(1, 100), Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\n" +
                  "La classe funziona come hub per tutte le altre classi che abbiano una variabile chiamata 'devtool'.\n"+
                  "Qualora si desiderasse debuggare o comunque fare un operazione raapidamenta, invece di fare tutte le quest del caso, inserire in questa classe:\n"+
                  "1 - variabile <bool>\n"+
                  "2 - <classe> classe da gestire con il devtool\n"+
                  "3 - dentro la <classe> usare la variabile 'devtool' in modo da azionare la funzionalita da gestire con <DevTool>\n"+
                  "Alternativamente si puo aggiungere qualsiasi combinazione di azioni forzate al fine di eseguire il devtool.\n"+
                  "NECESSARIO chiamare la variabile di dev nelle <classi> 'devtool'. PER UNIFORMARE";
    [Header("Player")]
    public bool shakeCamera = false;
    public bool godMode = false;
    [Space,Header("DPAD")]
    public bool giveDPad = false; 
    public bool enableCameraUpdate = false; 
    public bool enableSoundUpdate = false; 
    [Space,Header("Monster")]
    public bool spawnMonster = false;
    public bool kill = false;
    [Space]
    public int deathCount = 3;
    public bool updateDeathCount = false;
    [Space,Header("World")]
    public bool day = false;
    [Space,Header("Quests")]
    public bool endMQ2 = false;
    public bool endUnlockCamera = false;
    public bool endGame = false;
    

    [Space,Space]
    public GameObject monster;
    public killPerformer killperformer;
    public updates_Manager update_manager;
    public checkDPadUpdates camera_update_unlocker;
    public checkDPadUpdates sound_update_unlocker;
    public quest_dPad dpadQuest;
    public GameObject sun;
    public CameraShake cameraShake;
    public quest_MQ2 mq2;
    public quest_unlockCameraUpdate quest_camera;
    public quest_callHelp quest_help;

    
    void Update() {
        //CAMERA UPDATE -DEV
        update_manager.devtool_camera = enableCameraUpdate;
        if(enableCameraUpdate) { camera_update_unlocker.updateUpgrades(); }
        
        //SOUND UPDATE -DEV
        update_manager.devtool_sound = enableSoundUpdate;
        if(enableSoundUpdate) { sound_update_unlocker.updateUpgrades(); }

        //DPAG / PLAYER
        dpadQuest.devtool = giveDPad;
        if(shakeCamera) { cameraShake.shakeCamera(); shakeCamera = false; }
        killperformer.godMode = godMode;

        //QUESTS
        if(endMQ2) { mq2.devtool = true; }
        if(endUnlockCamera) { quest_camera.devtool = true; }

        //MONSTER - DEV
        if(spawnMonster) { monster.SetActive(true); }
        if(kill) { killperformer.reached(); kill = false; }
        if(updateDeathCount) { killperformer.deathCount = deathCount; updateDeathCount = false;}
        if(endGame) { quest_help.devtool = true; }

        //WORLD
        sun.SetActive(day);
    }
}

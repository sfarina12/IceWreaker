using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PC_inputOutput : MonoBehaviour
{
    public KeyCode upkey;
    public KeyCode downkey;
    public KeyCode enterkey;
    [Space,Tooltip("the PC associated with this screen. This is to avoid pressing the same imput for multiple PCs")]
    public Interact interactObject;
    [Space,Tooltip("the Screen object associated with the PC. Used to avoid makeing multiple PC cameras")]
    public GameObject screen;
    [Space]
    public PC_zoom pczoom;
    [Space]
    public audioPlayer keyAudio;
    public cameraInterface_update camera_update;

    TextMeshPro output;
    string buffer = "";

    List<string> memory;
    List<int> availableIndexes;
    List<int> choises;
    playerInteractHandler interacter;
    [HideInInspector] public int menuIndex = 0;
    [HideInInspector] public bool is_open = false;
    [HideInInspector] public int enter = -1;

    //$ per non stampare la righa come selezione ma come stesto normale
    void Start() { screen.SetActive(is_open); output = screen.GetComponent<TextMeshPro>(); interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();}
    int i=0;
    void Update() {
        //TODO:
        //per qualche motivo l'isInteracting non funziona in questa classe. Devo farlo manualmente l'interazione
        //se lo si risolve PLZ, mettere a posto questo
        if (Input.GetKeyDown(KeyCode.F) && interactObject.outlineableObject.enabled && pczoom.quest.canZoom) {
            if(!pczoom.zooming) {
                is_open = !is_open;
                screen.SetActive(is_open);

                if(is_open) {
                    if(camera_update.open) { camera_update.closeCameraInterface(true); } 
                } else { 
                    if(!camera_update.open && camera_update.wasOpen) { camera_update.openCameraInterface(); } 
                } 
            } else {
                //override on state of interacter, since it doesn't know if it actually can interact
                interactObject.isOn = !is_open;
            }
        }

        //----------------------[ADDITIONAL CHECKING FOR IS PLAYER IN FRONT OF A FUCKING BLACK SCREEN]----------------------
        //controllo se mi trovo nella destination ma lo schermo e spento
        if(pczoom.cameraDestination.transform.position == pczoom.playerCamera.transform.position) {
            if(!pczoom.zooming) {
                is_open = true;
                screen.SetActive(is_open);
            }
        } else {
            is_open = false;
            screen.SetActive(is_open);
        }
        //END ------------------[ADDITIONAL CHECKING FOR IS PLAYER IN FRONT OF A FUCKING BLACK SCREEN]----------------------
        
        if(is_open) {    
            if(Input.GetKeyDown(upkey)) { moveDown(); keyAudio.playAudio(); }
            if(Input.GetKeyDown(downkey)) { moveUp(); keyAudio.playAudio(); }
            if(choises.Count > 0)
                if(Input.GetKeyDown(enterkey)) { enter = choises[menuIndex]; keyAudio.playAudio(); }
        }

        output.text = buffer;
    }

    public void menu(List<string> indexes,bool clearMenuIndex = true) {
        memory = new List<string>();
        memory = indexes;
        availableIndexes = new List<int>();
        choises = new List<int>();
        enter = -1;
        int i = 0;
        foreach(string t in memory) {
            if(!t.StartsWith("$")) {
                availableIndexes.Add(i);
                choises.Add(int.Parse(t.Substring(0,t.IndexOf("#"))));
            }
            i++;
        }
        if(clearMenuIndex) { menuIndex = 0; }
        updateScreen();
    }

    void moveUp() {
        if(menuIndex < availableIndexes.Count-1) menuIndex++;
        else menuIndex=0;
        updateScreen();
    }
    void moveDown() {
        if(menuIndex > 0) menuIndex--;
        else menuIndex = availableIndexes.Count-1;
        updateScreen();
    }
    void updateScreen(){
        clear();
        int i=0;
        foreach(string t in memory) {
            if(availableIndexes.Contains(i)) {
                if(menuIndex == availableIndexes.IndexOf(i)) {
                    if(!t.StartsWith("$")) selected(removeKeyCharacter(t));
                    else println(removeKeyCharacter(t));
                }
                else println(removeKeyCharacter(t));
            } else println(removeKeyCharacter(t));
            i++;
        }
    }
    void println(string text) { buffer+=text+"\n"; }
    void print(string text) { buffer+=text; }
    void clear() { buffer=""; }
    void selected(string text) {
        buffer += "<mark=#14FF00aa>";
        println(text);
        buffer += "</mark>";
    }
    string removeKeyCharacter(string text) { 
        if(text.StartsWith("$")) return text.Substring(1,text.Length-1);
        if(text.Contains("#")) return text.Substring(text.IndexOf("#"),text.Length-1);
        return text;
    }
    //automatically zooms out from the PC. Can re-zoom again after
    public void kickOutOfPC() {
        interactObject.interactByScript = true;
        is_open=false;
        screen.SetActive(is_open);
        if(!camera_update.open && camera_update.wasOpen) { camera_update.openCameraInterface(); } 
    }
}

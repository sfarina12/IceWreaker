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

    TextMeshPro output;
    string buffer = "";

    int menuIndex = 0;
    List<string> memory;
    List<int> availableIndexes;
    List<int> choises;

    [HideInInspector]
    public int enter = -1;

    //$ per non stampare la righa come selezione ma come stesto normale
    void Start() { output = this.gameObject.GetComponent<TextMeshPro>(); }
    int i=0;
    void Update()
    {
        if(Input.GetKeyDown(upkey)) moveDown();
        if(Input.GetKeyDown(downkey)) moveUp();
        if(Input.GetKeyDown(enterkey)) {enter = choises[menuIndex];}

        output.text = buffer;
    }

    public void menu(List<string> indexes) {
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
        menuIndex = 0;
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
}

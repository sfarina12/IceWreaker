using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PC_inputOutput : MonoBehaviour
{
    public KeyCode upkey;
    public KeyCode downkey;

    TextMeshPro output;
    string buffer = "";
    int menuIndex=0;
    List<string> memory;
    void Start()
    {
        output = this.gameObject.GetComponent<TextMeshPro>();
        memory = new List<string>();

        //testing
        List<string> test = new List<string>(); 
        test.Add("scelta 1111111111111111111111111111111111111111");
        test.Add("scelta 2222222222222222222222222222222222222222");
        test.Add("scelta 3333333333333333333333333333333333333333");
        test.Add("scelta 4444444444444444444444444444444444444444");
        test.Add("scelta 4444444444444444444444444444444444444444");
        test.Add("scelta 4444444444444444444444444444444444444444");
        test.Add("scelta 4444444444444444444444444444444444444444");
        test.Add("scelta 4444444444444444444444444444444444444444");
        test.Add("scelta 4444444444444444444444444444444444444444");
        menu(test);
    }
    
    void Update()
    {
        if(Input.GetKeyDown(upkey)) moveDown();
        if(Input.GetKeyDown(downkey)) moveUp();

        output.text = buffer;
    }

    public void menu(List<string> indexes) {
        memory = indexes;
        menuIndex = 0;
        updateScreen();
    }

    void moveUp() {
        if(menuIndex < memory.Count-1) menuIndex++;
        else menuIndex=0;
        updateScreen();
    }
    void moveDown() {
        if(menuIndex > 0) menuIndex--;
        else menuIndex = memory.Count-1;
        updateScreen();
    }
    void updateScreen(){
        clear();
        int i=0;
        foreach(string t in memory) {
            if(i==menuIndex) selected(t);
            else println(t);
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
}

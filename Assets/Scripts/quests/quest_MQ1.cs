using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class quest_MQ1 : MonoBehaviour
{
    public PC_inputOutput PC;
    public Animator doorAnimator;
    public List<lightSwitching> all_switches;
    public animationsStates state;
    public List<GameObject> lightPCs;
    

    [HideInInspector]
    public bool questEnded = false;
    [HideInInspector]
    public bool canZoom = true;
    int menus = 0; 
    List<string> screen;
    bool performad = false;
    void Start() {
        screen = new List<string>(); 
        screen.Add("$SHIP MANTENANCE SYSTEM");
        screen.Add("$////////////////////////");
        screen.Add("$ALL ANAUTORIZED PERSONNAL MUST CLOSE THIS TERMINAL IMMEDIATLY");
        screen.Add("$DON'T APPLY ANY MODIFICATION WITHOUT THE ADMINISTRATOR AUTORIZATION");
        screen.Add("$");
        screen.Add("1#ENGINE STATUS");
        screen.Add("2#START ENGINE");
        screen.Add("3#DIAGNOSTIC");
        PC.menu(screen);
    }

    void Update() {
        if(PC.enter != -1) {
            menus = PC.enter;
            if(menus <= 4) {
                switch(menus) {
                    case 0: 
                        screen = new List<string>(); 
                        screen.Add("$SHIP MANTENANCE SYSTEM");
                        screen.Add("$////////////////////////");
                        screen.Add("$ALL ANAUTORIZED PERSONNAL MUST CLOSE THIS TERMINAL IMMEDIATLY");
                        screen.Add("$DON'T APPLY ANY MODIFICATION WITHOUT THE ADMINISTRATOR AUTORIZATION");
                        screen.Add("$");
                        screen.Add("1#ENGINE STATUS");
                        screen.Add("2#START ENGINE");
                        screen.Add("3#DIAGNOSTIC");
                        if(performad) {
                            screen.Add("$");
                            screen.Add("4#OPEN MAIN ENGINE ROOM");
                        }
                    break;
                    case 1:
                        screen = new List<string>(); 
                        screen.Add("$ENGINE STATUS:");
                        screen.Add("$");
                        screen.Add("$CAN'T ACCESS ENGINE STATUS DATA, PLEASE EXECUTE A ENGINE DIAGNOSTIC FOR MORE INFORMATIONS");
                        screen.Add("$");
                        screen.Add("0#BACK");
                    break;
                    case 2:
                        screen = new List<string>(); 
                        screen.Add("$STARTING MAIN ENGINE ROUTINES:");
                        screen.Add("$");
                        screen.Add("$THERE WAS AN ERROR DURING ENGINE STARTING ROUTINES.");
                        screen.Add("$FOR MORE INFORMATION PERFORME A DIAGNOSTIC OR CHECK THE ENGINE STATUS");
                        screen.Add("$");
                        screen.Add("0#BACK");
                        
                    break;
                    case 3:
                        screen = new List<string>(); 
                        screen.Add("$PERFORMING DIAGNOSTIC:");
                        screen.Add("$");
                        screen.Add("$ERRORS WILL BE LISTED BELOW");
                        screen.Add("$FOUND AN ISSUE WITH ENGINE PISTON BELT.");
                        screen.Add("$BELT CONDITION: PRECARIOUS");    
                        screen.Add("$ESTIMATED OPERABILITY: 5%");
                        screen.Add("$IT IS HIGHLY RACCOMANDED TO CHANGE THE DAMAGED COMPONENT");                        
                        screen.Add("$");
                        screen.Add("0#BACK");
                        performad = true;
                    break;
                    case 4:
                        screen = new List<string>(); 
                        screen.Add("$WARNING");
                        screen.Add("$////////////////////////");
                        screen.Add("$LACK OF ENERGY DETECTED!");
                        screen.Add("$BY OPENING THE MAIN ENGINE DOOR THERE WILL NOT BE ENOUGH ENERGY TO MAINTAIN SHIP BASIC ENERGY REQUIREMENTS");               
                        screen.Add("$");
                        screen.Add("$CONTINUE?");
                        screen.Add("5#Y");
                        screen.Add("0#N");
                    break;
                }
                PC.menu(screen);
            } else {
                screen = new List<string>(); 
                screen.Add("$OPENING MAIN ENGINE DOOR");
                screen.Add("$PLEASE WAIT UNTIL THE SEQUENZE IS ENDED");
                PC.menu(screen);
                doorAnimator.Play("heavyDoorOpenClose");
            }
        }

        if(state.isEnded) {
            foreach(lightSwitching a in all_switches) { a.turnoOff(); }
            foreach(GameObject l in lightPCs) l.SetActive(false);
            
            screen = new List<string>(); 
            PC.menu(screen);

            canZoom = false;
            questEnded = true;
        }
    }
}

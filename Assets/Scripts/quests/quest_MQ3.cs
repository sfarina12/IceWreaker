using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_MQ3 : MonoBehaviour
{
    //1 - ridirezionare l'energia al ponte
    //2 - avviare il timer della radio
    //3 - aprire la porta EXIT
    //4 - fine gioco

    public quest_MQ2 mq2;
    public PC_inputOutput PC;
    public List<lightSwitching> off_switches;
    public List<lightSwitching> on_switches;
    public List<GameObject> on_supplementaryLights;
    public List<GameObject> off_supplementaryLights;
    

    [HideInInspector]
    public bool questEnded = false;
    [HideInInspector]
    public bool canZoom = true;
    int menus = 0; 
    List<string> screen;
    bool redirected = false;
    bool one_time = false;
    void Start() {  }

    void Update() {
        if(mq2.questEnded) {
            if(!one_time) {
                screen = new List<string>(); 
                screen.Add("$ENGINE STATUS: OPERATIONAL");
                screen.Add("$POWER OUTPUT: 50%");
                screen.Add("$THERE IS NOT ENOUTH POWER TO SUSTAIN ADVANCED SHIP FUNCTIONALITY.");
                screen.Add("$DEVICES WHO USES MORE POWER THAN 50KW/h MAY NOT FUNCTION.");
                screen.Add("$");
                screen.Add("$");
                screen.Add("0#CONTINUE");
                PC.menu(screen);
                one_time = true;
            }

            if(PC.enter != -1) {
                menus = PC.enter;
                if(menus <= 6) {
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
                            screen.Add("4#REDIRECT ENERGY");
                        break;
                        case 1:
                            screen = new List<string>(); 
                            screen.Add("$ENGINE STATUS:");
                            screen.Add("$");
                            screen.Add("$THE ENGINE IS RUNNING AT LOW VOLTAGE. EMERGENCY LIGHTS: ACTIVE");
                            if(redirected) { screen.Add("$POWERING ROOM: BRIDGE."); }
                            screen.Add("$");
                            screen.Add("0#BACK");
                        break;
                        case 2:
                            screen = new List<string>(); 
                            screen.Add("$STARTING MAIN ENGINE ROUTINES:");
                            screen.Add("$");
                            screen.Add("$THE ENGINE IS ALREADY RUNNING.");
                            screen.Add("$");
                            screen.Add("0#BACK");
                        break;
                        case 3:
                            screen = new List<string>(); 
                            screen.Add("$PERFORMING DIAGNOSTIC:");
                            screen.Add("$");
                            screen.Add("$ERRORS WILL BE LISTED BELOW");
                            screen.Add("$NO ISSUE FOUND IN ENGINE OPERATIVITY");
                            screen.Add("$BELT CONDITION: WORKABLE");    
                            screen.Add("$ESTIMATED OPERABILITY: 20%");
                            screen.Add("$IT IS HIGHLY RACCOMANDED TO AVOID SHIP MOVEMENT.");                        
                            screen.Add("$");
                            screen.Add("0#BACK");
                        break;
                        case 4:
                            string index = redirected ? "5" : "6";
                            screen = new List<string>(); 
                            screen.Add("$WARNING");
                            screen.Add("$////////////////////////");
                            screen.Add("$BY REDIRECTING THE ENERGY THERE WILL BE NOT ENOUGH ENERGY TO POWER THE ENTIRE VASSEL BUT A ROOM");
                            screen.Add("$");
                            screen.Add(index+"#BRIDGE");
                            screen.Add("$");
                            screen.Add("0#BACK");
                        break;
                        case 5:
                            screen = new List<string>(); 
                            screen.Add("$REDIRACTION FAILED");
                            screen.Add("$////////////////////////");
                            screen.Add("$THE ENGINE IS ALREADY POWERING THE ROOM: BRIDGE");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("0#BACK");
                        break;
                        case 6:
                            screen = new List<string>(); 
                            screen.Add("$ENERGY SUCCESSULLY REDIRECTED.");
                            screen.Add("$THE BRIDGE IS NOW OPERATIONAL.");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("0#BACK");

                            foreach(lightSwitching a in off_switches) { a.turnoOff(); }
                            foreach(lightSwitching a in on_switches) { a.turnoOn(); }
                            foreach(GameObject l in off_supplementaryLights) l.SetActive(false);
                            foreach(GameObject l in on_supplementaryLights) l.SetActive(true);

                            redirected = true;
                        break;
                    }
                    PC.menu(screen);
                }
                // } else {
                //     screen = new List<string>(); 
                //     screen.Add("$ENERGY SUCCESSULLY REDIRECTED.");
                //     screen.Add("$THE BRIDGE IS NOW OPERATIONAL.");
                //     PC.menu(screen);
    
                //     foreach(lightSwitching a in off_switches) { a.turnoOff(); }
                //     foreach(lightSwitching a in on_switches) { a.turnoOn(); }
                //     foreach(GameObject l in off_supplementaryLights) l.SetActive(false);
                //     foreach(GameObject l in on_supplementaryLights) l.SetActive(true);
    
                //     screen = new List<string>(); 
                //     PC.menu(screen);
                //     redirected = true;
                // }
            }
        }
    }
}

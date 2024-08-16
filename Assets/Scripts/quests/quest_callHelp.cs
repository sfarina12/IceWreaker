using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class quest_callHelp : MonoBehaviour
{
    public PC_inputOutput PC;
    [Space]
    public GameObject needleAplitude;
    public GameObject needlePeriod;
    [Space]
    public float move_amp = 40;
    public float move_per = 40;
    [Space]
    //range da 0 - 100
    public int start_amp = 40;
    //range da 0 - 200
    public int start_per = 10;
    [Space]
    public GameObject monster;
    public float farDistance;
    public float nearDistance;
    public float closeDistance;
    public float behindDistance;
    [Space]
    public int timeBeforeEnd = 5;
    public float timeBeforeNextPump = 15f;
    public Animator endAnimator;
    [Space]
    public objectSoundSetter objectSound;
    public AudioSource pumpSound;

    
    [HideInInspector] public float act_nextPump = 0;
    List<string> screen;
    bool is_ended = false;
    int menus = 0; 
    int amplitude = 0;
    int period = 0;
    bool firstCheck = false;
    bool scareCheck = false;
    bool exitedMenu = true;
    int lastMenuPosition = 0;
    string monsterDistance = "NEAR";
    string oldDistance;
    [HideInInspector] public bool startedTimer = false;
    [HideInInspector] public bool devtool = false;
    [HideInInspector] public bool questEnded = false;
    
    void Start() {
        amplitude = start_amp;
        period = start_per;

        screen = new List<string>();
        screen.Add("$MAIN BRIDGE COMPUTER ASSISTANT PROGRAM"); 
        screen.Add("$General Industries INC.");
        screen.Add("$////////////////////////");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$TERMS OF SERVICE");
        screen.Add("$////////////////////////");
        screen.Add("$THIS PROGRAM IS UNDER THE Generic Safety License TM");
        screen.Add("$THE ILLICIT USE OF THIS SOFTWARE IS PUNISHIBLE WITH Ḋ̶̠̱̖̦̭͚̮͇͙̈́̓̽̑̈̌͘͜͜͝͝            E̵͇̗͚̱̙͔͚͆̄͋̒̈́̑͊̋̂͘̚           À̸̡̢̗̥͎̺̮̳̪̱̗͉͍͇͈̯̯͔̐̍̋̏̒͐̒̀͜͠               T̶͍͚̗̭̓̑̀͒͘̚͠       H̸̡̨̦̲̼͉̩̫̦͈̹̥̘̹̥͍̞̄͜");
        screen.Add("$");
        screen.Add("$");
        screen.Add("$");
        screen.Add("1#I DO UNDERSTAND AND WISH TO CONTINUE");
        PC.menu(screen);
    }

    
    void Update() {

        if(act_nextPump > 0) { act_nextPump-=Time.deltaTime; }
        if(devtool) { endAnimator.Play("resque_door"); }

        float distance = Vector3.Distance(monster.transform.position,transform.position);
        if(distance < farDistance || distance >= farDistance) { monsterDistance = "FAR"; }
        if(distance < nearDistance) { monsterDistance = "NEAR"; }
        if(distance < closeDistance) { monsterDistance = "CLOSE"; }
        if(distance < behindDistance) { monsterDistance = "BEHIND"; }
        
        if(!is_ended) {
            if((PC.enter != -1) || (oldDistance != monsterDistance && scareCheck)) {
                menus = PC.enter != -1 ? PC.enter : menus;
                screen = new List<string>();
                if(scareCheck) {
                    exitedMenu = true;
                    screen.Add("$MAIN BRIDGE COMPUTER ASSISTANT PROGRAM"); 
                    screen.Add("$////////////////////////");
                    screen.Add("$");
                    screen.Add("$BRIDGE OPERATIVITY:                                                       1%");
                    screen.Add("$SHIP'S WHEEL LICK STATUS:                    UNLOCKED");
                    screen.Add("$ENGINE STATUS:                                      LOW VOLTAGE");
                    screen.Add("$G  ̸͕̆ R̴̼̠̅   I̴̠͕͆    M̴͉̞̌    Y̵͈̟͌͝     ̵̳̲͒    P̴̲̔͛    I̶̘̼̍    G̷͔͖͐͝      ̴̤͒    L̷̜͊͝    Ỏ̷̼́    C̴͉̾   Ǎ̶̰̣    T̴̹̼͒    İ̶̻̏    Ō̸̗ͅ    Ṋ̴̺̎    :         "+monsterDistance);
                    screen.Add("$");
                    screen.Add("$////////////////////////");
                    screen.Add("$");
                }

                Vector3 tmp_pos;
                switch (menus) {
                    case 1: 
                        screen.Add("$     Ņ̶̥̿̈́     Ȏ̷͗ͅ      ̴͍̽     Ỷ̵̮     O̴͖͒     U̴̻͂      ̵̨̝̄͌     H̷͉̕     Ä̶̹͇́     V̶̤̉ͅ     È̷͙̞      ̴̘͓̏     N̶̰͔̑     Ǒ̴̲̊     T̴͙̂̄"); 
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("2#     İ̸̹     '̷̢͚̿     M̵̹͂̓      ̶̝̎ͅ     W̸̱̃     E̴͇͎̍̌     A̴̠͑     K̸̤̣͝     ");
                        PC.menu(screen);
                        scareCheck = true;
                    break;
                    case 2: 
                        screen.Add("3#PURGE MAIN ENGINE PUMP"); 
                        screen.Add("$");
                        screen.Add("3#OVERCHARGE MAIN ENGINE PUMP");
                        screen.Add("$");
                        screen.Add("4#CALL FOR HELP");
                        screen.Add("$");
                        screen.Add("$");
                        if(firstCheck) { screen.Add("6#SET UP RADIO FREQUENCIES"); }
                        PC.menu(screen);
                    break;
                    case 3: 
                        if(act_nextPump <= 0 && exitedMenu) {
                            act_nextPump = timeBeforeNextPump;
                            screen.Add("$OPERATING ON PUMP");
                            screen.Add("$EARING PROTECTION IS ADVICED");
                            objectSound.activateSound();
                            pumpSound.enabled = false;
                            pumpSound.enabled = true;
                            exitedMenu = false;
                        } else {
                            screen.Add("$OTHER OPERATIONS MUST BE FINISHED BEFORE STARTING A NEW ONE.");
                        }
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("2#BACK");
                        PC.menu(screen);
                    break;
                    case 4: 
                        screen.Add("$CALLING FOR HELP");
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("$CONTINUE TO SCAN FREQUENCIES");
                        screen.Add("$");
                        screen.Add("5#CONTINUE");
                        PC.menu(screen);
                    break;
                    case 5: 
                        if(period == 160 && amplitude == 60 && startedTimer) {
                            screen.Add("$HELP ALREADY CALLED");
                            screen.Add("$     C̸͚̯̣͇̠̭̰͍̦̖̗͇̜̓͂̃͂͂̍̑̑̎͂̏̆͜͝͠     H̷̘̫̬̓͑͂̏̅̿̐̔͠     Ḛ̵̯͚̘̝̘̘̟̆̌͐́͌͘͜     C̶̱̞̩̜͗͊̋̀̃̔̓̌̌̚͠͝͠͝     K̸̨̢̢̡̛̦̙̞̰͚̮͚̤͛̾̐̏̇̂̓͌̄̋̄͆͠      ̷̼͍̠̺̬͈̝̓͗̊̏̔͜͜ͅ     T̶̢͖͙̖͇̟̟̮̮̻͓̯̦̮̘̾̂̿̆̈̓̿̕     Ḩ̸̡̬̳̤͖̣͖̪̳̜̆̂̈́́     E̵̺̳͚͎̩̬͙̥͋      ̷̬͕͖̬͍̭͕͆͑̉̍     T̷̲̭̟̼̤̩̖̮̙͙̩̋     I̸̧̲̖̦̣̬̯̲̙̻̕     M̸͚̣̺͚͈͉̳͇͖̬̞͙͓̼͋̆̅̉ͅ     E̴͉͖̓͐͛͑̽     Ŗ̴̨̹̲̦̹̆͐");
                            screen.Add("$");
                            screen.Add("$HELP IS COMING IN: 0"+timeBeforeEnd+":00");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("2#BACK");
                        }

                        if(period == 160 && amplitude == 60 && !startedTimer) {
                            screen.Add("$FREQUENCY CHANNEL FOUND");
                            screen.Add("$CONNECTION ENSTABLISHED");
                            screen.Add("$");
                            screen.Add("$HELP IS COMING IN: 0"+timeBeforeEnd+":00");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("2#BACK");

                            startedTimer = true;
                            StartCoroutine(timer());
                        }

                        if(period != 160 && amplitude != 60 && !startedTimer) {
                            screen.Add("$NO FREQUENCY FOUND ON THIS CHANNEL");
                            screen.Add("$PLEASE USE THE RADIO TO ABJUST THE FREQUENCY");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("$");
                            screen.Add("2#CONTINUE");
                            
                            firstCheck = true;
                        }

                        PC.menu(screen);
                    break;
                    case 6: 
                        frequencyLoop:

                        screen.Add("$");
                        screen.Add("$AMPLITUDE  ~~"+amplitude);
                        screen.Add("$");
                        screen.Add("7#       - ▲ -");
                        screen.Add("$");
                        screen.Add("8#       + ▼ +");
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("$");
                        screen.Add("$       PERIOD  ~~"+period);
                        screen.Add("$");
                        screen.Add("9#       - ▲ -");
                        screen.Add("$");
                        screen.Add("0#       + ▼ +");
                        screen.Add("$");
                        screen.Add("2#EXIT");
                        PC.menuIndex = lastMenuPosition;
                        PC.menu(screen,false);
                    break;
                    case 7: 
                        if(amplitude > 0) {
                            amplitude -= 10; 
                            tmp_pos = needleAplitude.transform.localPosition;
                            needleAplitude.transform.localPosition = new Vector3(tmp_pos.x,tmp_pos.y,tmp_pos.z - move_amp);
                            lastMenuPosition = 0;
                        }
                        goto frequencyLoop;
                    case 8: 
                        if(amplitude < 100) {
                            amplitude += 10; 
                            tmp_pos = needleAplitude.transform.localPosition;
                            needleAplitude.transform.localPosition = new Vector3(tmp_pos.x,tmp_pos.y,tmp_pos.z + move_amp);
                            lastMenuPosition = 1;
                        }
                        goto frequencyLoop;
                    case 9: 
                        if(period > 0) {
                            period -= 10; 
                            tmp_pos = needlePeriod.transform.localPosition;
                            needlePeriod.transform.localPosition = new Vector3(tmp_pos.x,tmp_pos.y,tmp_pos.z - move_per);
                            lastMenuPosition = 2;
                        }
                        goto frequencyLoop;
                    case 0:
                        if(period < 200) { 
                            period += 10; 
                            tmp_pos = needlePeriod.transform.localPosition;
                            needlePeriod.transform.localPosition = new Vector3(tmp_pos.x,tmp_pos.y,tmp_pos.z + move_per);
                            lastMenuPosition = 3;
                        }
                        goto frequencyLoop;
                }
            }
        
            if(oldDistance != monsterDistance) { oldDistance = monsterDistance; }
        }
    }

    IEnumerator timer() {
        yield return new WaitForSeconds(timeBeforeEnd*60);
        endAnimator.Play("resque_door");
        questEnded = true;
    }
}

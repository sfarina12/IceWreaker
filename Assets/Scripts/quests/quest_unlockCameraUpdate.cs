using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class quest_unlockCameraUpdate : MonoBehaviour
{
    public PC_inputOutput PC;
    [Space]
    public int enemyEalth = 300;
    [Space]
    public Animator animator;
    public GameObject updateFloppy;
    [Space] 
    public GameObject dog_sprite;
    public GameObject dog_sprite_2;
    public GameObject cat_sprite;
    public GameObject cat_sprite_2;
    public GameObject fox_sprite;
    public GameObject fox_sprite_2;
    public GameObject wendigo_sprite;
    public GameObject wendigo_sprite_2;


    [HideInInspector] public bool questEnded = false;
    int menus = 0; 
    List<string> screen;
    int selected_pokemon = 0;
    int gamePhase = -1;
    float timer = 0;
    int avanzamento = 1;
    bool draw_update = false;
    //player
    int act_hp;
    int act_st;
    int act_dfs;
    //enemy
    int E_act_hp;
    int E_act_st;
    int E_act_dfs;
    string message;
    bool was_cured = false;
    bool is_ended = false;
    [HideInInspector] public bool devtool = false;
    
    List<pokemon> pokemons;
    //mosse
    List<mossa> mosse = new List<mossa>(){
        //nome|stamina|dmg|staminaRecover|dfs|hpRevocer
        new mossa("RED MOON"        ,60 ,70 ,0 ,0  ,0  ),
        new mossa("BITE"            ,10 ,30 ,5 ,0  ,0  ),
        new mossa("SNOW STORM"      ,50 ,40 ,10,40 ,10 ),
        new mossa("LICK WOUNDS"     ,30 ,0  ,0 ,-10,30 ),
        new mossa("EYE OF THE STORM",70 ,100,0 ,0  ,-10),
        new mossa("VIOLENCE"        ,100,170,0 ,-40,-50),
        new mossa("                                                                                               I̴̛̱͙̱̳̎̐́͌͘͜C̴̡̻͙͉̬̞̰̻̲̖̮͇̗͇͇͎͓͎̈͌̚͝E̸̩̖͖̩̮̦͈͔̎͂̾̋̄̒͑͘̕ͅW̶̡̗͔̳͈͍̹̪̻̺̪̭͎͕͓̩̥̞̹̙͔̰̗̣̳͛̾͜͜͝ͅŘ̷̟̭͔̩̗͚́͌͋̀̀Ę̵̡̢̛̛̹̪̘̲̺͕̗̲̯̰̪͙̜͖̖̗̻͈̣̬̽́̀̓̐̆̓̀̀̽̿̓̆̒̎͛͜͝ͅÄ̵̡̛̛̻͈͚̬̜̲̜͛̂̆̏̋̄́̔̅̀̂́͊̓͛̇̄͊͑̓͠ͅK̸̡̡̬̞͕̜̠͎̬̫̪̼̦̼̺̯͔͚͍̥̝̩̜̬̐́̀͆̈́͛̈̈́͊͆͌̐̂̓̕͝Ë̶̢̺̤̯̣͍̬̭̝̫̼̲͔͖͖̖̟͕͈̣̗͕́̑͌̄̓̔̈͛͘͜ͅR̴̨̧̡̛̰͕̻̟̰̜̗̦̹̯͔̞̩̻͓͖͓͔̦̃̋̑̆̈͑̿̿̓̾̋̏̇͐̐̓̅̾͒̎̀͠͝͝ͅ"    ,120,0  ,0 ,100,20),
        new mossa("REST"            ,0  ,0  ,50 ,0  ,10  ),
    };

    void Start() {
        pokemons = new List<pokemon>() {
                new pokemon(fox_sprite    ,fox_sprite_2    ,"Jokulsarlon Glacier",100,60 ,185 ,new int[]{0,7,2,3}),
                new pokemon(dog_sprite    ,dog_sprite_2    ,"Pinnacle"           ,150,80 ,180 ,new int[]{3,4,2,1}),           
                new pokemon(cat_sprite    ,cat_sprite_2    ,"Pleneau Bay"        ,110 ,200,110 ,new int[]{5,7,0,2}),
                new pokemon(wendigo_sprite,wendigo_sprite_2,"ừ̴̢̧̟̺͎̰̰͕̝̭̲̣̻̼̩͐̈̿͐̋̈͆̈́͘̕͠ͅn̸̡̛̗̹͖̺̥͇̻̤͕̠̠̾̿͛̀̍̎̂̆́̑̿́̾̍͑̆̈̿̉̐̕͘͘͜͝å̷̡̡̨͙̼͚̮̫̱͖̪͔͙̘̗̦͇̳̳̖͔̜̱͍̗̣͕̀͒͂̏͂̃͝͠ͅm̶̻̮̥͖͙̬̞̣̻̱̞̗̆͋̇͛͌̐͂̃̂͆̏̔̐̕ͅe̸̢͙̥͇̻̼̗̟̅̇̍͐̅̽͒̈́̍̎̏̕ͅḓ̸̢̝̻͉̟̜̠̈́̒̆̽͘"            ,150,160,enemyEalth,new int[]{5,7,2,6})        
        };
        E_act_hp = pokemons[3].hp;
        E_act_st = pokemons[3].stamina;
        E_act_dfs = pokemons[3].dfs;

        screen = new List<string>();
        screen.Add("$----------------------------------------------"); 
        screen.Add("$000000000000000000000000000000000000");
        screen.Add("$000111110111101111010001011110100100");
        screen.Add("$000001000100001000011011010010110100");
        screen.Add("$000001000100001000010101010010101100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001110010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000111110111101111010001011110100100");
        screen.Add("$000000000000000000000000000000000000");
        screen.Add("$----------------------------------------------"); 
        screen.Add("$1111BATTLE!1DEFEAT!1BE1DEFEATED!1111"); 
        screen = convertForPC(screen);
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("0#[[[[[[[[[[[START[GAME![[[[[[[[[[[[[");
        screen = convertForPC(screen,true);
        PC.menu(screen);
    }

    void restart(){
        pokemons[0].sprite_2.SetActive(false);
        pokemons[1].sprite_2.SetActive(false);
        pokemons[2].sprite_2.SetActive(false);
        pokemons[3].sprite_2.SetActive(false);
        gamePhase = 0;
        selected_pokemon = 0;
        pokemons = new List<pokemon>() {
                new pokemon(fox_sprite    ,fox_sprite_2    ,"Jokulsarlon Glacier",100,60 ,185 ,new int[]{0,7,2,3}),
                new pokemon(dog_sprite    ,dog_sprite_2    ,"Pinnacle"           ,150,80 ,180 ,new int[]{3,4,2,1}),           
                new pokemon(cat_sprite    ,cat_sprite_2    ,"Pleneau Bay"        ,110 ,200,110 ,new int[]{5,7,0,2}),
                new pokemon(wendigo_sprite,wendigo_sprite_2,"ừ̴̢̧̟̺͎̰̰͕̝̭̲̣̻̼̩͐̈̿͐̋̈͆̈́͘̕͠ͅn̸̡̛̗̹͖̺̥͇̻̤͕̠̠̾̿͛̀̍̎̂̆́̑̿́̾̍͑̆̈̿̉̐̕͘͘͜͝å̷̡̡̨͙̼͚̮̫̱͖̪͔͙̘̗̦͇̳̳̖͔̜̱͍̗̣͕̀͒͂̏͂̃͝͠ͅm̶̻̮̥͖͙̬̞̣̻̱̞̗̆͋̇͛͌̐͂̃̂͆̏̔̐̕ͅe̸̢͙̥͇̻̼̗̟̅̇̍͐̅̽͒̈́̍̎̏̕ͅḓ̸̢̝̻͉̟̜̠̈́̒̆̽͘"            ,150,160,enemyEalth,new int[]{5,7,2,6})        
        };
        E_act_hp = pokemons[3].hp;
        E_act_st = pokemons[3].stamina;
        E_act_dfs = pokemons[3].dfs;

        screen = new List<string>();
        screen.Add("$----------------------------------------------"); 
        screen.Add("$000000000000000000000000000000000000");
        screen.Add("$000111110111101111010001011110100100");
        screen.Add("$000001000100001000011011010010110100");
        screen.Add("$000001000100001000010101010010101100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001110010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000001000100001000010001010010100100");
        screen.Add("$000111110111101111010001011110100100");
        screen.Add("$000000000000000000000000000000000000");
        screen.Add("$----------------------------------------------"); 
        screen.Add("$1111BATTLE!1DEFEAT!1BE1DEFEATED!1111"); 
        screen = convertForPC(screen);
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("0#[[[[[[[[[[[START[GAME![[[[[[[[[[[[[");
        screen = convertForPC(screen,true);
        PC.menu(screen);
    }

    void Update() {
        if(devtool) {
            animator.Play("unlockFloppy");
            is_ended = true;
        }

        if(!is_ended) {
            if((PC.enter != -1 || draw_update) && gamePhase < 3) {
                menus = PC.enter;
                draw_update = false;
                switch(gamePhase) {
                    case -1:
                        if(menus == 0) { gamePhase = 0; draw_update=true; break;}
                    break;
                    case 0:
                        if(menus == 1) { draw_update=true; gamePhase = 1; break;}

                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$1111111111111111RULES1111111111111111");
                        screen.Add("$PICK  A  ICEMON  AND  DEFEAT  THE  OPPONENT!");
                        screen.Add("$USE  ALL  THE  MOVES  AT  YOUR  DISPOSAL  AND  WIN!");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$111111111111111WARNING111111111111111");
                        screen = convertForPC(screen);
                        screen.Add("$THIS  GAME  CONTAINS  GRAPHIC  VIOLENCE,  IT  IS");
                        screen.Add("$HIGHLY  SUGGESTED  TO  NOT  PLAY  THE  GAME");
                        screen.Add("$  IF  SUFFERING  FROM  CARDIOVASCULAR  PROBLEMS.");
                        screen.Add("$YOU  HAVE  BEEN  WARNED");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("1#[[[[[[[[[[[START[GAME![[[[[[[[[[[[[");
                        screen = convertForPC(screen,true);
                    break;
                    case 1:
                        switch(menus) {
                            case 3:
                                selected_pokemon++;      
                                if(selected_pokemon == pokemons.Count-1) selected_pokemon = 0;   
                            break;
                            case 2:
                                gamePhase = 2;
                                draw_update=true;
                            break;
                        }

                        screen = new List<string>(); 
                        screen = printPokemon(selected_pokemon,screen);
                        screen = convertForPC(screen);
                        screen = printPokemonMoves(selected_pokemon,screen);
                        screen.Add("$000000000000000000000000000000000000");
                        screen = convertForPC(screen);
                        screen.Add("$ ");
                        screen.Add("2#[[[[[[[[[[[[[[CHOOSE[IT[[[[[[[[[[[[[");
                        screen.Add("$ ");
                        screen.Add("3#[[[[[[[[[[[[[[[[▼[[[[[[[[[[[[[[[[[[");
                        screen = convertForPC(screen,true); 
                    break;
                    case 2:
                        pokemons[0].sprite.SetActive(false);
                        pokemons[1].sprite.SetActive(false);
                        pokemons[2].sprite.SetActive(false);
                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$011111111111111111111111111111111110");
                        screen.Add("$01111YOUR1OPPONENT1IS1CHOOSING1111110");
                        screen.Add("$011111111111111111111111111111111110");
                        screen.Add("$011111111111111111111111111111111110");
                        screen.Add("$011111111111111111111111111111111110");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen = convertForPC(screen);
                        gamePhase = 3;
                    break;
                }
                PC.menu(screen);
            }

            if(avanzamento <= 30 && gamePhase == 3) {
                timer += Time.deltaTime*(avanzamento);
                if(timer >= 5) {
                    screen = new List<string>(); 
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$000000000000000000000000000000000000");
                    screen.Add("$011111111111111111111111111111111110");
                    screen.Add("$01111YOUR1OPPONENT1IS1CHOOSING1111110");
                    screen.Add("$011111111111111111111111111111111110");
                    screen.Add("$011"+loading(avanzamento)+"110");
                    screen.Add("$011111111111111111111111111111111110");
                    screen.Add("$000000000000000000000000000000000000");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen.Add("$ ");
                    screen = convertForPC(screen);
                    avanzamento++;
                    PC.menu(screen);
                    timer = 0;
                } 
            } else 
            if(avanzamento >= 30 && gamePhase == 3) { 
                gamePhase = 4; 
                draw_update = true;
            }

            if((PC.enter != -1 || draw_update) &&  gamePhase > 3) {
                menus = PC.enter;
                draw_update = false;
                switch(gamePhase) {
                    case 4:
                        if(menus == 4) {
                            draw_update=true;
                            pokemons[3].sprite.SetActive(false);
                            gamePhase = 5;
                            act_hp = pokemons[selected_pokemon].hp;
                            act_st = pokemons[selected_pokemon].stamina;
                            act_dfs = pokemons[selected_pokemon].dfs;
                            break;
                        }

                        screen = new List<string>(); 
                        pokemons[3].sprite.SetActive(true);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ "); 
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$111111111111111111111111111111111111");
                        screen.Add("$11111YOUR1OPPONENT1HAS1CHOOSEN111111");
                        screen = convertForPC(screen);
                        screen.Add("$                                                                                ừ̴̢̧̟̺͎̰̰͕̝̭̲̣̻̼̩͐̈̿͐̋̈͆̈́͘̕͠ͅn̸̡̛̗̹͖̺̥͇̻̤͕̠̠̾̿͛̀̍̎̂̆́̑̿́̾̍͑̆̈̿̉̐̕͘͘͜͝å̷̡̡̨͙̼͚̮̫̱͖̪͔͙̘̗̦͇̳̳̖͔̜̱͍̗̣͕̀͒͂̏͂̃͝͠ͅm̶̻̮̥͖͙̬̞̣̻̱̞̗̆͋̇͛͌̐͂̃̂͆̏̔̐̕ͅe̸̢͙̥͇̻̼̗̟̅̇̍͐̅̽͒̈́̍̎̏̕ͅḓ̸̢̝̻͉̟̜̠̈́̒̆̽͘");
                        screen.Add("$111111111111111111111111111111111111");
                        screen.Add("$000000000000000000000000000000000000");
                        screen = convertForPC(screen);
                        screen.Add("$ ");
                        screen.Add("4#[[[[[[[[[[[[[[CONTINUE[[[[[[[[[[[[[[");
                        screen = convertForPC(screen,true); 
                    break;
                    case 5:
                        //fight
                        switch(menus) {
                            case 6: message = rdnMessage(performeAttack(mosse[pokemons[selected_pokemon].mosse[0]],true)); gamePhase=6; draw_update=true; break;
                            case 7: message = rdnMessage(performeAttack(mosse[pokemons[selected_pokemon].mosse[1]],true)); gamePhase=6; draw_update=true; break;
                            case 8: message = rdnMessage(performeAttack(mosse[pokemons[selected_pokemon].mosse[2]],true)); gamePhase=6; draw_update=true; break;
                            case 9: message = rdnMessage(performeAttack(mosse[pokemons[selected_pokemon].mosse[3]],true)); gamePhase=6; draw_update=true; break;
                        }

                        //grafica
                        pokemons[3].sprite_2.SetActive(true);
                        pokemons[selected_pokemon].sprite_2.SetActive(true);
                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$                                                                                    HP  "+E_act_hp+"/"+pokemons[3].hp);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$ ");
                        screen.Add("6# "+mosse[pokemons[selected_pokemon].mosse[0]].name);
                        screen.Add("$                                                               HP  "+act_hp+"/"+pokemons[selected_pokemon].hp);
                        screen.Add("7# "+mosse[pokemons[selected_pokemon].mosse[1]].name);
                        screen.Add("$                                                               STAMINA  "+act_st+"/"+pokemons[selected_pokemon].stamina);
                        screen.Add("8# "+mosse[pokemons[selected_pokemon].mosse[2]].name);
                        screen.Add("$                                                               DEFENCE "+act_dfs+"/"+pokemons[selected_pokemon].dfs);
                        screen.Add("9# "+mosse[pokemons[selected_pokemon].mosse[3]].name);
                        screen = convertForPC(screen,true);
                    break;
                    case 6:
                        if(menus == 5) {
                            draw_update=true;
                            if(act_hp <= 0) gamePhase=9;
                            else if(E_act_hp <= 0) gamePhase = 8; 
                            else gamePhase=7;
                            break;
                        }

                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$                                                                                    HP  "+(E_act_hp <= 0 ? 0 : E_act_hp)+"/"+pokemons[3].hp);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$ ");
                        screen.Add("$ "+message);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("5#[[[[[[[[[[[[[[CONTINUE[[[[[[[[[[[[[");
                        screen.Add("$ ");
                        screen = convertForPC(screen,true);
                    break;
                    case 7:
                        if(menus == 5) {
                            draw_update=true;
                            if(act_hp <= 0) gamePhase=9;
                            else if(E_act_hp <= 0) gamePhase = 8; 
                            else gamePhase=5;
                            break;
                        }

                        //AI
                        string mossa_scelta = "";
                        if(((int)(E_act_hp/pokemons[3].hp)*100) < 25) {
                            int n = UnityEngine.Random.Range(0,4);
                            if(n<2) {
                                //cura
                                List<mossa> lm = new List<mossa>(); 
                                foreach(mossa m in mosse) if(m.hpRevocer != 0 || m.dfs != 0) lm.Add(m); 
                                mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                                mossa_scelta = ms.name;
                                performeAttack(ms,false);
                            } else {
                                //attacca
                                List<mossa> lm = new List<mossa>(); 
                                foreach(mossa m in mosse) if(m.dmg != 0 && E_act_st >= m.stamina) lm.Add(m); 
                                mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                                mossa_scelta = ms.name;
                                performeAttack(ms,false);
                            }
                        } else if(((int)(E_act_st/pokemons[3].stamina)*100) < 25) { 
                            //cura stamina
                            List<mossa> lm = new List<mossa>(); 
                            foreach(mossa m in mosse) if(m.staminaRecover != 0) lm.Add(m); 
                            mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                            mossa_scelta = ms.name;
                            performeAttack(ms,false);
                        }else {
                            if(((int)(act_hp/pokemons[selected_pokemon].hp)*100) < 30) {
                                //attacca
                                List<mossa> lm = new List<mossa>(); 
                                foreach(mossa m in mosse) if(m.dmg != 0 && E_act_st >= m.stamina) lm.Add(m); 
                                mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                                mossa_scelta = ms.name;
                                performeAttack(ms,false);
                            } else if(was_cured) {
                                //attacca O difesa
                                int n = UnityEngine.Random.Range(0,4);
                                if(n<2) {
                                    //difesa
                                    List<mossa> lm = new List<mossa>(); 
                                    foreach(mossa m in mosse) if(m.dfs != 0) lm.Add(m); 
                                    mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                                    mossa_scelta = ms.name;
                                    performeAttack(ms,false);
                                } else {
                                    //attacca
                                    List<mossa> lm = new List<mossa>(); 
                                    foreach(mossa m in mosse) if(m.dmg != 0 && E_act_st >= m.stamina) lm.Add(m); 
                                    mossa ms = lm[UnityEngine.Random.Range(0,lm.Count)];
                                    mossa_scelta = ms.name;
                                    performeAttack(ms,false);
                                }
                            }
                        }

                        //grafica
                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$                                                                                    HP  "+E_act_hp+"/"+pokemons[3].hp);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$ ");
                        screen.Add("$                                                                   "+pokemons[3].name+"                                                                     USES");
                        screen.Add("$ ");
                        screen.Add("$ "+mossa_scelta);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("5#[[[[[[[[[[[[[[CONTINUE[[[[[[[[[[[[[");
                        screen.Add("$ ");
                        screen = convertForPC(screen,true);
                    break;
                    case 8:
                        //grafica
                        pokemons[0].sprite_2.SetActive(false);
                        pokemons[1].sprite_2.SetActive(false);
                        pokemons[2].sprite_2.SetActive(false);
                        pokemons[3].sprite_2.SetActive(false);
                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$010001011110100100001000101111010010");
                        screen.Add("$010001010010100100001000101001011010");
                        screen.Add("$010001010010100100001000101001010110");
                        screen.Add("$011111010010100100001000101001010010");
                        screen.Add("$000100010010100100001010101001010010");
                        screen.Add("$000100010010100100001101101001010010");
                        screen.Add("$000100011110111100001000101111010010");
                        screen.Add("$000000000000000000000000000000000000");
                        screen = convertForPC(screen);
                        PC.kickOutOfPC();
                        animator.Play("unlockFloppy");
                        is_ended = true;
                    break;
                    case 9:
                        if(menus == 5) {
                            restart();
                            break;
                        }

                        //grafica
                        screen = new List<string>(); 
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$                                                                                    HP  "+E_act_hp+"/"+pokemons[3].hp);
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$000000000000000000000000000000000000");
                        screen.Add("$ ");
                        screen.Add("$ YOU LOST!");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("$ ");
                        screen.Add("5#[[[[[[[[[[[[[[CONTINUE[[[[[[[[[[[[[");
                        screen.Add("$ ");
                        screen = convertForPC(screen,true);
                    break;
                }
                PC.menu(screen);
            }
        } else if(!updateFloppy.activeSelf && is_ended) {
            screen = new List<string>(); 
            screen.Add("$NO FLOPPY DETECTED");
            PC.menu(screen);
        }
    }

    string rdnMessage(int code) {
        int n = UnityEngine.Random.Range(0,4);
        if(code == 2) {
            switch(n){
                case 0:return "The attack was a success!";break;
                case 1:return "That must hurt!";break;
                case 2:return "                                               "+pokemons[3].name+"                                                             is really pissed off";break;
                case 3:return "This will make a nice scar on it's face";break;
                case 4:return "Nice one!";break;
            }
        } else if(code == 0) {
            return pokemons[selected_pokemon].name+" is too tired to do that!";
        } else if(code == 3) {
            switch(n){
                case 0:return "A moment of releaf! "+pokemons[selected_pokemon].name+" recovers stamina";break;
                case 1:return "Finally resting a little";break;
                case 2:return "                                               "+pokemons[3].name+"                                                             is waiting...";break;
                case 3:return "Uff, it is tough";break;
                case 4:return "Resting a little couldn't hurt?";break;
            }
        } else if(code == 6) {
            return pokemons[selected_pokemon].name+" has defeated the opponent.";
        } else if(code == 5) {
            return "YOU HAVE BEEN DEFEATED!";
        }
        return "";
    }

     int performeAttack(mossa move,bool is_player) {
        int messageCode = 2;
        //player sta attaccando
        if(is_player) {
            int pk_max_stamina = pokemons[selected_pokemon].stamina;
            int pk_max_hp = pokemons[selected_pokemon].hp;

            //calcolo recolver stamina passiva
            if(act_st < pk_max_stamina) { act_st += 5; }

            //calcolo recolver stamina per mossa fatta, se prevista
            if(act_st < pk_max_stamina) { act_st += move.staminaRecover; messageCode = 3; }

            act_st = act_st < pk_max_stamina ? act_st : pk_max_stamina;

            if(act_st > 0 && act_st >= move.stamina) {
                //calcolo danno inflitto
                int dmg = (int)((move.dmg * E_act_dfs) * 0.01f);
                if(dmg != 0) { messageCode = 4; }
                E_act_hp -= dmg;
                
                //calcolo cura, se prevista dalla mossa
                if(act_hp < pk_max_hp) { 
                    act_hp += move.hpRevocer; 
                    act_hp = act_hp < pk_max_hp ? act_hp : pk_max_hp;
                }
                
                if(dmg == 0 && move.hpRevocer != 0) { was_cured = true; } 
                else { was_cured = false; }

                //calcolo perdita stamina
                if(act_st > 0) {
                    act_st -= move.stamina; 
                    act_st = act_st <= 0 ? 0 : act_st ;
                }
                
                //calcolo modificatore difesa
                if(move.dfs != 0) { act_dfs += move.dfs; }
                else { act_dfs = pokemons[selected_pokemon].dfs; }
            } else {
                return messageCode = 0;
            }
        } else {
            int pk_max_stamina = pokemons[3].stamina;
            int pk_max_hp = pokemons[3].hp;

            //nemico attacca
            if(E_act_st < pk_max_stamina) { E_act_st += 5; }

            //calcolo recolver stamina passiva
            if(E_act_st < pk_max_stamina) { E_act_st += move.staminaRecover; }

            E_act_st = E_act_st < pk_max_stamina ? E_act_st : pk_max_stamina;

            if(E_act_st > 0 && E_act_st >= move.stamina) {
                act_hp -= (int)((move.dmg * act_dfs) * 0.01f);

                if(E_act_hp < pk_max_hp) { 
                    E_act_hp += move.hpRevocer; 
                    E_act_hp = E_act_st < pk_max_hp ? E_act_hp : pk_max_hp;
                }
                
                if(E_act_st > 0) {
                    E_act_st -= move.stamina; 
                    E_act_st = E_act_st <= 0 ? 0 : E_act_st ;
                }

                if(move.dfs != 0) { E_act_dfs += move.dfs; }
                else { E_act_dfs = pokemons[selected_pokemon].dfs; }
            } else {
                return messageCode = 1;
            }
        }

        if(act_hp <= 0) return messageCode = 5;
        if(E_act_hp <= 0) return messageCode = 6;
        return messageCode = 2;
    }

    //OLD attackPerformer
    int OLD_performeAttack(mossa move,bool is_player) {
        int messageCode = 2;
        //player sta attaccando
        if(is_player) {
            //calcolo recolver stamina passiva
            if(act_st < pokemons[selected_pokemon].stamina) act_st += 5;
            //calcolo recolver stamina per mossa fatta, se prevista
            if(act_st < pokemons[selected_pokemon].stamina) {act_st += move.staminaRecover; messageCode = 3;}
            if(act_st > 0 && act_st >= move.stamina) {
                //calcolo danno inflitto
                int dmg = (int)((move.dmg * E_act_dfs) * 0.01f);
                if(dmg != 0) messageCode = 4;
                E_act_hp -= dmg;
                //calcolo cura, se prevista dalla mossa
                if(act_hp < pokemons[selected_pokemon].hp) act_hp += move.hpRevocer;
                if(dmg == 0 && move.hpRevocer != 0) {was_cured = true;} else {was_cured = false;}
                //calcolo perdita stamina
                if(act_st > 0) {act_st -= move.stamina; act_st = act_st <= 0 ? 0 : act_st ;}
                //calcolo modificatore difesa
                if(move.dfs != 0) act_dfs += move.dfs;
                else act_dfs = pokemons[selected_pokemon].dfs;
            } else {
                return messageCode = 0;
            }
        } else {
            //nemico attacca
            if(E_act_st < pokemons[3].stamina) E_act_st += 5;
            if(E_act_st < pokemons[3].stamina) E_act_st += move.staminaRecover;
            if(E_act_st > 0 && E_act_st >= move.stamina) {
                act_hp -= (int)((move.dmg * act_dfs) * 0.01f);
                if(E_act_hp < pokemons[3].hp) E_act_hp += move.hpRevocer;
                if(E_act_st > 0) {E_act_st -= move.stamina; E_act_st = E_act_st <= 0 ? 0 : E_act_st ;}
                if(move.dfs != 0) E_act_dfs += move.dfs;
                else E_act_dfs = pokemons[selected_pokemon].dfs;
            } else {
                return messageCode = 1;
            }
        }

        if(act_hp <= 0) return messageCode = 5;
        if(E_act_hp <= 0) return messageCode = 6;
        return messageCode = 2;
    }

    string loading(int avanzamento) {
        string val="";
        for(int i=0;i<30;i++) { 
            if(i<avanzamento) { val+="_"; } 
            else val+="1";
        }
        return val;
    }

    List<string> convertForPC(List<string> screen,bool is_choise = false) {
        for(int i = 0; i<screen.Count;i++) {
            if(!is_choise) screen[i] = screen[i].Replace("1","<color=#000000>0</color>"); 
            else screen[i] = screen[i].Replace("[","<color=#000000>0</color>");            
        }
        return screen;
    }

    List<string> printPokemonMoves(int indexPokemon,List<string> screen) {
        screen.Add("$111111111111111111"+mosse[pokemons[indexPokemon].mosse[0]].name+"111");
        screen.Add("$1 11----MOVES----111"+mosse[pokemons[indexPokemon].mosse[1]].name+"111");
        screen.Add("$111111111111111111"+mosse[pokemons[indexPokemon].mosse[2]].name+"111");
        screen.Add("$111111111111111111"+mosse[pokemons[indexPokemon].mosse[3]].name+"111");
        return screen;
    }

    List<string> printPokemon(int indexPokemon,List<string> screen) {
        pokemons[0].sprite.SetActive(false);
        pokemons[1].sprite.SetActive(false);
        pokemons[2].sprite.SetActive(false);
        pokemons[indexPokemon].sprite.SetActive(true);
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ ");
        screen.Add("$ "); 
        switch(indexPokemon) {
            case 0: screen.Add("$0000000----Jokulsarlon  Glacier----0000000"); break;
            case 1: screen.Add("$000000000000----Pinnacle----00000000000"); break;
            case 2: screen.Add("$0000000000----Pleneau Bay----0000000000"); break;
        }
        return screen;
    }
}

public class mossa {
    //nome mossa
    public string name;
    //stamina usata per fare la mossa
    public int stamina;
    //danni fatti con la mossa
    public int dmg;
    //valore ricarica stamina
    public int staminaRecover;
    //valore di aumento difesa
    public int dfs;
    //valore di recupero HP
    public int hpRevocer;

    public mossa(string name,int stamina,int dmg,int staminaRecover,int dfs,int hpRevocer) {
        this.name = name;
        this.stamina = stamina;
        this.dmg = dmg;
        this.staminaRecover = staminaRecover;
        this.dfs = dfs;
        this.hpRevocer = hpRevocer;
    }
}

public class pokemon {
    public GameObject sprite;
    public GameObject sprite_2;
    //nome
    public string name;
    //ma stamina
    public int stamina;
    //max difesa
    public int dfs;
    //HP
    public int hp;
    public int[] mosse;

    public pokemon(GameObject sprite,GameObject sprite_2, string name,int stamina,int dfs,int hp,int[] mosse) {
        this.name = name;
        this.stamina = stamina;
        this.dfs = dfs;
        this.hp = hp;
        this.mosse = mosse;
        this.sprite = sprite;
        this.sprite_2 = sprite_2;
    }
}
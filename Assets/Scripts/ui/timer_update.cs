using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer_update : MonoBehaviour
{
    public bool debug = false;
    [Space]
    public quest_callHelp quest_help;
    public TextMeshProUGUI txt_timer;
    public GameObject no_data;

    float time = 0;
    void Start() { 
        no_data.SetActive(true); 
        txt_timer.gameObject.SetActive(false);
        
        time = quest_help.timeBeforeEnd * 60;
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);
        string act_time = string.Format("{00:00}{1:00}", min, sec);

        txt_timer.text = act_time[0]+act_time[1]+":"+act_time[2]+act_time[3];
        
    }

    void Update() {
        if(quest_help.startedTimer) {
            no_data.SetActive(false); 
            txt_timer.gameObject.SetActive(true);
            
            if (time > 0.0f) { 
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                string act_time = string.Format("{00:00}{1:00}", min, sec);
                
                string m1 = act_time[0].ToString();
                string m2 = act_time[1].ToString();
                string s1 = act_time[2].ToString();
                string s2 = act_time[3].ToString();
                
                txt_timer.text = m1+m2+":"+s1+s2;
                
            }

            time -= Time.deltaTime;
        }
    }

}

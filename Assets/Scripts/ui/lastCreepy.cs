using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class lastCreepy : MonoBehaviour
{
    public typewriterUI ip;
    public GameObject audioCreepy;

    public void typeScript() { 
        audioCreepy.SetActive(true);
        TextMeshProUGUI txt = ip.GetComponent<TextMeshProUGUI>();

        string n1 = format(Random.RandomRange(0,255).ToString());
        string n2 = format(Random.RandomRange(0,255).ToString());
        string n3 = format(Random.RandomRange(0,255).ToString());
        string n4 = format(Random.RandomRange(0,255).ToString());

        txt.text = "IPV4: "+n1+"."+n2+"."+n3+"."+n4;
        ip.gameObject.SetActive(true); 
    }

    string format(string n) {
        if (n.Length == 2) { return "0"+n; }
        if (n.Length == 1) { return "00"+n; }
        return n;
    }
}

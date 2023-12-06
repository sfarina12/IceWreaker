using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class clock : MonoBehaviour
{
    public TextMeshPro text;

    void LateUpdate()
    {

        int sysHour = System.DateTime.Now.Hour;
        int sysMin = System.DateTime.Now.Minute;
        int sysSec = System.DateTime.Now.Second;

        text.text = sysHour+":"+ sysMin + ":" + sysSec;
    }
}

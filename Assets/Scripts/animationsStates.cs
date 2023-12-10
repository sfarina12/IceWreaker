using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsStates : MonoBehaviour
{
    [HideInInspector]
    public bool isEnded = false;
    public void finished() {
        isEnded = true;
    }
}

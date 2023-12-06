using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitGame : MonoBehaviour
{
    [Tooltip("if set to true it will close the game as soon as the script will be active")]
    public bool isAutomatic = true;

    void Update()
    {
        if(isAutomatic)
            if (transform.GetComponent<buttonHandler>().clicked)
                Application.Quit();
    }

    public void closeGame() { Application.Quit(); }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class printMessage : MonoBehaviour
{
    [Tooltip("The animator needed to show the message in the player's UI.")]
    public Animator messageAnimator;
    [Tooltip("The text area needed to print the message on.")]
    public TextMeshProUGUI message;

    private void Start()
    {
        if (messageAnimator == null) Debug.LogError("No <Animator> Component found for <printMessage> Component. This will cause the inability to show the message in player's UI.");
        if (message == null) Debug.LogError("No <TextMeshProUGUI> Component found for <printMessage> Component. This will cause the inability to print the message in player's UI.");
    }

    //this function will be called by 3rd party functions that need to print all kind of messages.
    public void showMessage(string text)
    {
        messageAnimator.Play("displayMsg");
        message.text = text;
    }
}

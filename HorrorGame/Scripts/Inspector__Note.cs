using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector__Note : MonoBehaviour
{
    [TextArea(1, 100), Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\n" +
                  "There are two kinds of audio:\n" +
                  "1 - the one played by the <audioPlayer> Component\n" +
                  "2 - the one played by the animator\n" +
                  "Some objects, like the door doesn't have a <audioPlayer> Component instead plays the audio via it's animator.\n" +
                  "And how it works? For every audio you want to play with the animator, you need to make a Animation that has the same name, at last inside of the Animator, of the Animation that will play for the object.\n" +
                  "And how long this animation should be? At last long as the object animation, or something similar, just as long as much it need to not start a sound loop.\n" +
                  "The AudioSource associated with this audio animation need to be set with Play On Awake, and deactivate the gameobject with the AudioSource when it's done.\n" +
                  "Example: See the door animator. For every question ask sfarina12.";
}

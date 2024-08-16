using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioIgnoreOcclusion : MonoBehaviour
{
    [Tooltip("every object in this list will be ignored for the occlusion simulation of the audio. This component MUST be placed in the same gameobject of the <AudioSource> in order to work")]
    public LayerMask ignoreLayers;
}

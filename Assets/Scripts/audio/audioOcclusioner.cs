using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class audioOcclusioner : MonoBehaviour
{
    [Tooltip("If global, will automatically set the audio occlusion to every audio component in the scene. If is off it will search in the current object for <AudioSource> component. If not found nothing will happen.")]
    public bool isGlobal = false;
    [Space]
    public GameObject player;
    public LayerMask ignoreLayers;
    public float hightValue = 5000f;
    public float lowValue = 365f;
    [Space]
    public bool drawDebug= false;

    List<AudioLowPassFilter> filters;
    void Start()
    {
        filters = new List<AudioLowPassFilter>();
        if(!isGlobal) {
            if(gameObject.GetComponent<AudioLowPassFilter>() != null) {
                filters.Add(gameObject.GetComponent<AudioLowPassFilter>());
            }
        } else {
            filters = Resources.FindObjectsOfTypeAll<AudioLowPassFilter>().ToList<AudioLowPassFilter>();
        }
         
    }

    void Update()
    {
        RaycastHit hit;
        foreach(AudioLowPassFilter s in filters) {
            LayerMask ignoreLayers_s = s.transform.GetComponent<audioIgnoreOcclusion>().ignoreLayers;
            ignoreLayers_s += ignoreLayers;

            if(Physics.Linecast(s.transform.position,player.transform.position, out hit,~ignoreLayers_s)) {           
                if(drawDebug) drawX(hit);
            
                if(hit.transform.tag.Equals("Player") || hit.transform.Equals(s.transform)) {
                    s.cutoffFrequency = hightValue;
                } else {
                    s.cutoffFrequency = lowValue;
                }
            }
        }
        
    }

    void drawX(RaycastHit hit) {
        Debug.DrawLine(new Vector3(hit.point.x+.5f,hit.point.y+.5f,hit.point.z),new Vector3(hit.point.x-.5f,hit.point.y-.5f,hit.point.z),Color.yellow);
        Debug.DrawLine(new Vector3(hit.point.x-.5f,hit.point.y+.5f,hit.point.z),new Vector3(hit.point.x+.5f,hit.point.y-.5f,hit.point.z),Color.yellow);
    }
}

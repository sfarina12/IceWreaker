using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitching : MonoBehaviour
{
    public Interact interactObject;
    public List<Light> lights;
    public List<MeshRenderer> models;
    [Space]
    public Material light_on;
    public Material light_off;

    void Start() {
        if(light_on == null) Debug.LogError("empty 'on' material for this light switch");
        if(light_off == null) Debug.LogError("empty 'off' material array for this light switch");
    }

    void Update()
    {
        if (!interactObject.isOn) {
            foreach(Light light in lights) 
                light.transform.gameObject.SetActive(true);
            foreach(MeshRenderer model in models) 
               model.material = light_on;
        }
        else {
            foreach (Light light in lights)
                light.transform.gameObject.SetActive(false);
            foreach(MeshRenderer model in models) 
               model.material = light_off;
        }
    }
}

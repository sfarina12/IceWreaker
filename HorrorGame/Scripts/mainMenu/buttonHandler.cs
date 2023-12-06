using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public Renderer btnBody;
    public Camera customCamera;
    [Space]
    [Header("Materials")]
    public Material idleMaterial;
    public Material hilightedMaterial;
    public Material clickedMaterial;

    [HideInInspector]
    public bool clicked = false;
    [HideInInspector]
    public bool hovering = false;

    public void idle_btn() { btnBody.material = idleMaterial; }
    public void hilighted_btn() { btnBody.material = hilightedMaterial; }
    public void clicked_btn() { btnBody.material = clickedMaterial; }

    private void Update()
    {
        RaycastHit hit;
        Ray ray;
        
        if(customCamera==null)
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        else
            ray = customCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform != null)
            {
                if (hit.transform == transform)
                {
                    transform.gameObject.GetComponent<buttonHandler>().hilighted_btn();
                    hovering = true;

                    if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                    {
                        transform.gameObject.GetComponent<buttonHandler>().clicked_btn();
                        clicked = true;
                    }
                    else
                        clicked = false;
                }
                else
                { transform.gameObject.GetComponent<buttonHandler>().idle_btn(); hovering = false; }
            }
            else
                hovering = false;
        }
    }

}

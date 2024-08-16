using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class camerainterface_updateList : MonoBehaviour
{
    bool open_choise = false;
    int selected = 0;
    bool is_mouseR_pressed = false;

    public List<Image> updateIcons;
    public List<GameObject> selection_updateIcons;
    public Animator animator;
    public GameObject activeUpdate;
    [Space]
    public GameObject contentUpdate;

    void Start() { }

    //mouse di destra per aprire il menu degli update
    void Update() {
        is_mouseR_pressed = Input.GetMouseButtonDown(1);

        //scegliere l'update da vedere
        if(open_choise) {
            int old_selected = selected;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f ) {
               selected++;
               selected = selected >= updateIcons.Count ? 0 : selected;
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) {
               selected--;
               selected = selected < 0 ? updateIcons.Count-1 : selected;
            }

            if(old_selected != selected) { selection_updateIcons[old_selected].SetActive(false); }
            selection_updateIcons[selected].SetActive(true);
            
            if(is_mouseR_pressed) {
                foreach(Transform t in activeUpdate.transform) {t.gameObject.SetActive(false);}
                activeUpdate.transform.GetChild(selected).gameObject.SetActive(true);
                //disattivo e attivo l'update content giusto
                foreach(Transform t in contentUpdate.transform) {t.gameObject.SetActive(false);}
                if(selected <= contentUpdate.transform.childCount) { contentUpdate.transform.GetChild(selected).gameObject.SetActive(true); }
            }
        }        

        activeUpdate.SetActive(!open_choise);

        //aprire o chiudere la lista di update da vedere
        if(is_mouseR_pressed) {
            if(!open_choise) {
                if(selected == 0) animator.Play("open_noUnpdates");
                else animator.Play("close_updateList_effect");

                foreach(Image img in updateIcons) {img.transform.gameObject.SetActive(true);}
                
            } else {
                if(selected == 0) animator.Play("close_noUpdates");
                else animator.Play("close_updateList");

                foreach(Image img in updateIcons) {img.transform.gameObject.SetActive(false);}
            };
            open_choise = !open_choise;
        }
    }
}

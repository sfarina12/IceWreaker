using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using AdditionalTools;


public class screen_pointer : MonoBehaviour
{
    [Tooltip("[can be null] Pointer enlarger when getting close to the interactable object")]
    public Image pointer;
    public playerInteractHandler interacter;
    public Camera playerCamera;
    [Min(1),Tooltip("The max distance to check the pointer size, To avoid checkin the whole map")]
    public float maxArea = 3f;
    public float max_pointer_size = 10;
    [Space]
    [Tooltip("[can be null] when pointing directoly to the interactable object will fire the animation named <name_pointerAnimationOn> else <name_pointerAnimationOff>")]
    public Animator pointerAnimator;
    [Tooltip("[can be null] Name of the animation played when pointing directly at object")]
    public string namePointerAnimOn;
    [Tooltip("[can be null] Name of the animation played when remove pointing from object")]
    public string namePointerAnimOff;
    
    bool canPointerAnimation = false;
    Interact[] A;
    pickUp_book[] B;
    quest_pickUpItem[] C;
    quest_pickUpUpdate[] D;
    quest_dPad E;
    quest_unlockHammer[] F;
    quest_useHammer G;

    //to add another kind of selectable items (i highly suggest to use <Interact> and not to recreate form new)
    //just add <type> to the list and a foreach
    
    void Start() {
        A = FindObjectsOfType<Interact>();
        B = FindObjectsOfType<pickUp_book>();
        C = FindObjectsOfType<quest_pickUpItem>();
        D = FindObjectsOfType<quest_pickUpUpdate>();
        E = FindObjectOfType<quest_dPad>();
        F = FindObjectsOfType<quest_unlockHammer>();
        G = FindObjectOfType<quest_useHammer>();

        foreach(Interact inter in A) {
            if(inter.interactableFlag && inter.enabled && inter.gameObject.activeSelf) {
                 if (pointer != null) { 
                    if(inter.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                        inter.canPointer = true;  
                        inter.pointerCollider = inter.outlineableObject.gameObject.GetComponent<Collider>(); 
                    } else { 
                        Debug.LogError("Couldn't find the pointer collider for object " + inter.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                        inter.canPointer = false; 
                    }
                }

                if (inter.canPointer) {
                    if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
                }
            }
        }

        foreach(pickUp_book book in B) {
            if(book.enabled && book.gameObject.activeSelf) {
                 if (pointer != null) { 
                    if(book.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                        book.canPointer = true;  
                        book.pointerCollider = book.outlineableObject.gameObject.GetComponent<Collider>(); 
                    } else { 
                        Debug.LogError("Couldn't find the pointer collider for object " + book.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                        book.canPointer = false; 
                    }
                }

                if (book.canPointer) {
                    if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
                }
            }
        }

        foreach(quest_pickUpItem item in C) {
            if(item.enabled && item.gameObject.activeSelf) {
                 if (pointer != null) { 
                    if(item.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                        item.canPointer = true;  
                        item.pointerCollider = item.outlineableObject.gameObject.GetComponent<Collider>(); 
                    } else { 
                        Debug.LogError("Couldn't find the pointer collider for object " + item.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                        item.canPointer = false; 
                    }
                }

                if (item.canPointer) {
                    if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
                }
            }
        }

        foreach(quest_pickUpUpdate item in D) {
            if(item.enabled && item.gameObject.activeSelf) {
                 if (pointer != null) { 
                    if(item.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                        item.canPointer = true;  
                        item.pointerCollider = item.outlineableObject.gameObject.GetComponent<Collider>(); 
                    } else { 
                        Debug.LogError("Couldn't find the pointer collider for object " + item.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                        item.canPointer = false; 
                    }
                }

                if (item.canPointer) {
                    if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
                }
            }
        }

        if(E.enabled && E.gameObject.activeSelf) {
             if (pointer != null) { 
                if(E.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                    E.canPointer = true;  
                    E.pointerCollider = E.outlineableObject.gameObject.GetComponent<Collider>(); 
                } else { 
                    Debug.LogError("Couldn't find the pointer collider for object " + E.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                    E.canPointer = false; 
                }
            }

            if (E.canPointer) {
                if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
            }
        }

        foreach(quest_unlockHammer item in F) {
            if(item.enabled && item.gameObject.activeSelf) {
                 if (pointer != null) { 
                    if(item.outlineableObject.gameObject.GetComponent<Collider>() != null) {
                        item.canPointer = true;  
                        item.pointerCollider = item.outlineableObject.gameObject.GetComponent<Collider>(); 
                    } else { 
                        Debug.LogError("Couldn't find the pointer collider for object " + item.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                        item.canPointer = false; 
                    }
                }

                if (item.canPointer) {
                    if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
                }
            }
        }

        if(G.enabled && G.gameObject.activeSelf) {
             if (pointer != null) { 
                if(G.pipeOutline.gameObject.GetComponent<Collider>() != null) {
                    G.canPointer = true;  
                    G.pointerCollider = G.pipeOutline.gameObject.GetComponent<Collider>(); 
                } else { 
                    Debug.LogError("Couldn't find the pointer collider for object " + G.transform.name + ". Please intert it manually or else the pointer behavior will be ignored.");
                    G.canPointer = false; 
                }
            }

            if (G.canPointer) {
                if(pointerAnimator != null && namePointerAnimOn != "" && namePointerAnimOff != "") { canPointerAnimation = true; }
            }
        }
    }

    void Update() {
        Vector3 camera_pos = playerCamera.transform.position;
        float max = -1;
        bool is_hightlited = false;

        //----------------[ADD HERE THE NEW FOREACH FOR CHECKING THE INTERACTION POINTER]----------------

        //A
        foreach(Interact inter in A) {
            if(is_hightlited) {continue;}            

            if((inter.interactableFlag || inter.showPointer) && inter.enabled && inter.gameObject.activeSelf) {
                if(inter.canPointer) {
                    float distance = Vector3.Distance(inter.transform.position,playerCamera.transform.position);
                    if(distance <= maxArea) { 
                        if(inter.outlineableObject.enabled) { is_hightlited = true; }
                        float result = calcualte_pointer(camera_pos,inter.pointerCollider,inter.pointerDistance); 
                        if(result > max) { max = result; }
                    }
                }
            }
        }

        //B
        if(!is_hightlited) {
            foreach(pickUp_book inter in B) {
                if(is_hightlited) {continue;}

                if(inter.enabled && inter.gameObject.activeSelf) {
                    if(inter.canPointer) {
                        float distance = Vector3.Distance(inter.transform.position,playerCamera.transform.position);
                        if(distance <= maxArea) { 
                            if(inter.outlineableObject.enabled) { is_hightlited = true; }
                            float result = calcualte_pointer(camera_pos,inter.pointerCollider,inter.pointerDistance); 
                            if(result > max) { max = result; }
                        }
                    }
                }
            }
        }

        //C
        if(!is_hightlited) {
            foreach(quest_pickUpItem inter in C) {
                if(is_hightlited || inter == null || inter.outlineableObject == null) {continue;}
                
                if(inter.enabled && inter.gameObject.activeSelf) {
                    if(inter.canPointer) {
                        float distance = Vector3.Distance(inter.transform.position,playerCamera.transform.position);
                        if(distance <= maxArea) {
                            if(inter.outlineableObject.enabled) { is_hightlited = true; }
                            float result = calcualte_pointer(camera_pos,inter.pointerCollider,inter.pointerDistance); 
                            
                            if(result > max) { max = result; }
                        }
                    }
                }
            }
        }

        //D
        if(!is_hightlited) {
            foreach(quest_pickUpUpdate inter in D) {
                if(is_hightlited) {continue;}

                if(inter.enabled && inter.gameObject.activeSelf) {
                    if(inter.canPointer) {
                        float distance = Vector3.Distance(inter.transform.position,playerCamera.transform.position);
                        if(distance <= maxArea) { 
                            if(inter.outlineableObject.enabled) { is_hightlited = true; }
                            float result = calcualte_pointer(camera_pos,inter.pointerCollider,inter.pointerDistance); 
                            if(result > max) { max = result; }
                        }
                    }
                }
            }
        }

        //E
        if(!is_hightlited && E.outlineableObject != null) {
            if(E.enabled && E.gameObject.activeSelf && !E.questEnded) {
                if(E.canPointer) {
                    float distance = Vector3.Distance(E.transform.position,playerCamera.transform.position);
                    if(distance <= maxArea) { 
                        if(E.outlineableObject.enabled) { is_hightlited = true; }
                        float result = calcualte_pointer(camera_pos,E.pointerCollider,E.pointerDistance); 
                        if(result > max) { max = result; }
                    }
                }
            }
        }

        //F
        if(!is_hightlited) {
            foreach(quest_unlockHammer inter in F) {
                if(is_hightlited) {continue;}

                if(inter.enabled && inter.gameObject.activeSelf) {
                    if(inter.canPointer) {
                        float distance = Vector3.Distance(inter.transform.position,playerCamera.transform.position);
                        if(distance <= maxArea) { 
                            if(inter.outlineableObject.enabled) { is_hightlited = true; }
                            float result = calcualte_pointer(camera_pos,inter.pointerCollider,inter.pointerDistance); 
                            if(result > max) { max = result; }
                        }
                    }
                }
            }
        }

        //G
        if(!is_hightlited) {
            if(G.enabled && G.gameObject.activeSelf && !G.questEnded) {
                if(G.canPointer) {
                    float distance = Vector3.Distance(G.transform.position,playerCamera.transform.position);
                    if(distance <= maxArea) { 
                        if(G.pipeOutline.enabled) { is_hightlited = true; }
                        float result = calcualte_pointer(camera_pos,G.pointerCollider,G.pointerDistance); 
                        if(result > max) { max = result; }
                    }
                }
            }
        }

        //END ------------[ADD HERE THE NEW FOREACH FOR CHECKING THE INTERACTION POINTER]----------------

        //----------------[GESTIONE RESIZING POINTER]----------------
        if(is_hightlited) { pointer.rectTransform.sizeDelta = new Vector2(max_pointer_size,max_pointer_size); } 
        else { pointer.rectTransform.sizeDelta = new Vector2(max,max); }

        if(is_hightlited && canPointerAnimation) { pointerAnimator.Play(namePointerAnimOn); }
        if(!is_hightlited && canPointerAnimation) { pointerAnimator.Play(namePointerAnimOff); }
        //END -------------[GESTIONE RESIZING POINTER]----------------
    }

    float calcualte_pointer(Vector3 camera_pos,Collider collider,float pointerDistance) {
        Vector3 point_2 = collider.bounds.center;
        Vector3 screenPos = interacter.playerCamera.WorldToViewportPoint(point_2);

        float distance = Vector3.Distance(camera_pos,point_2);
        float maxDistance = pointerDistance == 0 ? interacter.maxDistance : pointerDistance;
        if(distance < maxDistance) {
            Vector2 obj_pos = new Vector2(screenPos.x - .5f,screenPos.y - .5f);
            Vector2 center_pos = new Vector2(0,0);

            distance = Vector2.Distance(center_pos,obj_pos);
            
            if(distance <= 1) { return (1-distance) * max_pointer_size; }
        }
        return 0;
    }
}

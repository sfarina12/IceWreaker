using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pickUp_book : MonoBehaviour
{
    [Tooltip("The outline object to interact with")]
    public Outline outlineableObject;
    [Space]
    public text_book bookContent;
    [Space]
    public GameObject closeBookMessage;
    public GameObject zoomBookMessage;
    public GameObject zoomBookContainer;
    [Space]
    public open_dPad dPad;

    [Space]
    public KeyCode keyToCloseBook;
    public KeyCode keyToZoomBook;
    [Space]
    public checkDPadUpdates updateLight;
    [Space]
    public audioPlayer audioOpen_book;
    public audioPlayer audioClose_book;

    [Space]
    public GameObject dPadLight;


    GameObject zoomBookContainer_noLightUpdate;
    TextMeshPro bookText;
    TextMeshProUGUI zoomBookText;
    Animator bookAnimator;
    bool open = false;
    bool isZommed = false;
    playerInteractHandler interacter;
    

    private void Start()
    {
        if (!outlineableObject.tag.Equals("interactable")) Debug.LogError("The object with outline is not interactable");

        bookAnimator = GetComponent<Animator>();
        interacter = GameObject.Find("player").GetComponent<playerInteractHandler>();

        bookText = GetComponentInChildren<TextMeshPro>();
        zoomBookText= GameObject.Find("PSX renderer/Canvas/zoomedBook_Text").GetComponentInChildren<TextMeshProUGUI>();
        zoomBookContainer_noLightUpdate= GameObject.Find("PSX renderer/Canvas/zoomedBook_Text/zoomedBook_noLightUpade");

        closeBookMessage.SetActive(false);
        zoomBookMessage.SetActive(false);
        zoomBookContainer_noLightUpdate.SetActive(true);

        //dPadLight = GameObject.Find("d-padLight");
    }

    private void Update()
    {
        if (interacter.isInteracting && outlineableObject.enabled && !open)
        {
            interacter.showInteractText = false;
            open = true;

            bookText.text = bookContent.text;
            zoomBookText.text = bookContent.text;
            closeBookMessage.SetActive(true);
            zoomBookMessage.SetActive(true);

            openBook(gameObject);
        }

        if (open)
        {
            if (Input.GetKeyDown(keyToCloseBook))
            { 
                open = false; 
                isZommed=false;

                closeBookMessage.SetActive(false);
                zoomBookContainer.SetActive(false);
                zoomBookMessage.SetActive(false);

                openBook(gameObject); 

                interacter.showInteractText = true;
            }

            if(Input.GetKeyDown(keyToZoomBook))
            {
                isZommed=!isZommed;
                zoomBookContainer.SetActive(isZommed);
            }
        }
    }

    public void openBook(GameObject book)
    {
        changeBookCover(book);
        performeAnimation(open);
    }

    private void performeAnimation(bool isOpen)
    {
        if (isOpen)
            audioOpen_book.playAudio();
        else
            audioClose_book.playAudio();

        bookAnimator.Play(isOpen?"openBook":"closeBook");
        if(updateLight.isUpdate && dPad.enabled)
        {
            dPadLight.SetActive(!dPadLight.activeSelf);
            zoomBookContainer_noLightUpdate.SetActive(false);
        }
    }

    public void changeBookCover(GameObject book)
    {
        Material material = book.GetComponent<MeshRenderer>().materials[1];
        transform.GetComponent<MeshRenderer>().sharedMaterials[1] = material;
    }
}

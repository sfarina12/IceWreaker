using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadMap : MonoBehaviour
{
    [Header("Loading settings")]
    public int sceneToLoad;
    [Tooltip("optional Animation"),Space]
    public Animator animator;
    public Animator animatorAudio;
    [Space]
    public bool isNewGame=false;
    [Tooltip("tempo da attendere prima di poter AVVIARE il livello")]
    public float timeToWait;
    [Space,Tooltip("It will automatically load the level when script will be set enabled. Without button input")]
    public bool isAutomatic = false;
    [Space, Tooltip("If true the script will be run only by an external input for example a button input (UI)")]
    public bool isFunction = false;
    public bool enableLoading = true;

    private float act_time=0;
    private bool startTime = false;
    bool started_loading = true;

    private void Start()
    {
        if(enableLoading) {
            if (isAutomatic)
            {
                if (animator != null)
                {
                    animator.enabled = true;
                    if (!isNewGame)
                        animator.Play("startLoading");
                    else
                        animator.Play("newGame");

                    if (animatorAudio != null) animatorAudio.Play("playAudio");
                }

                startTime = true;
            }
        }
    }

    public void loadLevel()
    {
        if(enableLoading) {
            if (animator != null)
            {
                animator.enabled = true;
                if (!isNewGame)
                    animator.Play("startLoading");
                else
                    animator.Play("newGame");

                if (animatorAudio != null) animatorAudio.Play("playAudio");
            }

            startTime = true;
        }
    }

    void Update()
    {
        if(enableLoading) {
            if (!isAutomatic && !isFunction)
            {
                if (transform.GetComponent<buttonHandler>().clicked)
                {
                    if (animator != null)
                    {
                        animator.enabled = true;
                        if (!isNewGame)
                            animator.Play("startLoading");
                        else
                            animator.Play("newGame");

                        if (animatorAudio != null) animatorAudio.Play("playAudio");
                    }

                    startTime = true;
                }
            }

            if (startTime) { act_time += Time.deltaTime; }

            if (act_time >= timeToWait && started_loading)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                StartCoroutine(LoadAsyncronously(sceneToLoad));
                //SceneManager.UnloadSceneAsync(0);
                started_loading = false;
            }
        }
    }

    IEnumerator LoadAsyncronously(int levelIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelIndex);

        while (!op.isDone) { yield return null; }
    }
}

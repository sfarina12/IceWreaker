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

    private float act_time=0;
    private bool startTime = false;

    private void Start()
    {
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

    public void loadLevel()
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

    void Update()
    {
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

        if (startTime)
            act_time += Time.deltaTime;

        if (act_time >= timeToWait)
        {
            StartCoroutine(LoadAsyncronously(sceneToLoad));
            //SceneManager.UnloadSceneAsync(0);
        }
    }

    IEnumerator LoadAsyncronously(int levelIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(levelIndex);

        while (!op.isDone)
        {
            //Debug.Log(op.progress);

            yield return null;
        }


    }
}

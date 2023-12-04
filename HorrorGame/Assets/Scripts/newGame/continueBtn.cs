using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class continueBtn : MonoBehaviour
{
    public string animationName;
    public Animator animator;
    [Space,Tooltip("Can be null")]
    public TextMeshProUGUI objToDisappear;
    public void playAnimation() { animator.Play(animationName); if (objToDisappear != null) objToDisappear.enabled = false; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class changePathObstable : MonoBehaviour
{
    public NavMeshObstacle obstable;
    public Interact interact;

    void Update()
    {
        if (interact.isOn && !interact.animator.GetBool("isInteracting"))
            obstable.enabled = false;
        else
            obstable.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class killPerformer : MonoBehaviour
{
    public NavMeshAgent agent;
    public m_patrolling patrolling;
    public GameObject player;
    public SC_FPSController playerController;
    [Space]
    public Animator animatorMonster;
    public Animator animatorPlayer;
    public Animator psxAnimator;
    public List<GameObject> stuffToDisable;
    public GameObject monsterGeneric;
    [Space]
    public float killDistance = 2f;
    public bool showDistance = false;
    [Space,Range(0,90)]
    public float degreeTollerance = 90;
    public Transform lookAtObjectFront;
    public Transform lookAtObjectBack;
    public Transform PositionAtObjectBack;

    bool isFront = false;
    bool kill = false;
    private void Start() { monsterGeneric.SetActive(false); }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            float rotMonster = transform.rotation.eulerAngles.y;
            float rotPlayer = player.transform.rotation.eulerAngles.y;

            if ((rotPlayer + degreeTollerance) >= rotMonster && (rotPlayer - degreeTollerance) <= rotMonster)
                isFront = false;
            else
                isFront = true;

            //Debug.Log(isFront);
            if (showDistance) Debug.Log(distance);
            
            if (distance < killDistance && distance != 0 && !kill) { kill = true; reached();}
        }
    }

    public void reached()
    {
        playerController.enabled = false;

        foreach (GameObject obj in stuffToDisable)
            obj.SetActive(false);

        patrolling.disablePatrolling = true;
        monsterGeneric.SetActive(true);
        agent.enabled = false;

       
        player.transform.LookAt(isFront?lookAtObjectFront: lookAtObjectBack);
        player.transform.position = PositionAtObjectBack.position;

        animatorPlayer.Play(isFront ? "kill_front" : "kill_back");
        psxAnimator.Play("kill");
        animatorMonster.Play(isFront ? "kill_front" : "kill_back");
    }
}

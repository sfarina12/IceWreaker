using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ques_firstSpawnMonster : MonoBehaviour
{
    public GameObject monster;

    [Space]
    public GameObject monster_Cutscene;
    public NavMeshAgent agent;
    
    [Space,Header("AudioMonsterFirstEncounter")]
    public AudioSource firstCry;
    public Transform destination;
    public Animator animator;
    
    [Space]
    public BoxCollider invisibleWall;
    public GameObject water;
    public float timeBeforeDestination;

    float act_time = 0;
    bool stop = false;

    private void Start()
    {
        monster.SetActive(false);
        monster_Cutscene.SetActive(false);
        invisibleWall.enabled=false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            if (!stop)
            {
                invisibleWall.enabled = true;
                water.SetActive(false);
                monster_Cutscene.SetActive(true);
                firstCry.Play();
                stop = true;
            }
    }

    private void Update()
    {
        if (stop)
        {
            if (act_time >= timeBeforeDestination)
            {
                float speed = agent.velocity.magnitude;
                animator.SetFloat("Vertical", speed);

                agent.SetDestination(destination.position);

                if (agent.remainingDistance < 0.6f && agent.remainingDistance != 0)
                {
                    monster_Cutscene.SetActive(false);
                    monster.SetActive(true);
                    invisibleWall.enabled = false;
                    this.enabled = false;
                }
            }
            else { act_time += Time.deltaTime; }
        }
    }
}

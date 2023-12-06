using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class m_patrolling : MonoBehaviour
{
    [Header("agent stuff")]
    public NavMeshAgent agent;
    public MeshRenderer map;

    [Header("agent settings")]
    [Tooltip("random time before a destination is picked, only integer"),Min(0)]
    public Vector2 destination_time;
    [Min(0)]
    public Vector2 chaseingSound_destination_time;
    public float runningSpeed;
    [Space]
    public Animator animator;
    public SphereCollider playerCollider;

    [Header("AudioController")]
    public audioPlayer monsterAudioPatrolling;
    public audioPlayer monsterAudioAggressive;

    bool startTimeRandomizer = true;
    int randomTime;
    float act_time = 0;
    float baseSpeed;
    bool enlargeRadious = false;
    float originalRadious;

    [HideInInspector]
    public bool disablePatrolling = false;

    private void OnTriggerStay(Collider other)
    {
        if (!disablePatrolling)
        {
            if (other.tag.Equals("Player"))
            {
                if (!enlargeRadious)
                { playerCollider.radius *= 2; enlargeRadious = true; }
                soundTriggered(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!disablePatrolling)
        {
            if (other.tag.Equals("Player"))
            {
                playerCollider.radius /= 2;
                soundTriggered(other.transform);

                enlargeRadious = false;
            }
        }
    }

    void Start() { baseSpeed = agent.speed; originalRadious = playerCollider.radius; }

    void Update()
    {
        if (!disablePatrolling)
        {
            playerAmplifier();

            if (!enlargeRadious)
                if(monsterAudioPatrolling!=null) monsterAudioPatrolling.playAudio();
            else
                if (monsterAudioAggressive != null) monsterAudioAggressive.playAudio();
            

            float speed = agent.velocity.magnitude;

            animator.SetFloat("Vertical", speed);

            float dist = agent.remainingDistance;
            if (dist < 0.5f)
            {
                agent.speed = baseSpeed;

                if (startTimeRandomizer)
                    timeRandomizer(destination_time);

                if (act_time >= randomTime)
                {
                    startTimeRandomizer = true;

                    NavMeshHit hit;

                    Vector3 randomPoint = new Vector3(
                        Random.Range(map.bounds.min.x, map.bounds.max.x),
                        0,
                        Random.Range(map.bounds.min.z, map.bounds.max.z));

                    if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                    {
                        agent.SetDestination(hit.position);
                    }
                }
                else { act_time += Time.deltaTime; }
            }
        }

    }

    private void timeRandomizer(Vector2 time)
    {
        if (!disablePatrolling)
        {
            act_time = 0;

            int minTime = (int)Mathf.Round(time.x);
            int maxTime = (int)Mathf.Round(time.y);

            randomTime = Random.Range(minTime, maxTime);

            startTimeRandomizer = false;
        }
    }

    public void soundTriggered(Transform target)
    {
        if (!disablePatrolling)
        {
            agent.SetDestination(target.position);
            timeRandomizer(chaseingSound_destination_time);
            agent.speed = runningSpeed;
        }
    }

    public void playerAmplifier()
    {
        if (!disablePatrolling)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift)) { playerCollider.radius = originalRadious; }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift)) { playerCollider.radius = 1; }

            if (Input.GetKeyDown(KeyCode.LeftShift)) { playerCollider.radius = originalRadious * 2; }
            else if (Input.GetKeyUp(KeyCode.LeftShift)) { playerCollider.radius = originalRadious / 2; }

            if (Input.GetKey(KeyCode.LeftControl)) { playerCollider.radius = originalRadious / 2; }
            else if (Input.GetKeyUp(KeyCode.LeftControl)) { playerCollider.radius = originalRadious * 2; }
        }
    }
}

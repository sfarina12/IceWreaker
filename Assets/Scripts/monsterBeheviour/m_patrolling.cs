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
    [Tooltip("if takeing too much time to reach the destination, change this one"),Min(0)]
    public float destinationTime_override = 0;
    [Min(0)]
    public Vector2 chaseingSound_destination_time;
    public float runningSpeed;
    [Space]
    public Animator animator;
    public SphereCollider playerCollider;
    public SC_FPSController platerController;
    [Space,Tooltip("The time that will rememeber that it was chasing. When arrive to 0, it will forget")]
    public float timeRememberChasing = 1f;

    [Header("Door opening settings"),Min(0)]
    public float minDoorDistance = 0f;
    [Tooltip("[min 0, max 100] respectivly, min and max chance to open a door or leave it be")]
    public Vector2 chanceOpenDoor = new Vector2(0,1);
    [Min(0),Tooltip("time before deciding if open the door or breake it")]
    public float timeBeforeDeciding = 0f;
    [Space,Tooltip("[min 0, max 100] respectivly, min and max chance to open a door or breake it")]
    public Vector2 chanceBreakeDoor = new Vector2(0,1);
    [Space]
    public List<Interact> doorList;

    [Header("AudioController")]
    public audioPlayer monsterAudioWalking;
    public audioPlayer monsterAudioRunning;
    [Space]
    public float timeAudioNeutral;
    public audioPlayer monsterAudioNeutral;
    public audioPlayer monsterAudioChasing;

    bool startTimeRandomizer = true;

    //TIMERS
    int randomTime;
    float act_time = 0;
    //nel caso in cui ci metta troppo tempo per raggiungere la destinazione, aggiorna la posizione
    float act_destinationTime_override = 0;
    float timer_1 = 0;
    float timer_2 = 0;
    
    float baseSpeed;
    float originalRadious;
    Vector3 lastPosition = Vector3.zero;
    [HideInInspector] public bool enlargeRadious = false;
    [HideInInspector] public bool disablePatrolling = false;
    bool wasChasing = false;
    //door stuff
    float act_time_door_1 = 0;
    Interact chosenDoor = null;

    private void OnTriggerStay(Collider other) {
        if (!disablePatrolling) {
            if (other.tag.Equals("PlayerAudio")) {
                if (!enlargeRadious) { 
                    playerCollider.radius *= 2; 
                    enlargeRadious = true; 
                }
                soundTriggered(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!disablePatrolling) {
            if (other.tag.Equals("PlayerAudio")) {
                playerCollider.radius /= 2;
                soundTriggered(other.transform);
                enlargeRadious = false;
                wasChasing = true;
                timer_2 = timeRememberChasing;
            }
        }
    }

    public void playWalkingAudio() { 
        if(agent.speed == runningSpeed) { monsterAudioRunning.playAudio(); }
        else { monsterAudioWalking.playAudio(); }
    }

    void Start() { baseSpeed = agent.speed; originalRadious = playerCollider.radius; }

    void Update() {
        if (!disablePatrolling) {

            if(timer_2 > 0) { timer_2 -= Time.deltaTime; }
            else { timer_2 = 0; wasChasing = false; }

            playerAmplifier();
            doorChecker();

            if (!enlargeRadious) {
                if (monsterAudioNeutral != null) {
                    if(timer_1 <= 0) {
                        timer_1 = timeAudioNeutral;
                        monsterAudioNeutral.playAudio();
                        monsterAudioChasing.stopAudio();
                    }

                    if(timer_1 > 0) { timer_1 -= Time.deltaTime; }
                }
            } else {
                if (monsterAudioChasing != null) { 
                    monsterAudioNeutral.stopAudio(); 
                    monsterAudioChasing.playAudio(); 
                }
            }

            
            float speed = agent.velocity.magnitude;

            animator.SetFloat("Vertical", speed);

            float dist = agent.remainingDistance;
            if (dist < 0.5f || act_destinationTime_override >= destinationTime_override) {
                agent.speed = baseSpeed;

                if (startTimeRandomizer) timeRandomizer(destination_time);

                if (act_time >= randomTime) {
                    startTimeRandomizer = true;

                    NavMeshHit hit;

                    Vector3 randomPoint = new Vector3(
                        Random.Range(map.bounds.min.x, map.bounds.max.x),
                        0,
                        Random.Range(map.bounds.min.z, map.bounds.max.z));

                    //set random destination when not chasing player
                    if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                        agent.SetDestination(hit.position);
                        act_destinationTime_override = 0;
                    }
                }
                else { 
                    act_time += Time.deltaTime; 
                    act_destinationTime_override += Time.deltaTime; 
                }
            }
        }

    }

    private void timeRandomizer(Vector2 time) {
        if (!disablePatrolling) {
            act_time = 0;

            int minTime = (int)Mathf.Round(time.x);
            int maxTime = (int)Mathf.Round(time.y);

            randomTime = Random.Range(minTime, maxTime);

            startTimeRandomizer = false;
        }
    }

    private void doorChecker() {
        if(act_time_door_1 > 0) {
            if(Vector3.Distance(chosenDoor.transform.position,transform.position) < minDoorDistance) { act_time_door_1 -= Time.deltaTime; } 
            else { 
                act_time_door_1 = 0; 
                chosenDoor = null; 
            }
            act_destinationTime_override = 0;
        }

        if((enlargeRadious || wasChasing) && act_time_door_1 <= 0 && chosenDoor == null) {
            //tra tutte le porte, controllo quella che e piu vicina a me, e poi decido se mi interessa o no
            float actMin_door_distance = 1000f;
            Interact actMin_door_interact = null;

            foreach(Interact door in doorList) {
                if(!door.isOn) {
                    float act_door_distance = Vector3.Distance(door.transform.position,transform.position);
                    if(act_door_distance < minDoorDistance) {
                        if(actMin_door_distance > act_door_distance) { 
                            actMin_door_distance = act_door_distance; 
                            actMin_door_interact = door;
                        }
                    }
                }
            }

            if(actMin_door_interact != null) {
                int rr = Random.Range(0, 100);
                if(rr > chanceOpenDoor.x && rr < chanceOpenDoor.y) { 
                    act_time_door_1 = timeBeforeDeciding; 
                    chosenDoor = actMin_door_interact;
                }
            }
        }

        if(act_time_door_1 <= 0 && chosenDoor != null) {
            int rr = Random.Range(0, 100);
            if(rr > chanceBreakeDoor.x && rr < chanceBreakeDoor.y) { 
                //rompi porta
                chosenDoor.interactByScript = true;
                chosenDoor.interactableFlag = false;
                act_destinationTime_override = 0;
                chosenDoor.animator.Play("breakDoor");
                //chosenDoor.audioPlayer.playAudio();
            } else {
                //apri porta
                chosenDoor.interactByScript = true;
                act_destinationTime_override = 0;
            }

            chosenDoor = null;
            act_time_door_1 = 0;
        }
    }

    public void soundTriggered(Transform target) {
        if (!disablePatrolling) {
            agent.SetDestination(target.position);
            timeRandomizer(chaseingSound_destination_time);
            agent.speed = runningSpeed;
            act_destinationTime_override = 0;
        }
    }

    //gestione audio emesso dal player
    public void playerAmplifier() {
        if (!disablePatrolling) {
            float tmp_speed = Vector3.Distance(platerController.transform.position, lastPosition) / Time.deltaTime;
            lastPosition = platerController.transform.position;
            int speed = (int)runningSpeed;

            if(speed == 0) { playerCollider.radius = 1; }
            else if(platerController.isRunning) { playerCollider.radius = originalRadious * 2; }
            else if(platerController.isCrouch) { playerCollider.radius = originalRadious / 2; }
            else { playerCollider.radius = originalRadious; }
        }
    }
}

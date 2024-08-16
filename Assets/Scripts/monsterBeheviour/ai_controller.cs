using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ai_controller : MonoBehaviour {
    
    [Header("AI Agent")]
    public NavMeshAgent agent;
    [Tooltip("Contains the meshes of all parts of the map. Every time the player unlock an area, the <agent> will try walking on it")]
    public List<map> maps;
    public Animator agent_animator;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    [Space,Header("Settings"),Tooltip("The minimum distance to reach the destination point")]
    public float distanceReached = 0.5f;
    public float runningSpeed;
    [Tooltip("How lickly will remain in the same area"),Range(0f, 1f)]
    public float ignoreRule;
    [Tooltip("Time before forget that was chasing the player"),Min(0f)]
    public float rememberChasing;
    public Vector2 timeNextDestination;
    [Tooltip("How randomly often will go in the same spot where the player currently is"),Range(0f, 1f)]
    public float playerCheck = 0f;

    [Space,Tooltip("The minimum distance to hear the player")]
    public float hearingPlayer = 0f;
    [Tooltip("Multiply the [earingPlayer] value when monster hear player")]
    public float hearingMultiplier = 0f;
    [Tooltip("Divide the [earingPlayer] value when player sneak")]
    public float crouchDivider = 0f;
    [Tooltip("Multiply the [earingPlayer] value when player sprint")]
    public float SprintMultiplier = 0f;
    
    [Space]
    public List<Interact> doors;
    [Tooltip("Minimum door distance to understand if has a door closed in front")]
    public float doorDistance = 0f;
    [Tooltip("[min 0, max 100] respectivly, min and max chance to open a door or leave it be")]
    public Vector2 openDoor = new Vector2(0,1);
    [Min(0),Tooltip("time before deciding if open the door or breake it")]
    public float doorDecidingTime = 0f;
    [Tooltip("[min 0, max 100] respectivly, min and max chance to open a door or breake it")]
    public Vector2 breakDoor = new Vector2(0,1);
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [Space,Header("Audio Settings")]
    public audioPlayer audioNeutral;
    [Tooltip("time before emit a neutral sound")]
    public float timeNeutral = 0f;
    public audioPlayer audioChasing;
    [Space]
    public audioPlayer audioWalking;
    public audioPlayer audioRunning;


    NavMeshHit hit;
    Vector3 destination = Vector3.zero;
    GameObject player;
    SC_FPSController player_controller;
    Vector3 lastPoision;
    Interact door_to_open;

    int lastMap;
    
    bool startAI = true;
    [HideInInspector] public bool isChasing = false;
    bool wasChasing = false;
    bool can_audio_1 = true;
    bool can_audio_2 = true;

    float baseSpeed = 0f;
    float hearingDistance = 0f;
    float timer_audio_1 = 0;
    float timer_next_destination = 0;
    float timer_door_1 = 0;
    float timer_chasing = 0;

    void Start() { 
        if(audioNeutral == null) { can_audio_1 = false; }
        if(audioChasing == null) { can_audio_2 = false; }

        baseSpeed = agent.speed;
        hearingDistance = hearingPlayer;
        player = GameObject.Find("player");
        player_controller = player.GetComponent<SC_FPSController>();
    }

    public void playWalkingAudio() { 
        if(agent.speed == runningSpeed) { audioRunning.playAudio(); }
        else { audioWalking.playAudio(); }
    }

    void Update() {

        controller_door_opener();
        controller_audio_emitter();
        controller_player_trigger();

        //----------------------[UPDATE CHASING]-----------------------
        if(timer_chasing > 0) { timer_chasing -= Time.deltaTime; }
        else { timer_chasing = 0; wasChasing = false; }
        //END ------------------[UPDATE CHASING]-----------------------
        
        //----------------------[UPDATE SPEED]-----------------------
        float speed = agent.velocity.magnitude;
        agent_animator.SetFloat("Vertical", speed);
        //END ------------------[UPDATE SPEED]-----------------------

        //-------------------[RANDOM DESTINATION]--------------------
        float distance = Vector3.Distance(transform.position, destination);
        
        if((distance < distanceReached && !isChasing) || startAI) {
            
            if(timer_next_destination > 0) { timer_next_destination -= Time.deltaTime; }
            else { timer_next_destination = Random.Range(timeNextDestination.x, timeNextDestination.y); }

            if(timer_next_destination <= 0) {

                if(startAI) { startAI = false; }

                if(Random.value > playerCheck) {  
                    List<map> availableMaps = new List<map>();
                    //availableMaps.Add(maps[0]);
                    agent.speed = baseSpeed;

                    foreach(map m in maps) {
                        //posso prendere anche questa mappa nel pool delle possibili destinazioni perche e aperta
                        if(m.entrance == null) { availableMaps.Add(m); }
                        else if(m.entrance.isOn) { availableMaps.Add(m); }
                    }

                    int selectedMap = 0;
                    map selectedM;
                    selectedMap = Random.Range(0, availableMaps.Count-1); 
                    selectedM = availableMaps[selectedMap];

                    //evitare di prendere sempre la stessa mappa per ogni punto
                    if(lastMap == selectedMap) {      
                        if(Random.value > ignoreRule) { 
                            int calc = selectedMap - 1;
                            if(calc < 0) { calc = availableMaps.Count - 1; }

                            selectedMap = calc;
                            selectedM = availableMaps[selectedMap];
                        }
                    }

                    lastMap = selectedMap;

                    Vector3 randomPoint = new Vector3(
                        Random.Range(selectedM.walkabeMesh.bounds.min.x, selectedM.walkabeMesh.bounds.max.x),
                        selectedM.walkabeMesh.transform.position.y,
                        Random.Range(selectedM.walkabeMesh.bounds.min.z, selectedM.walkabeMesh.bounds.max.z)
                    );

                    destination = randomPoint;

                    if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas)) { agent.SetDestination(hit.position); }
                } else {
                    destination = player.transform.position;
                    if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas)) { agent.SetDestination(hit.position); }
                }
            }
        } else {
            //check to avoid getting stuck in loop
            float stuckDistance = Vector3.Distance(lastPoision, transform.position);
            if(stuckDistance == 0f) { startAI = true; }
        }
        //END ---------------[RANDOM DESTINATION]--------------------

        lastPoision = transform.position;
    }

    
    //trig the monster to follow the noise
    public void trigger(Vector3 position) {
        agent.SetDestination(position);
        agent.speed = runningSpeed;
        destination = position;
        timer_next_destination = 0;
    }

    public void controller_player_trigger() {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if(distance < hearingDistance) {
            //ho sentito il player
            isChasing = true;
            wasChasing = true;

            hearingDistance = hearingPlayer * hearingMultiplier;

            agent.SetDestination(player.transform.position);
            agent.speed = runningSpeed;

            destination = player.transform.position;
            timer_next_destination = 0;

            timer_chasing = rememberChasing;
        } else {
            isChasing = false;
            hearingDistance = hearingPlayer;
        }

        if(player_controller.isMoving) {
            if(player_controller.isCrouch) { hearingDistance /= crouchDivider; }
            if(player_controller.isRunning) { hearingDistance *= SprintMultiplier; }
        } else { hearingDistance /= (crouchDivider * 2); }
    }

    private void controller_audio_emitter() {
        if (!isChasing) {
            if (can_audio_1) {
                if(timer_audio_1 <= 0) {
                    timer_audio_1 = timeNeutral;
                    audioNeutral.playAudio();
                    audioNeutral.stopAudio();
                }
            }
        } else {
            if (can_audio_2) { audioChasing.playAudio(); timer_audio_1 = 0; }
        }

        if(timer_audio_1 > 0) { timer_audio_1 -= Time.deltaTime; }
    }

    private void controller_door_opener() {
        if(timer_door_1 > 0) {
            if(Vector3.Distance(door_to_open.transform.position,transform.position) < doorDistance) { timer_door_1 -= Time.deltaTime; } 
            else { 
                timer_door_1 = 0; 
                door_to_open = null; 
            }
            timer_next_destination = 0;
        }
        
        if((isChasing || wasChasing) && timer_door_1 <= 0 && door_to_open == null) {
            // 1 - capire dove il player in quale camera si trova
            // 2 - prendere la porta della camera e controllare se chiusa           

            foreach(map m in maps) {
                if(m.entrance != null) {
                    if(m.walkabeMesh.bounds.Contains(player.transform.position) && !m.entrance.isOn) {
                        int rr = Random.Range(0, 100);
                        if(rr > openDoor.x && rr < openDoor.y) { 
                            timer_door_1 = doorDecidingTime; 
                            door_to_open = m.entrance;                            
                        } 
                    }
                }
            }
        }

        if(timer_door_1 <= 0 && door_to_open != null) {
            int rr = Random.Range(0, 100);
            if(rr > breakDoor.x && rr < breakDoor.y) { 
                //rompi porta
                door_to_open.interactByScript = true;
                door_to_open.interactableFlag = false;
                timer_next_destination = 0;
                door_to_open.animator.Play("breakDoor");
            } else {
                //apri porta
                door_to_open.interactByScript = true;
                timer_next_destination = 0;
            }

            door_to_open = null;
            timer_door_1 = 0;
        }
    }
    //-------------------------[VISUAL PLAYER DISTANCE]-------------------------
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, hearingDistance);
    }
    //END ---------------------[VISUAL PLAYER DISTANCE]-------------------------
}

[Serializable] 
public class map {
    public MeshRenderer walkabeMesh;
    public Interact entrance;
}
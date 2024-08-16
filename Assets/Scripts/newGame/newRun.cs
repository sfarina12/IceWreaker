
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class newRun : MonoBehaviour
{
    public Transform playerCabinPosition;
    public SC_FPSController playerController;
    public Interact button;
    [Space]
    public NavMeshAgent monster;
    public List<Transform> monsterSpawnLocations;
    [Space]
    public GameObject ambienceAudio;

    public void startNewRun() {
        ambienceAudio.SetActive(true);

        playerController.characterController.enabled = false;
        playerController.canMove = false;
        playerController.transform.position = playerCabinPosition.position;
        playerController.characterController.enabled = true;
        playerController.canMove = true;

        int index = Random.Range(0, monsterSpawnLocations.Count);
        monster.Warp(monsterSpawnLocations[index].position);
    }
}

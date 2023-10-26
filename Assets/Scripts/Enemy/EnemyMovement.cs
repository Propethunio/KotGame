using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float fow = 15f;
    [SerializeField] float rotateSpeed = 7f;
    [SerializeField] GameObject player;

    NavMeshAgent agent;
    bool isWalking;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update() {
        HandleMovement();
    }

    void HandleMovement() {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance > fow) {
            return;
        }
        agent.destination = player.transform.position;
        transform.forward = Vector3.Slerp(transform.forward, player.transform.position - transform.position, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
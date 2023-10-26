using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float patrolMoveSpeed = 3f;
    [SerializeField] float attackMoveSpeed = 7f;
    [SerializeField] float fow = 15f;
    [SerializeField] float range = 12f;
    [SerializeField] float rotateSpeed = 7f;
    [SerializeField] GameObject player;

    NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolMoveSpeed;
    }

    void Update() {
        HandleMovement();
    }

    void HandleMovement() {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer < fow) {
            agent.speed = attackMoveSpeed;
            agent.SetDestination(player.transform.position);
            transform.forward = Vector3.Slerp(transform.forward, player.transform.position - transform.position, rotateSpeed * Time.deltaTime);
        } else if(agent.remainingDistance <= agent.stoppingDistance) {
            agent.speed = patrolMoveSpeed;
            Vector3 point;
            if(TryGetRandomPoint(transform.position, range, out point)) {
                agent.SetDestination(point);
            }
        }
    }

    bool TryGetRandomPoint(Vector3 center, float range, out Vector3 result) {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
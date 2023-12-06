using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float patrolMoveSpeed = 3f;
    [SerializeField] float attackMoveSpeed = 7f;
    [SerializeField] float fow = 15f;
    [SerializeField] float shootingDistance = 8.5f;
    [SerializeField] float rotateSpeed = 7f;
    [SerializeField] float fireForce = 16f;
    [SerializeField] GameObject player;
    [SerializeField] float fireRate = 1.5f;

    float lastShoot;
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
        if(distanceToPlayer <= shootingDistance) {
            agent.isStopped = true;
            transform.forward = player.transform.position - transform.position;
            Shoot();
        } else if(distanceToPlayer <= fow) {
            agent.speed = attackMoveSpeed;
            agent.SetDestination(player.transform.position);
            transform.forward = Vector3.Slerp(transform.forward, player.transform.position - transform.position, rotateSpeed * Time.deltaTime);
        } else if(agent.remainingDistance <= agent.stoppingDistance) {
            agent.speed = patrolMoveSpeed;
            Vector3 point;
            if(TryGetRandomPoint(transform.position, 10f, out point)) {
                agent.SetDestination(point);
            }
        }
    }

    void Shoot() {
        if(Time.time >= 1f / fireRate + lastShoot) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * fireForce, ForceMode.Impulse);
            lastShoot = Time.time;
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
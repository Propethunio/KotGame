using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    [SerializeField] GameInput gameInput;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float fireForce = 16f;
    [SerializeField] float fireRate = .6f;

    float lastShoot;

    void Start() {
        lastShoot = Time.time - fireRate;
    }

    void Update() {
        HandleShooting();
    }

    void HandleShooting() {
        Vector2 inputVector = gameInput.GetShootingVectorNormalized();
        if(inputVector == Vector2.zero) {
            return;
        }
        Vector3 shootingDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.forward = Vector3.Slerp(transform.forward, shootingDir, rotateSpeed * Time.deltaTime);
        Shoot();
    }

    void Shoot() {
        if(Time.time < fireRate + lastShoot) {
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        lastShoot = Time.time;
    }
}
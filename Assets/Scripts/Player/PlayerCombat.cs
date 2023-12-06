using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public enum Weapon {
        none,
        machineGun,
        shootgun
    }

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireForce = 16f;
    [SerializeField] float pistolFireRate = 1.2f;
    [SerializeField] float machineGunFireRate = 6f;
    [SerializeField] float shootgunFireRate = .7f;
    [SerializeField] int shootgunPallets = 5;

    [HideInInspector] public int bullets;
    [HideInInspector] public Weapon weapon;

    float lastShoot;

    public void TryShoot() {
        switch(weapon) {
            case Weapon.none:
                ShootPistol();
                break;
            case Weapon.machineGun:
                ShootMachinegun();
                break;
            case Weapon.shootgun:
                ShootShootgun();
                break;
        }
    }

    void ShootPistol() {
        if(Time.time >= 1f / pistolFireRate + lastShoot) {
            print(firePoint.forward);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((firePoint.forward).normalized * fireForce, ForceMode.Impulse);
            lastShoot = Time.time;
        }
    }

    void ShootMachinegun() {
        if(Time.time >= 1f / machineGunFireRate + lastShoot) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector3 spread = new Vector3(Random.Range(-.1f, .1f), 0f, Random.Range(-.1f, .1f));
            bullet.GetComponent<Rigidbody>().AddForce((firePoint.forward + spread).normalized * fireForce, ForceMode.Impulse);
            lastShoot = Time.time;
            bullets--;
            if(bullets <= 0) {
                weapon = Weapon.none;
            }
        }
    }

    void ShootShootgun() {
        if(Time.time >= 1f / shootgunFireRate + lastShoot) {
            for(int i = 0; i < shootgunPallets; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Vector3 spread = new Vector3(Random.Range(-.2f, .2f), 0f, Random.Range(-.2f, .2f));
                bullet.GetComponent<Rigidbody>().AddForce((firePoint.forward + spread).normalized * fireForce, ForceMode.Impulse);
            }
            lastShoot = Time.time;
            bullets--;
            if(bullets <= 0) {
                weapon = Weapon.none;
            }
        }
    }
}
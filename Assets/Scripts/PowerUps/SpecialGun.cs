using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGun : MonoBehaviour {

    [SerializeField] int bullets;
    [SerializeField] PlayerCombat.Weapon weapon;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            PlayerCombat combat = other.gameObject.GetComponent<PlayerCombat>();
            combat.bullets = bullets;
            combat.weapon = weapon;
            Destroy(gameObject);
        }
    }
}
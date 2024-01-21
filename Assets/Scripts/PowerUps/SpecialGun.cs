using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGun : MonoBehaviour {

    [SerializeField] int bullets;
    [SerializeField] PlayerCombat.Weapon weapon;

    public delegate void PickedWeapon();
    public static event PickedWeapon OnPickedWeapon;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            PlayerCombat combat = other.gameObject.GetComponent<PlayerCombat>();
            combat.bullets = bullets;
            combat.weapon = weapon;
            OnPickedWeapon?.Invoke();
            Destroy(gameObject);
        }
    }
}
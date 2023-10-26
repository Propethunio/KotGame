using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDamage : MonoBehaviour
{
    public PlayerHealthSystem playerHealthSystem;

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            playerHealthSystem.isEnemyEnterCollision = true;
            Debug.Log("Trig");
        }
        else {
            playerHealthSystem.isEnemyEnterCollision = false;
        }
    }

    void OnTriggerStay(Collider collision) {
         if (collision.gameObject.tag == "Enemy") {
            playerHealthSystem.isEnemyStayCollision = true;
             Debug.Log("Trig2");
        }
        else {
            playerHealthSystem.isEnemyStayCollision = false;
            Debug.Log("Trig2 doesn't work.");
        } 
    }
}

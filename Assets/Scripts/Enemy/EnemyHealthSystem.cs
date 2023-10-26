using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour {

    int enemyHealthPoints = 3;

    void Update() {
        CheckForDeath();
    }
    
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Bullet") {
            enemyHealthPoints--;
            Debug.Log("Hit!");
        }
    }

    void CheckForDeath() {
        if(enemyHealthPoints <=0) {
            Destroy(gameObject);
        }
    }
}

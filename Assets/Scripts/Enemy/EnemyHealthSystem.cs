using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour {

    int enemyHealthPoints = 3;
    [SerializeField] GameObject seed;

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
            Instantiate(seed);
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        Instantiate(seed, transform.position, transform.rotation);
    }
}

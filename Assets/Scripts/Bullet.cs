using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float lifeTime = 2.25f;

    void Awake() {
        Destroy(gameObject, lifeTime);
    }
}
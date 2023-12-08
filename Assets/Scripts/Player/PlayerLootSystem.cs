using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLootSystem : MonoBehaviour
{
    [SerializeField] PlayerController playerMovement;
    [SerializeField] InGameUIHandler inGameUIHandler;
    public int seedNumber = 0; // seeds value
    
    void OnCollisionEnter(Collision collision) {
        //Collecting seeds
        if (collision.gameObject.tag == "Seed") {
            Destroy(collision.gameObject);
            seedNumber+=100;
            inGameUIHandler.SeedDisplay();
        }
    }
}

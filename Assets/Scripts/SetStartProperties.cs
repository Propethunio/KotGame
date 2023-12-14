using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartProperties : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject doors;
    [SerializeField] InGameUIHandler inGameUIHandler;
    Vector3 doorsPosition;
    bool doorsOnTrigger;
    
    void Awake()
    {
        doorsPosition = doors.transform.position;
    }

    void Start() {
        SetPlayerPosition();
    }

    void Update()
    {
        EndGame();
    }

    void SetPlayerPosition() {
        doorsPosition.x = 1f;
        doorsPosition.y = 1f;
        player.transform.position = doorsPosition;
    }

    void EndGame(){
        if(inGameUIHandler.slider.value == 0 && doorsOnTrigger) {
            Debug.Log("Game ended.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorsOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorsOnTrigger = false;
        }
    }
}

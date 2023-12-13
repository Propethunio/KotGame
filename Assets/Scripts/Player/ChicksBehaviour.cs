using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ChicksBehaviour : MonoBehaviour {
    NavMeshAgent chick;
    GameObject player;
    PlayerController playerController;
    Transform target;
    PlayerHealthSystem playerHealthSystem;

    //==============DAMAGE FLASH==============
    MeshRenderer meshRenderer;
    Color orgColor;
    float flashTime = 0.30f;
    
    void Awake() {
        chick = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        orgColor = meshRenderer.material.color;
        playerHealthSystem = player.GetComponent<PlayerHealthSystem>();
    }

    void Update() {
       if (chick.enabled && playerController.newBodyPart && playerController.bodyParts.Count > 0 && chick.isOnNavMesh) {
        target = playerController.bodyParts[playerController.bodyParts.Count - 1].transform;
        FindTheWayToLine();
        }
    }

    void FindTheWayToLine() {
        chick.SetDestination(target.position);

        if(DestinationReached(chick, transform.position)){
            Debug.Log("Destination reached!");
                transform.SetParent(target);
                chick.enabled = false;
                transform.localPosition = Vector3.zero;
        }
    }

    public static bool DestinationReached(NavMeshAgent agent, Vector3 actualPosition) {
        if (agent.pathPending) {
            
            return Vector3.Distance(actualPosition, agent.pathEndPosition) <= agent.stoppingDistance;
        }
        else {
            return (agent.remainingDistance <= agent.stoppingDistance);
        }
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            DamageFlash();
            playerHealthSystem.TakeDamage();
            
        }
    }  

    void DamageFlash() {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop () {
        meshRenderer.material.color = orgColor;
    }
}

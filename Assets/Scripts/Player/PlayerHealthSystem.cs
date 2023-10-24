using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour {
    
    public int eggNumber;
    [SerializeField] GameObject showScore;
    [SerializeField] TextMeshProUGUI eggNumberText;
    [SerializeField] PlayerMovement playerMovement;


    void Update() {
        ShowEggScore();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Egg") {
            eggNumber++;
            Debug.Log($"Egg! ({eggNumber})");
            Destroy(collision.gameObject);
            playerMovement.GrowTail();
        }
    }

    void ShowEggScore() {
        if(eggNumber >= 1)
        {
            showScore.SetActive(true);
            eggNumberText.SetText($"x{eggNumber.ToString()}");
        }
        else{
            showScore.SetActive(false);
        }
    }
}

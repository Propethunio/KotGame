using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour {
    
    int eggNumber;
    bool canTakeDamage = true; 
    [SerializeField] GameObject showScore;
    [SerializeField] TextMeshProUGUI eggNumberText;
    [SerializeField] PlayerController playerMovement;
    int damageToTake = 3;
    int takenDamage = 0;

    void Update() {
        ShowEggScore();
        Death();
    }

    void OnCollisionEnter(Collision collision) {
        //Egg collecting and growing tail
        if (collision.gameObject.tag == "Egg") {
            eggNumber++;
            damageToTake+=3;
            Destroy(collision.gameObject);
            playerMovement.GrowTail();
        }

        //Checking for enemy collision
        if (collision.gameObject.tag == "Enemy") {
            TakeDamage();
        }
    }

    void ShowEggScore() {
        if (eggNumber >= 1) {
            showScore.SetActive(true);
            eggNumberText.SetText($"x{eggNumber.ToString()}");
        }
        else {
            showScore.SetActive(false);
        }
    }

    void TakeDamage() {
        if(eggNumber == 0)
        {
            StartCoroutine(WithoutEgg(1.0f));
        }

        if (eggNumber >= 1 && canTakeDamage) {
            StartCoroutine(DecreaseEgg(1.0f));
        }
    }

    IEnumerator DecreaseEgg(float cooldownTime) {
        takenDamage++;

        if(takenDamage == 3){
            eggNumber--;
            playerMovement.DecreaseTail();
            takenDamage = 0;
            damageToTake-=3;
        }

        canTakeDamage = false; 
        yield return new WaitForSeconds(cooldownTime);
        canTakeDamage = true; 
    }

    IEnumerator WithoutEgg(float cooldownTime) {
        takenDamage++;

        if(takenDamage == 3){
            takenDamage = 0;
            damageToTake-=3;
        }

        canTakeDamage = false; 
        yield return new WaitForSeconds(cooldownTime);
        canTakeDamage = true; 
    }

    void Death() {
        if(damageToTake == 0) {
            Debug.Log("Dead!");
        }
    }
}

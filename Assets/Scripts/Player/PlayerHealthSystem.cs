using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour {
    
    int eggNumber;
    [SerializeField] GameObject showScore;
    [SerializeField] TextMeshProUGUI eggNumberText;
    [SerializeField] PlayerControlls playerMovement;
    public bool isEnemyEnterCollision;
    public bool isEnemyStayCollision;
    bool isDecreasingEggs = false;


    void Update() {
        ShowEggScore();
        CheckForDamage();
    }

    void OnCollisionEnter(Collision collision) {
        //Egg collecting and growing tail
        if (collision.gameObject.tag == "Egg") {
            eggNumber++;
            Destroy(collision.gameObject);
            playerMovement.GrowTail();
        }

        //Checking for enemy enter collision
        if (collision.gameObject.tag == "Enemy") {
            isEnemyEnterCollision = true;
        }
        else {
            isEnemyEnterCollision = false;
        }
    }

    void OnCollisionStay(Collision collision) {
        //Checking for enemy enter collision
        if (collision.gameObject.tag == "Enemy") {
            isEnemyStayCollision = true;
        }
        else {
            isEnemyStayCollision = false;
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

    void CheckForDamage() {
        if (eggNumber == 0 && isEnemyEnterCollision) {
            Debug.Log("DEAD!");
        }

        if (eggNumber >= 1) {
            if (isEnemyStayCollision) {
                if (!isDecreasingEggs) {
                    StartCoroutine(DecreaseEggs(4.0f));
                }
            }
            else {
                StopCoroutine(DecreaseEggs(1.0f)); 
            }
        }
    }

    IEnumerator DecreaseEggs(float decreaseInterval) {
        isDecreasingEggs = true;
        while (eggNumber > 0){
            eggNumber--;
            playerMovement.DecreaseTail();
            yield return new WaitForSeconds(decreaseInterval);
        }
        isDecreasingEggs = false;
        
        if (eggNumber == 0 && isEnemyEnterCollision) {
            Debug.Log("VERY DEAD!");
        }
    }
}

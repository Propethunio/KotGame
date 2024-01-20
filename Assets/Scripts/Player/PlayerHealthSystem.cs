using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthSystem : MonoBehaviour {

    [SerializeField] GameObject starsCointener;
    [SerializeField] GameObject endScreen;
    [SerializeField] TMP_Text textEnd;

    int eggNumber;
    bool canTakeDamage = true; 
    [SerializeField] GameObject showScore;
    [SerializeField] TextMeshProUGUI eggNumberText;
    [SerializeField] PlayerController playerMovement;
    [SerializeField] int damageToTake = 3;
    [SerializeField] int takenDamage = 0;
    public Vector3 takenEggPosition;

    //==============DAMAGE FLASH==============
    [SerializeField] MeshRenderer meshRenderer;
    Color orgColor;
    float flashTime = 0.30f;

    void Start() {
        orgColor = meshRenderer.material.color;
    }

    void Update() {
        ShowEggScore();
        Death();
    }

    void OnCollisionEnter(Collision collision) {
        //Egg collecting and growing tail
        if (collision.gameObject.tag == "Egg") {
            eggNumber++;
            damageToTake+=3;

            if(collision.gameObject != null) 
                takenEggPosition = collision.gameObject.transform.position;

            playerMovement.GrowTail();
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "MeleeAttack") {
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

    public void TakeDamage() {
        if(eggNumber == 0) {
            DamageFlash();
            StartCoroutine(WithoutEgg(0.5f));
        }

        if (eggNumber >= 1 && canTakeDamage) {
            DamageFlash();
            StartCoroutine(DecreaseEgg(0.5f));
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
            endScreen.SetActive(true);
            textEnd.text = "You lost - died";
            Time.timeScale = 0f;
            starsCointener.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameUIHandler : MonoBehaviour
{
    //================SEED================
    int seedNumber = 0; // seeds value
    [SerializeField] TextMeshProUGUI seedText;

    //================PAUSE================
    [SerializeField] GameObject pauseMenu;
    bool isPauseMenuOn;

    //================TIME================
    [SerializeField] Slider slider;

    [SerializeField] float duration = 120.0f; //  minutes in seconds
    private float targetValue;
    private float startValue;
    private float elapsedTime = 0.0f;

    private void Start() {
        slider.value = 100f;
        targetValue = 0f;
        startValue = slider.value;

        if(!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
    }

    public void PauseMenu() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;  
        isPauseMenuOn = true;
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPauseMenuOn = false;
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Levels");
    }

    public void SeedDisplay() {
        seedNumber+=100;
        seedText.SetText($"{seedNumber.ToString()}");
    }

    private System.Collections.IEnumerator DecreaseSliderValue() {
        while (elapsedTime < duration) {
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            slider.value = newValue;
            elapsedTime += Time.deltaTime;

            yield return null;
        }   
            slider.value = targetValue;
    } 
}

/*
void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Seed") {
            Destroy(collision.gameObject);
            inGameUIHandler.SeedDisplay();
        }
    }
*/

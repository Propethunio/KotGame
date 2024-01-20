using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject starsCointener;
    [SerializeField] TMP_Text textEnd;

    //================SEED================
    [SerializeField] TextMeshProUGUI seedText;
    [SerializeField] PlayerLootSystem playerLootSystem;

    //================PAUSE================
    [SerializeField] GameObject pauseMenu;
    bool isPauseMenuOn;

    //================TIME================
    public Slider slider;
    [SerializeField] float duration = 120.0f; //  minutes in seconds
    private float targetValue;
    private float startValue;
    private float elapsedTime = 0.0f;

    //================GUNS================
    [SerializeField] PlayerCombat playerCombat;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject machinegun;
    [SerializeField] TextMeshProUGUI[] bulletsText = new TextMeshProUGUI[2];


    private void Start() {
        slider.value = 100f;
        targetValue = 0f;
        startValue = slider.value;

        if(!isPauseMenuOn)
            StartCoroutine(DecreaseSliderValue());
    }

    void Update() {
        DisplayGuns();
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
        seedText.SetText($"{playerLootSystem.seedNumber.ToString()}");
    }

    private System.Collections.IEnumerator DecreaseSliderValue() {
        while (elapsedTime < duration) {
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            slider.value = newValue;
            elapsedTime += Time.deltaTime;

            yield return null;
        }   
            slider.value = targetValue;
        GameEnd();
    }

    private void GameEnd()
    {
        endScreen.SetActive(true);
        textEnd.text = "You lost - time end";
        starsCointener.SetActive(false);
        Time.timeScale = 0f;
    }

    void DisplayGuns() {

        if(playerCombat.gunInUse[1] == true)
        {
            shotgun.SetActive(true);
            bulletsText[0].SetText($"{playerCombat.bullets.ToString()}");
        }
        else
        {
            shotgun.SetActive(false);
        }

        if(playerCombat.gunInUse[2] == true)
        {
            machinegun.SetActive(true);
            bulletsText[1].SetText($"{playerCombat.bullets.ToString()}");
        }
        else
        {
            machinegun.SetActive(false);
        }
    }
}
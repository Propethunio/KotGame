using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
    }


    public void Retry()
    {
        SceneManager.LoadScene("Area51");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

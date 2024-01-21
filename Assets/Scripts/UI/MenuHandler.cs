using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject panelHow;
    public void How() {
        panelHow.SetActive(true);
    }

    public void Exit()
    {
        panelHow.SetActive(false);
    }
}

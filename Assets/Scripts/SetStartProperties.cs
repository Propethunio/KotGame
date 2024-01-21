using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartProperties : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject doors;
    [SerializeField] GameObject doors2;
    [SerializeField] InGameUIHandler inGameUIHandler;
    [SerializeField] GameObject getBackToCoopScreen;
    Vector3 doorsPosition;

    public delegate void GetBack();
    public static event GetBack OnGetBack;

    private void OnEnable()
    {
        OnGetBack += EndGame;
    }

    void Awake()
    {
        doorsPosition = doors.transform.position;
    }

    void Start() {
        SetPlayerPosition();
        StartCoroutine(WaitTillPlayerLeave());
    }


    void SetPlayerPosition() {
        doorsPosition.x = 1f;
        doorsPosition.y = 1f;
        player.transform.position = doorsPosition;
    }

    void EndGame() {
        getBackToCoopScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        getBackToCoopScreen.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnGetBack?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnGetBack -= EndGame;
    }

    private IEnumerator WaitTillPlayerLeave()
    {
        yield return new WaitForSeconds(3f);
        doors2.GetComponent<BoxCollider>().enabled = true;
    }
}

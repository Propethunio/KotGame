using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ChickPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] ChicksPowerUps chicksPowerUpsObject;
    [SerializeField] Renderer material;

    public delegate void IsPicked();
    public static event IsPicked OnIsPicked;

    [SerializeField] LayerMask layer1;
    [SerializeField] LayerMask layer2;

    private void OnEnable()
    {
        switch (gameObject.tag)
        {
            case "Protection":
                OnIsPicked += CorProtection;
                OnIsPicked += SetParent;
                break;
            case "Strength":
                OnIsPicked += SetParent;
                break;
        }
    }

    private void Start() {
        string tag = gameObject.tag;
    }

    private void Update() {
    
    }

    private void SetParent() {
        Rotate parent = GameObject.FindObjectOfType<Rotate>();
        
         transform.SetParent(parent.gameObject.transform);
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Here");
            OnIsPicked?.Invoke();
        }
    }

    private void CorProtection() {
        StartCoroutine(Protection());
    }

    private IEnumerator Protection() {

        Debug.Log("Here2");
        Color color = material.material.color;
        color = new Color(1, 1, 1, 0.5f);
        material.material.color = color;

        Physics.IgnoreLayerCollision(6, 8, true );


        yield return new WaitForSeconds(chicksPowerUpsObject.durationTime);

        color = new Color(1, 1, 1, 1);
        material.material.color = color;

        Physics.IgnoreLayerCollision(6, 8, false);
    }

    private IEnumerator Strength() {

        yield return new WaitForSeconds(chicksPowerUpsObject.durationTime);

    }


    private void OnDisable() {
        switch (gameObject.tag)
        {
            case "Protection":
                OnIsPicked -= CorProtection;
                OnIsPicked -= SetParent;
                break;
            case "Strength":
                OnIsPicked -= SetParent;
                break;
        }
    }
}

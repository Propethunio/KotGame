using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickPowerUpBehaviour : MonoBehaviour
{
    [SerializeField] ChicksPowerUps chicksPowerUpsObject;
    [SerializeField] Renderer material;
    GameObject rotationObj;

    public delegate void IsPicked();
    public static event IsPicked OnIsPicked;

    private void OnEnable()
    {
       OnIsPicked += CorProtection;       
    }

    private void Start()
    {
        rotationObj = GameObject.FindGameObjectWithTag("Rotation");
        string tag = gameObject.tag;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Here");
            OnIsPicked?.Invoke();
            gameObject.transform.SetParent(rotationObj.transform);
            transform.localPosition += new Vector3(0f, 0f, 1f);
            gameObject.GetComponent<Rotate>().enabled = true;
        }
    }

    void CorProtection()
    {
        StartCoroutine(Protection());
    }

    private IEnumerator Protection()
    {
        Debug.Log("Here2");
        Color color = material.material.color;
        color = new Color(1, 1, 1, 0.5f);
        material.material.color = color;

        Physics.IgnoreLayerCollision(6, 8, true);

        yield return new WaitForSeconds(chicksPowerUpsObject.durationTime);

        color = new Color(1, 1, 1, 1);
        material.material.color = color;

        Physics.IgnoreLayerCollision(6, 8, false);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        OnIsPicked -= CorProtection;
    }
}

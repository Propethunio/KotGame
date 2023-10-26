using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] GameObject bodyPrefab;
    List<GameObject> bodyParts = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();
    [SerializeField] int gap = 10;
    float newZPosition = -1.0f;

    public bool isWalking;

    void Update() {
        HandleMovement();
        
    }

    void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;


        //Chicks position and movement
        if (isWalking && bodyParts.Count > 0) {
            bodyParts[0].transform.position = transform.position;
        }

        PositionHistory.Insert(0, transform.position);
        int index = 1;
        foreach (var body in bodyParts) {
            Vector3 point = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];
            body.transform.position = point;
            index++;
        }
    }

    public bool IsWalking() {
        return isWalking;
    }

    public void GrowTail() {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZPosition);
        GameObject body = Instantiate(bodyPrefab, newPosition, Quaternion.identity);
        body.transform.SetParent(gameObject.transform);
        bodyParts.Add(body);
       
    }

    public void DecreaseTail() {
        int numChildren = gameObject.transform.childCount;            

        if (numChildren > 0)
        {
            bodyParts.RemoveAt(bodyParts.Count - 1);
            Destroy(gameObject.transform.GetChild(numChildren - 1).gameObject);       
        }
    }

}
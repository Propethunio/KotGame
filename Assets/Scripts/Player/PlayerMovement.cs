using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;

    bool isWalking;

    void Update() {
        HandleMovement();
    }

    void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        isWalking = moveDir != Vector3.zero;
    }

    public bool IsWalking() {
        return isWalking;
    }
}
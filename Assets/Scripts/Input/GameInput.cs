using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

    PlayerInputActions playerInputActions;

    void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }

    public Vector2 GetShootingVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Shoot.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
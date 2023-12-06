using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameObject bodyPrefab;
    List<GameObject> bodyParts = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();
    [SerializeField] int gap = 10;
    float newZPosition = -1.0f;

    PlayerCombat combat;
    Rigidbody rb;
    Vector3 moveDir;

    void Start() {
        rb = GetComponent<Rigidbody>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update() {
        HandleInputs();
    }

    void FixedUpdate() {
        HandleMovement();
    }

    void HandleInputs() {
        Vector2 movementInputVector = gameInput.GetMovementVectorNormalized();
        moveDir = new Vector3(movementInputVector.x, 0f, movementInputVector.y);
        Vector2 shootingInputVector = gameInput.GetShootingVectorNormalized();
        if(moveDir != Vector3.zero && shootingInputVector == Vector2.zero) {
            RotatePlayer(moveDir);
        }
        if(shootingInputVector != Vector2.zero) {
            Vector3 shootingDir = new Vector3(shootingInputVector.x, 0f, shootingInputVector.y);
            RotatePlayer(shootingDir);
            combat.TryShoot();
        }

        //Chicks position and movement

        if(bodyParts.Count > 0) {
            bodyParts[0].transform.position = transform.position;
        }

        PositionHistory.Insert(0, transform.position);
        int index = 1;
        foreach(var body in bodyParts) {
            Vector3 point = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];
            body.transform.position = point;
            index++;
        }
    }

    void HandleMovement() {
        rb.MovePosition(transform.position + (moveDir * moveSpeed * Time.fixedDeltaTime));
    }

    void RotatePlayer(Vector3 dir) {
        transform.forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.deltaTime);
    }

    public void GrowTail() {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZPosition);
        GameObject body = Instantiate(bodyPrefab, newPosition, Quaternion.identity);
        body.transform.SetParent(gameObject.transform);
        bodyParts.Add(body);
    }

    public void DecreaseTail() {
        int numChildren = gameObject.transform.childCount;

        if(numChildren > 0) {
            bodyParts.RemoveAt(bodyParts.Count - 1);
            Destroy(gameObject.transform.GetChild(numChildren - 1).gameObject);
        }
    }
}
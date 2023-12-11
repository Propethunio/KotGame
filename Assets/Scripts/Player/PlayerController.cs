using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] GameInput gameInput;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameObject positionHolderPrefab;
    [SerializeField] PlayerHealthSystem playerHealthSystem;
    [SerializeField] GameObject bodyPrefab;
    public List<GameObject> bodyParts = new List<GameObject>();
    List<Vector3> PositionHistory = new List<Vector3>();
    [SerializeField] int gap = 10;
    float newZPosition = -1.0f;
    [SerializeField] GameObject bodyParent;
    PlayerCombat combat;
    Rigidbody rb;
    Vector3 moveDir;   
    bool isWalking;
    public bool newBodyPart;

    void Start() {
        rb = GetComponent<Rigidbody>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update() {
        HandleInputs();
        isWalking = moveDir != Vector3.zero;
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

       const int maxHistoryCount = 2000;

        if (isWalking) {
            PositionHistory.Insert(0, transform.position);

            if (PositionHistory.Count > maxHistoryCount) {
                PositionHistory.RemoveRange(maxHistoryCount, PositionHistory.Count - maxHistoryCount);
            }
        }

        int index = 1;
        foreach (var body in bodyParts) {
            int pointIndex = Mathf.Min(index * gap, PositionHistory.Count - 1);
            Vector3 point = PositionHistory[pointIndex];
            body.transform.position = point;
            index++;
        }
    }

    void HandleMovement() {
        rb.MovePosition(transform.position + (moveDir * moveSpeed * Time.smoothDeltaTime));
    }

    void RotatePlayer(Vector3 dir) {
        transform.forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.smoothDeltaTime);
    }

    public void GrowTail() {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZPosition);
        GameObject positionHolder = Instantiate(positionHolderPrefab, newPosition, Quaternion.identity);
        positionHolder.transform.SetParent(bodyParent.transform);
        bodyParts.Add(positionHolder);
        newBodyPart = true;

        GameObject body = Instantiate(bodyPrefab, playerHealthSystem.takenEggPosition, Quaternion.identity);
    }

    public void DecreaseTail() {
        int numChildren = bodyParent.transform.childCount;

        if(numChildren > 0) {
            bodyParts.RemoveAt(0);
            Destroy(bodyParent.transform.GetChild(0).gameObject);
        }
    }
}
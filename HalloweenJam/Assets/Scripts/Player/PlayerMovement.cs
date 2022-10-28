using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform gfx;

    private Rigidbody rb;
    private bool isMoving;
    private bool isPaused;

    public UnityAction<bool> PlayerMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameStateManager.Instance.OnGameStateChange += OnGameStateChange;
    }

    void Update()
    {
        Vector2 movementDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (movementDirection.magnitude > 1) {
            movementDirection = movementDirection.normalized;
        }

        Vector2 cameraRelativeMovement = GetCameraRelativeMovement(movementDirection);
        if (isPaused) cameraRelativeMovement *= 0;

        Move(cameraRelativeMovement);
        Rotate(cameraRelativeMovement);
    }

    private void Move(Vector2 direction) {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        Vector3 pos = transform.position + movement * movementSpeed * Time.deltaTime;
        rb.MovePosition(pos);

        // movement event
        if (!isMoving && direction.sqrMagnitude > 0) {
            isMoving = true;
            PlayerMoving?.Invoke(true);
        }
        else if (isMoving && direction.sqrMagnitude == 0) {
            isMoving = false;
            PlayerMoving?.Invoke(false);
        }
    }

    private Vector2 GetCameraRelativeMovement(Vector2 movementDirection) {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraForward * movementDirection.y + cameraRight * movementDirection.x;
        return new Vector2(movement.x, movement.z);
    }

    static float ROTATION_OFFSET = 90;
    private void Rotate(Vector2 direction) {
        if (direction.sqrMagnitude > 0) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - ROTATION_OFFSET;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.down);
            gfx.transform.rotation = Quaternion.Slerp(gfx.transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnGameStateChange(GameState newGameState) {
        switch (newGameState) {
            case GameState.Gameplay:
                isPaused = false;
                break;
            case GameState.Paused:
                isPaused = true;
                break;
        }
    }
}

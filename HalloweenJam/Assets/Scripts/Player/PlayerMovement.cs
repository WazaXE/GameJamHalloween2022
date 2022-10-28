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

    public UnityAction<bool> PlayerMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 movementVector = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (movementVector.magnitude > 1) {
            movementVector = movementVector.normalized;
        }
        Move(movementVector);
        Rotate(movementVector);
    }

    private void Move(Vector3 direction) {
        Vector2 movementVector = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (movementVector.magnitude > 1) {
            movementVector = movementVector.normalized * movementSpeed * Time.deltaTime;
        }
        else {
            movementVector *= movementSpeed * Time.deltaTime;
        }

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

    private void Rotate(Vector3 direction) {

    }
}

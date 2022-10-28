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
        Vector2 movementDirection = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
        if (movementDirection.magnitude > 1) {
            movementDirection = movementDirection.normalized;
        }

        Move(new Vector3(movementDirection.x, 0, movementDirection.y));
        Rotate(movementDirection);
    }

    private void Move(Vector3 direction) {
        Vector3 movement = direction * movementSpeed * Time.deltaTime;
        Vector3 pos = transform.position + movement;
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

    static float ROTATION_OFFSET = 90;
    private void Rotate(Vector2 direction) {
        if (direction.sqrMagnitude > 0) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - ROTATION_OFFSET;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.down);
            gfx.transform.rotation = Quaternion.Slerp(gfx.transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }
}

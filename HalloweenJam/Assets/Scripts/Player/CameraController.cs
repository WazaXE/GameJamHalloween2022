using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform rotationTarget;

    void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal Right"));
        float rotationAmount = Input.GetAxis("Horizontal Right") * rotationSpeed * Time.deltaTime;
        Vector3 rotation = rotationTarget.rotation.eulerAngles;
        rotation.y += rotationAmount;
        rotationTarget.rotation = Quaternion.Euler(rotation);
    }
}

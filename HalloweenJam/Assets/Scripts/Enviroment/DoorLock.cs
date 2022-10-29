using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DoorLock : MonoBehaviour
{
    [SerializeField] private bool locked;
    [SerializeField] private GameObject key;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(locked) rb.freezeRotation = true;
    }

    public bool UnlockDoor(GameObject usedKey) {
        if (key == null) return false;
        if (usedKey != key) return false;

        rb.freezeRotation = false;
        Destroy(usedKey);
        return true;
    }
}

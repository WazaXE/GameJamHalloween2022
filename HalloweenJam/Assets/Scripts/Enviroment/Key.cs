using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        KeyHandler keyHandler = other.GetComponent<KeyHandler>();
        if (keyHandler == null) return;
        Debug.Log(keyHandler);
        keyHandler.AddKey(gameObject);
        gameObject.SetActive(false);
    }
}

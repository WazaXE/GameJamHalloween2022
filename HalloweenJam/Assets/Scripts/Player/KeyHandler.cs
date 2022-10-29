using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    private List<GameObject> keys;

    private void OnTriggerEnter(Collider other) {
        Key key = other.GetComponent<Key>();
        if (key == null) return;
        key.Collect(transform);
        key.OnCollected += OnKeyCollected;
    }

    private void OnKeyCollected(Collectable c) {
        Key key = (Key)c;
        AddKey(key.GetKey());
    }

    private void AddKey(GameObject key) {
        if(keys == null) keys = new List<GameObject>();
        keys.Add(key);
    }

    public void EnteredUnlockArea(DoorLock doorLock) {
        if (keys == null) return;
        for (int i = 0; i < keys.Count; i++) {
            if (doorLock.UnlockDoor(keys[i])) {
                keys.Remove(keys[i]);
                break;
            }
        }
    }
}

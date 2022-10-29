using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    private List<GameObject> keys;

    public void AddKey(GameObject key) {
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

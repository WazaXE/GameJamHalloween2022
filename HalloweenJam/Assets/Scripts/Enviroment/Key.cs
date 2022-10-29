using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectable
{
    public GameObject GetKey() {
        return gameObject;
    }

    protected override void Collected() {
        gameObject.SetActive(false);
    }
}

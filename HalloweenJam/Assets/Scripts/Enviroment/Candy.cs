using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : Collectable
{
    [SerializeField] private int value;

    public int Value => value;

    protected override void Collected() {
        Destroy(gameObject);
    }
}

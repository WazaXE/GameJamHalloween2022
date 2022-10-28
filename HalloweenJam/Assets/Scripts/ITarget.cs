using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    public Transform Position { get; }
    public Faction Faction { get; }
}

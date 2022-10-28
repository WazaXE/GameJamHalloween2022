using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolPathNodes", menuName = "AI/PatrolPathNodes")]
public class PatrolPathNodes : ScriptableObject
{
    [SerializeField] private Vector3[] patrolPoints;

    public Vector3[] PatrolPoints => patrolPoints;
}

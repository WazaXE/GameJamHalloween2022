using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolPathNodes", menuName = "AI/PatrolPathNodes")]
public class PatrolPath : ScriptableObject
{
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private bool isLoop;

    public Vector3[] PatrolPoints => patrolPoints;
    public bool IsLoop => isLoop;

    /// <summary>
    /// Only use from PathBuilder
    /// Not recommended to be used runtime
    /// </summary>
    /// <param name="points"></param>
    public void SetPatrolPoints(Vector3[] points, bool isLoop) {
        patrolPoints = points;
        this.isLoop = isLoop;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : MonoBehaviour
{
    [SerializeField] private PatrolPath targetPath;

    [SerializeField] private Color pointColor;
    [SerializeField] private Color lineColor;

    private void OnDrawGizmos() {
        if (targetPath == null) return;

        for (int i = 0; i < targetPath.PatrolPoints.Length; i++) {
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(targetPath.PatrolPoints[i], 0.2f);

            if (i < targetPath.PatrolPoints.Length - 1) {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(targetPath.PatrolPoints[i], targetPath.PatrolPoints[i + 1]);
            }
        }
    }
}

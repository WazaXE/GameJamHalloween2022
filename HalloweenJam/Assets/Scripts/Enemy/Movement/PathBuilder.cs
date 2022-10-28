using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] private PatrolPath targetPath;
    [SerializeField, Tooltip("If path goes in a loop check this")] bool isLoop;

    public void BuildPath() {
        if(transform.childCount <= 1) {
            Debug.LogWarning("Add more transforms to be able to build a proper path!", this);
            return;
        }

        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++) {
            points.Add(transform.GetChild(i).position);
        }

        targetPath.SetPatrolPoints(points.ToArray(), isLoop);
        Debug.Log("Path Constructed");
    }

    private void OnDrawGizmosSelected() {
        for (int i = 0; i < transform.childCount - 1; i++) {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.2f);

            if (transform.childCount == 1) continue;

            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
}

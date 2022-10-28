using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathBuilder))]
public class BoardEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        PathBuilder script = (PathBuilder)target;
        if (GUILayout.Button("Build Path")) {
            script.BuildPath();
        }
    }
}

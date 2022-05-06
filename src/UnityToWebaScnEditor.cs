using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnityToWebaScn))]
public class UnityToWebaScnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UnityToWebaScn webaScnExporter = (UnityToWebaScn)target;

        if (GUILayout.Button("Generate scn Lines"))
        {
            webaScnExporter.GenerateBatchedScnLines();
        }

        DrawDefaultInspector();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapBuilder))]
public class BuildEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("BuildMap"))
        {
            Debug.Log("こんにちわ");
            MapBuilder builder = target as MapBuilder;
            builder.BuildMap();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


[UnityEditor.CustomEditor(typeof(ObjectFlood))]
public class FloodEditor : Editor
{
    private ObjectFlood _target;
    private void OnEnable()
    {
        _target = (ObjectFlood)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            _target.Flood();
        }
    }
}

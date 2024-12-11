using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(IntensityManager))]
public class IntensityEditor : Editor
{
    private IntensityManager _target;

    private void OnEnable()
    {
        _target = (IntensityManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Prepare"))
        {
            _target.ManageSubScenes();
        }
    }
}

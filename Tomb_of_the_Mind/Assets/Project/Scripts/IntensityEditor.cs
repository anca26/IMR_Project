using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(SubsceneManager))]
public class IntensityEditor : Editor
{
    private SubsceneManager _target;

    private void OnEnable()
    {
        _target = (SubsceneManager)target;
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

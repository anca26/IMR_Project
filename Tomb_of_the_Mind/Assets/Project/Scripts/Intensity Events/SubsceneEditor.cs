#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[UnityEditor.CustomEditor(typeof(SubsceneManager))]
public class SubsceneEditor : Editor
{
    private SubsceneManager _target;

    private void OnEnable()
    {
        // SubsceneEditor
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
#endif
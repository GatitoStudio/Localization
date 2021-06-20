using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationConfigEditor), true)]
public class LocalizationEditorDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        var list = serializedObject.FindProperty("test");
        EditorGUILayout.PropertyField(list, new GUIContent("My List Test"), true);
    }
 
}

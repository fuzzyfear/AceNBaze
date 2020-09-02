using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharackterStats))]
public class CharackterStats_Editor : Editor
{
    SerializedProperty _baseStats_prop;
    const string       _baseStats_name = "_baseStats";

    SerializedProperty _characterStats_prop;
    const string       _characterStats_name = "_characterStats";

    SerializedProperty _characterWorkingStats_prop;
    const string       _characterWorkingStats_name = "_characterWorkingStats";

    private void OnEnable()
    {
        _baseStats_prop             = serializedObject.FindProperty(_baseStats_name);
        _characterStats_prop        = serializedObject.FindProperty(_characterStats_name);
        _characterWorkingStats_prop = serializedObject.FindProperty(_characterWorkingStats_name);

    }


    public override void OnInspectorGUI()
    {




        serializedObject.Update();

        EditorGUILayout.PropertyField(_baseStats_prop);
        EditorGUILayout.Space();
      
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_characterStats_prop, true);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_characterWorkingStats_prop, true);

        serializedObject.ApplyModifiedProperties();
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FunctionTick))]
public class FunctionTick_Editor : Editor
{
    SerializedProperty _functions_prop;
    const string       _functions_name = "_functions";

    SerializedProperty _playerStats_prop;
    const string       _playerStats_name = "_playerStats";

    SerializedProperty _agent_prop;
    const string       _agent_name = "_agent";



    private GUILayoutOption miniButtonWidth   = GUILayout.Width(30f);
    private GUILayoutOption miniFunctionWidth = GUILayout.Width(30f);

    private GUIContent  moveButtonContentUpp  = new GUIContent("\u2191", "move upp"       ),
                        moveButtonContentDown = new GUIContent("\u2193", "move down"      ),
                        addButtonContent      = new GUIContent("+"     , "add element"    ),
		                deleteButtonContent   = new GUIContent("-"     , "delete element" );

    private void OnEnable()
    {
        _functions_prop   = serializedObject.FindProperty(_functions_name);
        _playerStats_prop = serializedObject.FindProperty(_playerStats_name);
        _agent_prop       = serializedObject.FindProperty(_agent_name);
    }


    public override void OnInspectorGUI()
    {
      //  base.OnInspectorGUI();



        serializedObject.Update();

        EditorGUILayout.PropertyField(_playerStats_prop);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_agent_prop);


        displayList(_functions_prop);
        serializedObject.ApplyModifiedProperties();
    }





    private void displayList(SerializedProperty list)
    {

       


        for(int i = 0; i <_functions_prop.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),GUIContent.none);
                ContentButtons(_functions_prop, i);

            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button(addButtonContent, EditorStyles.miniButtonLeft))
        {
            list.arraySize += 1;
        }
    }

    private void ContentButtons(SerializedProperty list, int index)
    {
        if (GUILayout.Button(moveButtonContentUpp, EditorStyles.miniButtonLeft, miniButtonWidth))
            list.MoveArrayElement(index, index - 1);
        if (GUILayout.Button(moveButtonContentDown, EditorStyles.miniButtonLeft, miniButtonWidth))
            list.MoveArrayElement(index, index + 1);
        if (GUILayout.Button(addButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
            list.InsertArrayElementAtIndex(index);
        if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            int oldSize = list.arraySize;
            list.DeleteArrayElementAtIndex(index);
            if(list.arraySize == oldSize)
                list.DeleteArrayElementAtIndex(index);  

        }
    }
}

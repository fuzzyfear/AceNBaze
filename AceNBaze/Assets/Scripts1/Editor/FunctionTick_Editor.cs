using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FunctionTick))]
public class FunctionTick_Editor : Editor
{


    FunctionTick functionTick;

    SerializedProperty _functions_prop;
    const string       _functions_name = "_functions";

    SerializedProperty _functionActive_prop;
    const string        _functionActive_name = "_functionActive";

    SerializedProperty _abilitys_prop;
    const string       _abilitys_name = "_abilitys";

    SerializedProperty _Modifier_prop;
    const string       _Modifier_name = "_Modifier";






    private GUILayoutOption miniButtonWidth   = GUILayout.Width(30f);
   // private GUILayoutOption miniFunctionWidth = GUILayout.Width(30f);

    private GUIContent  moveButtonContentUpp  = new GUIContent("\u2191", "move upp"       ),
                        moveButtonContentDown = new GUIContent("\u2193", "move down"      ),
                        addButtonContent      = new GUIContent("+"     , "add element"    ),
		                deleteButtonContent   = new GUIContent("-"     , "delete element" );

    private void OnEnable()
    {
        functionTick = target as FunctionTick;

        _functions_prop      = serializedObject.FindProperty(_functions_name);
        _functionActive_prop = serializedObject.FindProperty(_functionActive_name);
        _abilitys_prop       = serializedObject.FindProperty(_abilitys_name);
        _Modifier_prop       = serializedObject.FindProperty(_Modifier_name);
    
     
    }


    public override void OnInspectorGUI()
    {
       //  base.OnInspectorGUI();



        serializedObject.Update();
        EditorGUILayout.HelpBox("The Order of the childern must be:" +
                                " \n \n _CharackterAbilitys " +
                                "\n _CharackterStats " +
                                "\n _Functions \n _Model " +
                                "\n ... \n \n " +
                                "since code relyes on the order to acces them fast.", MessageType.Info);
        //
        EditorGUILayout.PropertyField(_abilitys_prop);
        //
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_Modifier_prop);

        EditorGUILayout.Space();

        displayList(_functions_prop, _functionActive_prop);

        serializedObject.ApplyModifiedProperties();
    }


    private void displayList(SerializedProperty listFunc, SerializedProperty listActiv)
    {
        SyncList(listFunc, listActiv);

   
   


        for (int i = 0; i < listFunc.arraySize; ++i)
        {
            EditorGUILayout.BeginHorizontal();


              

                EditorGUILayout.PropertyField(listFunc.GetArrayElementAtIndex(i),GUIContent.none);
               
                ContentButtons(listFunc, listActiv, i);

            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button(addButtonContent, EditorStyles.miniButtonLeft))
        {
            listFunc.arraySize  += 1;
            listActiv.arraySize += 1;
        }
    }

    private void ContentButtons(SerializedProperty listFunc, SerializedProperty listActiv, int index)
    {

        EditorGUILayout.PropertyField(listActiv.GetArrayElementAtIndex(index), GUIContent.none, miniButtonWidth);

        if (GUILayout.Button(moveButtonContentUpp, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            listFunc.MoveArrayElement(index, index - 1);
            listActiv.MoveArrayElement(index, index - 1);
        }
         
        if (GUILayout.Button(moveButtonContentDown, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            listFunc.MoveArrayElement(index, index + 1);
            listActiv.MoveArrayElement(index, index + 1);
        }

        //if (GUILayout.Button(addButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        //{
        //    listFunc.InsertArrayElementAtIndex(index);
        //    listActiv.InsertArrayElementAtIndex(index);
        //}

        if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            int oldSize = listFunc.arraySize;
            listFunc.DeleteArrayElementAtIndex(index);
            if(listFunc.arraySize == oldSize)
                listFunc.DeleteArrayElementAtIndex(index);



            oldSize = listActiv.arraySize;
            listActiv.DeleteArrayElementAtIndex(index);
            if (listActiv.arraySize == oldSize)
                listActiv.DeleteArrayElementAtIndex(index);

        }


    }

    private void SyncList(SerializedProperty listFunc, SerializedProperty listActiv)
    {
        if (listFunc.arraySize == listActiv.arraySize)
            return;

        while (listFunc.arraySize > listActiv.arraySize)
            listActiv.InsertArrayElementAtIndex((listActiv.arraySize == 0) ? listActiv.arraySize : listActiv.arraySize - 1);

        while (listFunc.arraySize < listActiv.arraySize)
        {
            int oldSize = listActiv.arraySize;
            listActiv.DeleteArrayElementAtIndex((listActiv.arraySize == 0) ? listActiv.arraySize : listActiv.arraySize - 1);
            if (listActiv.arraySize == oldSize)
                listActiv.DeleteArrayElementAtIndex((listActiv.arraySize == 0) ? listActiv.arraySize : listActiv.arraySize - 1);
        }
   

    }
}

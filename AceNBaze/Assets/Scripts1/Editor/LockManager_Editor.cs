using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;









[CustomEditor(typeof(LockManager))]
public class LockManager_Editor : Editor
{
    LockManager lockManager;
   
    private void OnEnable()
    {
        lockManager = target as LockManager;
      
    }

    public override bool RequiresConstantRepaint(){  return true;   }


    public override void OnInspectorGUI()
    {
        //  base.OnInspectorGUI();
        serializedObject.Update();
        Lock[] locks = lockManager.DEBUG_GetLocks();

        foreach (Lock actionLock in locks)
        {
            GUILayout.BeginVertical("HelpBox");

                GUILayout.BeginHorizontal();

                    string actrionName = "Action: " + actionLock.LockName + " (..., ";

                    string[] dataTypes = actionLock.GetTypes();
                    int lenght         = dataTypes.Length;
                    for (int i = 0; i < lenght; ++i)
                        if(i+1 !=lenght)
                            actrionName += dataTypes[i] + ", ";
                        else
                            actrionName += dataTypes[i] + ")";


                GUILayout.Label(actrionName, EditorStyles.boldLabel);
                    
                GUILayout.EndHorizontal();
          
                GUILayout.BeginHorizontal();

                    GUILayout.BeginVertical("GroupBox");

                        GUILayout.Label("Current lock: "+actionLock.CurrentLockName + " <" + actionLock.CurrentLockHash.ToString()+">", EditorStyles.largeLabel);


                    GUILayout.EndVertical();

                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
  
    }

}

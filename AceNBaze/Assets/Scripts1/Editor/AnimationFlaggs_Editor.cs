using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationFlagger))]
public class AnimationFlaggs_Editor : Editor
{

    AnimationFlagger animationFlagger;



    private void OnEnable()
    {
        animationFlagger = (AnimationFlagger)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        //serializedObject.Update();




        //serializedObject.ApplyModifiedProperties();
    }







}

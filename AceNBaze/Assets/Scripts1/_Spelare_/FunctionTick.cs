using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionTick : MonoBehaviour
{
    //index for the child that has the ability script on it
    public static int CharackterAbilityChildIndex = 0;




    [SerializeField] private CharacterBaseAbilitys _abilitys; // stats and grund functions like agent
    [SerializeField] private LockManager           _Modifier; // modifes stuff in _abilitys

    [SerializeField] private _FunctionBase[]       _functions;




    private void Start()
    {

    }


    public void Update()
    {
        foreach (_FunctionBase func in _functions)
            func.Tick(_abilitys, _Modifier);
        
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionTick : MonoBehaviour
{
    //index for the child that has the ability script on it
    public static int CharackterAbilityChildIndex = 0;




    [SerializeField] private CharacterBaseAbilitys _abilitys; // stats and grund functions like agent
    [SerializeField] private Modifier              _Modifier; // modifes stuff in _abilitys

    [SerializeField] private _FunctionBase[]       _functions; // list of all the functions, d.v.s the logic of the character
    [SerializeField] private bool[]                _functionActive;//on of swithces for the functions, will be ised for debugs and development


    public void Update()
    {
        //foreach (_FunctionBase func in _functions)
        //    func.Tick(_abilitys, _Modifier);
        int length = _functions.Length;
        for (int i = 0; i < length; ++i)
            if (_functionActive[i])
                _functions[i].Tick(_abilitys, _Modifier);
        
    }




}

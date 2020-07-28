using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionTick : MonoBehaviour
{
    //index for the child that has the ability script on it
    #region Index of the children 
    public static int CharackterAbilityChildIndex = 0;
    public static int CharackterStatsChildIndex   = 1;
    public static int LockManagerChildIndex       = 2;
    #endregion



    [SerializeField] private CharacterBaseAbilitys _abilitys; // stats and grund functions like agent
    [SerializeField] private Modifier              _Modifier; // modifes stuff in _abilitys

    [SerializeField] private _FunctionBase[]       _functions_update; // list of all the functions thats uppdates in the normal uppdate, d.v.s the logic of the character
    [SerializeField] private bool[]                _functionActive_update;//on of swithces for the functions, will be ised for debugs and development


    [SerializeField] private _FunctionBase[] _functions_late_update;     //  list of all the functions thats uppdates in the late uppdate, d.v.s things thant needs to uppdate after the agnet has moved 
    [SerializeField] private bool[]          _functionActive_late_update;//on of swithces for the functions, will be ised for debugs and development



    public void Update()
    {
        int length = _functions_update.Length;
        for (int i = 0; i < length; ++i)
            if (_functionActive_update[i])
                _functions_update[i].Tick(_abilitys, _Modifier);
    }




    public void LateUpdate()
    {


        //foreach (_FunctionBase func in _functions)
        //    func.Tick(_abilitys, _Modifier);
        int length = _functions_late_update.Length;
        for (int i = 0; i < length; ++i)
            if (_functionActive_late_update[i])
                _functions_late_update[i].Tick(_abilitys, _Modifier);

    }




}

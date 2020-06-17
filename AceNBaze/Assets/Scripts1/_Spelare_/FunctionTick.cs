using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionTick : MonoBehaviour
{


    [SerializeField]
    private CharacterBaseAbilitys _abilitys; // stats and grund functions like agent
    private LockManager           _Modifier; // modifes stuff in _abilitys

 

    [SerializeField] private _FunctionBase[] _functions;


    

    private void Start()
    {
        _Modifier = new LockManager();
    }


    public void Update()
    {
        foreach (_FunctionBase func in _functions)
            func.Tick(_abilitys, _Modifier);
        
    }

}

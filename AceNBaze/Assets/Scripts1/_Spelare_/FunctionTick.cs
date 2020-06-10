using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FunctionTick : MonoBehaviour
{

//========================================================================
// structar för funktioner
//========================================================================

    public struct flags
    {
    }
    //TODO: Fixa klar så att det går att låsa saker
    public struct flag
    {
        const string LockfreeLock = "free";
        const int Lockfreehash    = 1294909896; //hårdkodad, värdet av Animator.StringToHash(LockfreeLock)
        public flag(string name)
        {
            PreatyName = name;
            flagKey = Animator.StringToHash(name);

            currentLockName = LockfreeLock;
            currentLockHash = Lockfreehash;
        }

        public string PreatyName { get; }
        public int flagKey { get; }

        public string currentLockName;
        public int currentLockHash;

        public bool Lock(FunctionBase.LockKey key)
        {
            return true;

        }

    }


    [SerializeField] private CharacterInfo  _playerStats;
    [SerializeField] private NavMeshAgent   _agent;

    [SerializeField] private FunctionBase[] _functions;


    

    private void Start()
    {
        Debug.Log(Animator.StringToHash("free"));
    }


    public void Update()
    {
       
        
    }




}

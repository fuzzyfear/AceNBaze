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

    public struct flag
    {
        const string LockfreeName  = "free";
        const int     Lockfreehash = 1294909896; //hårdkodad, värdet av Animator.StringToHash(LockfreeLock)
        public flag(string name)
        {
            PreatyName = name;
            flagKey    = Animator.StringToHash(name);

            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }

        public string PreatyName { get; }
        public int    flagKey    { get; }

        public string currentLockName;
        public int    currentLockHash;

        public bool Lock(FunctionBase.LockKey key)
        {

            bool didLock = currentLockHash == Lockfreehash;
            if (didLock)
            {
                currentLockHash = key.lockKey;
                currentLockName = key.PreatyName;
            }
            return didLock;

        }
        public bool UnLock(FunctionBase.LockKey key)
        {
            bool didUnLock = currentLockHash == key.lockKey;
            if (didUnLock)
            {
                currentLockHash = Lockfreehash;
                currentLockName = LockfreeName;
            }
            return didUnLock;

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

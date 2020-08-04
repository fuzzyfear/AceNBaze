using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Base lock
/// </summary>
public abstract class Lock: MonoBehaviour
{
    protected static string LockfreeName = "free";
    protected static int    Lockfreehash = 1294909896;

    protected string _lockName;
    protected int    _lockHash;

    /// <summary>
    /// Used to chow that the action is currently 
    /// being used by some one but it is not 
    /// hard locked by them
    /// </summary>
    [SerializeField] protected bool     _softLock;
#if UNITY_EDITOR
    [SerializeField] protected string[] _SoftLocks; //most for debug
    public string[] softLocks => _SoftLocks;
#else
   [SerializeField] protected int _SoftLocks = 0; 
#endif


    [SerializeField] protected string currentLockName;
    [SerializeField] protected int    currentLockHash;

    public string LockName => _lockName;
    public int    LockHash => _lockHash;

    public string CurrentLockName => currentLockName;
    public int    CurrentLockHash => currentLockHash;

    protected int HashKey(string key) { return Animator.StringToHash(key); }

    public Lock(string lockName)
    {
        _lockName = lockName;
        _lockHash = Animator.StringToHash(lockName);

        currentLockName = LockfreeName;
        currentLockHash = Lockfreehash;

        _softLock = false;
#if UNITY_EDITOR
        _SoftLocks = new string[0];
#else
       _SoftLocks = 0; 
#endif

    }


    public bool ControllKey(string keyName){ return currentLockHash == HashKey(keyName);    }
    public bool ControllKey(int keyHash)   { return currentLockHash == keyHash;             }
    public bool ControllKey()              { return currentLockHash == Lockfreehash;        }

    //==================================================================================================
    // Functions to lock and unlock 
    //==================================================================================================



    #region SoftLock

    public bool SoftLock() { return _softLock; }
#if UNITY_EDITOR
    public void SoftLock(string keyName)
    {

        List<string> names = _SoftLocks.ToList();
        int index = names.IndexOf(keyName);
        if (index != -1)
            names.RemoveAt(index);

        names.Add(keyName);
        _SoftLocks = names.ToArray();
        _softLock = true;
    }
    public void SofUntLock(string keyName)
    {

        List<string> names = _SoftLocks.ToList();
        names.Remove(keyName);
        _SoftLocks = names.ToArray();
        _softLock = _SoftLocks.Length > 0;
    }
#else
    public void SoftLock(string keyName)
    {
        _SoftLocks += 1;
        _softLock = true;
    }
    public void SofUntLock(string keyName)
    {
        _SoftLocks -= 1;
        _softLock = _SoftLocks > 0;
    }
#endif
    #endregion

    #region Owner controlls
    public bool OwneLock(string keyName) { return ControllKey(keyName); }
    public bool OwneLock(int    KeyHash) { return ControllKey(KeyHash); }

    public bool OwneLockOrFree(string keyName) { return ControllKey() || ControllKey(keyName); }
    public bool OwneLockOrFree(int    KeyHash) { return ControllKey() || ControllKey(KeyHash); }
    #endregion

    /// <summary>
    /// Controlls if the key holder ownes the lock, attempts to lock it if not
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the key holder owns or succeded in claming the lock</returns>
    public bool OwnesOrLock(string keyName) { return (ControllKey(keyName)) ? true : LockAction(keyName); }
    /// <summary>
    /// Controlls if the key holder ownes the lock, attempts to lock it if not
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the key holder owns or succeded in claming the lock</returns>
    public bool OwnesOrLock(int keyHash) { return (ControllKey(keyHash)) ? true : LockAction(keyHash); }

    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(string keyName)
    {
        bool loked = ControllKey();  

        if (loked)
        {
            currentLockName = keyName;
            currentLockHash = HashKey(keyName);
        }
         

        return loked;
    }
    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyHash"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(int keyHash)
    {
        bool loked = ControllKey();

        if (loked)
        {
            currentLockName = keyHash.ToString();
            currentLockHash = keyHash;
        }

        return loked;
    }

    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(string keyName)
    {
        bool loked = ControllKey(keyName);


        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }
    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyHash"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(int keyHash)
    {
        bool loked = ControllKey(keyHash);
        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }




    /// <summary>
    /// To get the types for the debug, so the can be displayed in the editpr
    /// </summary>
    /// <returns></returns>
    public abstract string[] GetTypes();

}



/// <summary>
/// Lock that stors function what taks 1 parameter
/// </summary>
/// <typeparam name="DataType">Type of the parameter</typeparam>
public class Lock<DataType> : Lock
{

    //private _ActionBase action;
    public delegate void baseAction(CharacterBaseAbilitys character, DataType args);

    private baseAction action;


    public Lock(string lockName, baseAction action):base(lockName)
    {
        this.action = action;
    }


    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction(CharacterBaseAbilitys Character, DataType input, string keyname)
    {
        bool couldDoAction = ControllKey(keyname) || ControllKey();
        if(couldDoAction)
            action(Character, input);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction(CharacterBaseAbilitys Character, DataType input, int keyhash)
    {
        bool couldDoAction = ControllKey(keyhash) || ControllKey();
        if (couldDoAction)
            action(Character, input);

        return couldDoAction;
    }

    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction(CharacterBaseAbilitys Character, DataType input, string keyname)
    {
        bool couldDoAction = LockAction(keyname) || ControllKey(keyname);
        if (couldDoAction)
            action(Character, input);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction(CharacterBaseAbilitys Character, DataType input, int keyhash)
    {
        bool couldDoAction = LockAction(keyhash) || ControllKey(keyhash);
        if (couldDoAction)
            action(Character, input);

        return couldDoAction;
    }




    public override string[] GetTypes()
    {
        string[] datatypes = { typeof(DataType).Name };

        return datatypes;
    }
}



/// <summary>
/// Lock that stors function what taks 2 parameter (example: function that changes value in animator)
/// </summary>
/// <typeparam name="DataType1">Type of the parameter 1</typeparam>
/// <typeparam name="DataType2">Type of the parameter 2</typeparam>
public class Lock<DataType1, DataType2> : Lock
{

    //private _ActionBase action;
    public delegate void baseAction(CharacterBaseAbilitys character, DataType1 args1, DataType2 args2);

    private baseAction action;


    public Lock(string lockName, baseAction action) : base(lockName)
    {
        this.action = action;
    }

    
    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction(CharacterBaseAbilitys Character, DataType1 input1, DataType2 input2, string keyname)
    {
        bool couldDoAction = ControllKey() || ControllKey(keyname);
        if (couldDoAction)
            action(Character, input1, input2);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAction(CharacterBaseAbilitys Character, DataType1 input1, DataType2 input2, int keyhash)
    {
        bool couldDoAction = ControllKey() || ControllKey(keyhash);
        if (couldDoAction)
            action(Character, input1, input2);

        return couldDoAction;
    }

    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyname"  > the name of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction(CharacterBaseAbilitys Character, DataType1 input1, DataType2 input2, string keyname)
    {
        bool couldDoAction = LockAction(keyname) || ControllKey(keyname);
        if (couldDoAction)
            action(Character, input1, input2);

        return couldDoAction;
    }
    /// <summary>
    /// Use the action that if its aviable and locks it 
    /// </summary>
    /// <typeparam name="T"    >  Type of the input</typeparam>
    /// <param name="Character"> the character stats that will be modifyed</param>
    /// <param name="input"    > variable that will be used during the modification (ex new target position to walk to)</param>
    /// <param name="keyhash"> the key (hase of namen) of the funktion that wants to use the action</param>
    /// <returns> true if it could use the action fals other wise</returns>
    public bool UseAndLockAction(CharacterBaseAbilitys Character, DataType1 input1, DataType2 input2, int keyhash)
    {
        bool couldDoAction = LockAction(keyhash) || ControllKey(keyhash);
        if (couldDoAction)
            action(Character, input1, input2);

        return couldDoAction;
    }


    public override string[] GetTypes()
    {
        string[] datatypes = { typeof(DataType1).Name, typeof(DataType2).Name };

        return datatypes;
    }


}
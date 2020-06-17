using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// lås som lagra en grund funktion som behöver en variablar 
/// </summary>
/// <typeparam name="DataType"> vad funktion tar som parameter värde </typeparam>
public class Lock<DataType> : MonoBehaviour
{

    static string LockfreeName = "free";
    static int    Lockfreehash = 1294909896;

    public string LockName { get; }
    public int    LockHash { get; }


    //private _ActionBase action;
    public delegate void baseAction(CharacterBaseAbilitys character, DataType args);

    private baseAction action;


    private string currentLockName; //most for debug
    private int    currentLockHash;

    public Lock(string name, baseAction action)
    {
        LockName = name;
        LockHash = Animator.StringToHash(name);

        currentLockName = LockfreeName;
        currentLockHash = Lockfreehash;

        this.action = action;
    }



    public bool ControllKey(string keyName){ return currentLockHash == Animator.StringToHash(keyName);    }
    public bool ControllKey(int keyHash)   { return currentLockHash == keyHash;                           }
    public bool ControllKey()              { return currentLockHash == Lockfreehash;                      }


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
        bool couldDoAction = ControllKey() || ControllKey(keyname);
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
        bool couldDoAction = ControllKey() || ControllKey(keyhash);
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




//==================================================================================================
// Functions to lock and unlock 
//==================================================================================================

    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(string keyName)
    {
        bool loked = ControllKey(keyName);

        if(loked)
            currentLockHash = Animator.StringToHash(keyName);

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
            currentLockHash = keyHash;

        return loked;
    }

    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(string keyName)
    {
        bool loked = ControllKey(name);  
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
        bool loked = (currentLockHash == keyHash);
        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }


}



/// <summary>
///  lås som lagra en grund funktion som behöver två variablar (ex ändra animatior variable)
/// </summary>
/// <typeparam name="DataType1">vad funktion tar som parameter första värde</typeparam>
/// <typeparam name="DataType2">vad funktion tar som parameter andra värde</typeparam>
public class Lock<DataType1, DataType2> : MonoBehaviour
{

    static string LockfreeName = "free";
    static int    Lockfreehash = 1294909896;

    public string LockName { get; }
    public int    LockHash { get; }


    //private _ActionBase action;
    public delegate void baseAction(CharacterBaseAbilitys character, DataType1 args1, DataType2 args2);

    private baseAction action;


    private string currentLockName; //most for debug
    private int    currentLockHash;

    public Lock(string name, baseAction action)
    {
        LockName = name;
        LockHash = Animator.StringToHash(name);

        currentLockName = LockfreeName;
        currentLockHash = Lockfreehash;

        this.action = action;
    }



    public bool ControllKey(string keyName) { return currentLockHash == Animator.StringToHash(keyName); }
    public bool ControllKey(int keyHash)    { return currentLockHash == keyHash; }
    public bool ControllKey()               { return currentLockHash == Lockfreehash; }


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




    //==================================================================================================
    // Functions to lock and unlock 
    //==================================================================================================

    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(string keyName)
    {
        bool loked = ControllKey(keyName);

        if (loked)
            currentLockHash = Animator.StringToHash(keyName);

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
            currentLockHash = keyHash;

        return loked;
    }

    /// <summary>
    /// Unlocks the action
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>True if current owner unlocks it, fals if any one else trys to unlock it</returns>
    public bool UnLockAction(string keyName)
    {
        bool loked = ControllKey(name);
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
        bool loked = (currentLockHash == keyHash);
        if (loked)
        {
            currentLockName = LockfreeName;
            currentLockHash = Lockfreehash;
        }
        return loked;
    }


}
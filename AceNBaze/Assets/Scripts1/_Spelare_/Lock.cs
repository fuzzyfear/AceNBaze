using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    static string LockfreeName = "free";
    static int    Lockfreehash = 1294909896;

    public string LockName { get; }
    public int    LockHash { get; }


    private _ActionBase action;

    private string currentLockName; //most for debug
    private int    currentLockHash;

    public Lock(string name, _ActionBase action)
    {
        LockName = name;
        LockHash = Animator.StringToHash(name);

        currentLockName = LockfreeName;
        currentLockHash = Lockfreehash;
        this.action     = action;
    }
    //TODO: fixa klart denna 

    public bool UseAction<T>(CharacterBaseAbilitys Character, T input, string keyname)
    {

        action.ActionFunction<T>(Character, input);
        return true;
    }

    public bool UseAndLockAction()
    {
        return true;
    }



    /// <summary>
    /// Locks the action 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns>true if the action could be claimed, fals if it already loked</returns>
    public bool LockAction(string keyName)
    {
        bool loked = (currentLockHash == Lockfreehash);

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
        bool loked = (currentLockHash == Lockfreehash);

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
        bool loked = (currentLockHash == Animator.StringToHash(keyName));
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

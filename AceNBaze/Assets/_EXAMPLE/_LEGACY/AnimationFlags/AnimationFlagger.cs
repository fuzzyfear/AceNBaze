using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlagger : MonoBehaviour
{

    /// <summary>
    /// actions that can be don on a flagg
    /// </summary>
    public enum FlagActions  {LOCK, UNLOCK, SET };

    #region Flag structure
    protected static string LockfreeName = "free";
    protected static int    Lockfreehash = 1294909896;


    [System.Serializable]
    public struct AnimationFlag
    {
        private int HashKey(string keyName) { return Animator.StringToHash(keyName); }


        [SerializeField] private string _flagName;
        [SerializeField] private bool   _flag;
        [SerializeField] private string _currentLockName; //most for debug
        [SerializeField] private int    _currentLockHash;



        public bool flag => _flag;

        public string currentLockName => _currentLockName;
        public int    currentLockHash => _currentLockHash;



        public AnimationFlag(string flagName)
        {
            _flagName        = flagName;
            _flag            = false;
            _currentLockName = LockfreeName;
            _currentLockHash = Lockfreehash;

        }

        private bool ControllKey(string keyName) { return currentLockHash == HashKey(keyName); }
        private bool ControllKey(int keyHash)    { return currentLockHash == keyHash;                        }
        private bool ControllKey()               { return currentLockHash == Lockfreehash;                   }


       

        #region Owner controlls
        private bool OwneFlag(string keyName)       { return ControllKey(keyName); }
        private bool OwneFlag(int KeyHash)          { return ControllKey(KeyHash); }

        private bool OwneFlagOrFree(string keyName) { return ControllKey() || ControllKey(keyName); }
        private bool OwneFlagOrFree(int KeyHash)    { return ControllKey() || ControllKey(KeyHash); }
        #endregion


        #region Lock managmant

        /// <summary>
        /// Locks the flag so only the class with the key can cuse it. Returns true if the flag could be locked or if it allredy is lockde by the key
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns>true if the flag could be locked or if it allredy is lockde by the key</returns>
        public bool LockFlag(string keyName)
        {
            bool succses = ControllKey();
            if (succses)
            {
                _currentLockName = keyName;
                _currentLockHash = HashKey(keyName);
            }
            else
            {
                succses = ControllKey(keyName);
            }
     

            return succses;
        }
        /// <summary>
        /// Locks the flag so only the class with the key can cuse it 
        /// </summary>
        /// <param name="keyHash"></param>
        /// <returns>true if the flag could be locked or if it allredy is lockde by the key</returns>
        public bool LockFlag(int keyHash)
        {
            bool succses = ControllKey();
            if (succses)
            {
                _currentLockName = keyHash.ToString();
                _currentLockHash = keyHash;
            }
            else
            {
                succses = ControllKey(keyHash);
            }
            return succses;
        }


        /// <summary>
        /// Locks the flag so only the class with the key can cuse it 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns>true if the flag could be unlocked or if it allredy is lockde by the key</returns>
        public bool UnLockFlag(string keyName)
        {
            bool succses = ControllKey(keyName);
            if (succses)
            {
                _currentLockName = LockfreeName;
                _currentLockHash = Lockfreehash;
            }
            return succses;
        }
        /// <summary>
        /// Locks the flag so only the class with the key can cuse it 
        /// </summary>
        /// <param name="keyHash"></param>
        /// <returns>true if the flag could be locked or if it allredy is lockde by the key</returns>
        public bool UnLockFlag(int keyHash)
        {
            bool succses = ControllKey(keyHash);
            if (succses)
            {
                _currentLockName = LockfreeName;
                _currentLockHash = Lockfreehash;
            }
            return succses;
        }


        #endregion




        /// <summary>
        /// Sets the flags value if the flag is free or if it is owned by the key
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="flagValue"></param>
        /// <returns>true if the flag was set</returns>
        public bool SetFlag(string keyName, bool flagValue)
        {
            bool succses = OwneFlagOrFree(keyName);
            if (succses)
            {
                _flag            = flagValue;
            }
            return succses;
        }
        /// <summary>
        /// Sets the flags value if the flag is free or if it is owned by the key
        /// </summary>
        /// <param name="keyHash"></param>
        /// <param name="flagValue"></param>
        /// <returns>true if the flag was set</returns>
        public bool SetFlag(int keyHash, bool flagValue)
        {
            bool succses = OwneFlagOrFree(keyHash);
            if (succses)
            {
                _flag            = flagValue;
            }
            return succses;
        }


        /// <summary>
        /// Should only be caled by animatoioner, do not call this class 
        /// from script!
        /// </summary>
        public void TriggerFlag() { _flag = true; }
        

    }
    #endregion


    private Dictionary<string, int> _flagIndexDictionary;

    [SerializeField] private AnimationFlag[] _flags;


    private void Awake()
    {
        _flagIndexDictionary = new Dictionary<string, int>();

    }


 


    /// <summary>
    /// Modifas a value on a flagg, will return true if it was abble to preform the action
    /// </summary>
    /// <param name="flagName">Flag that will be modified [aviable flags is: F1]</param>
    /// <param name="action"></param>
    /// <returns>true it the action could be preformed and was succes full</returns>
    public bool ActionOnFlag(string flagName, FlagActions action)
    {

        bool succes = false;
        int flag;
        if (_flagIndexDictionary.TryGetValue(flagName ,out flag))
        {
            switch (action)
            {
                case FlagActions.LOCK:
                    succes = _flags[flag].LockFlag(flagName);
                break;
                case FlagActions.UNLOCK:
                    succes = _flags[flag].UnLockFlag(flagName);
                    break;
                case FlagActions.SET:
                    succes = _flags[flag].SetFlag(flagName,false);
                    break;
                default:
                    Debug.LogError("tried do the action " + action + " whitch dosent exist");
                    break;
            }

          
        }
        else
        {
            Debug.LogError("tried to read flag " + flagName + " whitch dosent exist");
        }
        return succes;

    }

    /// <summary>
    /// returns the value of the flag, returns true if the flag has been triggerd 
    /// </summary>
    /// <param name="flag"> list of avialbe triggers: F1</param>
    /// <returns>value of the flag (true => the animation has triggerd it)</returns>
    public bool ControllFlag(string flag)
    {


        return _flags[_flagIndexDictionary[flag]].flag;
    }


    /// <summary>
    /// Only to be called by animations, do not call from script
    /// </summary>
    /// <param name="flagName">the flag that shuld be triggerd</param>
    public void TriggerFlag(string flagName)
    {
        int flag;
        if (_flagIndexDictionary.TryGetValue(flagName, out flag))
        {
            _flags[flag].TriggerFlag();
        }
        else
        {
            Debug.LogError("Attemted to trigger flag " + flagName + " whitch dosent exist");
        }
    }
}

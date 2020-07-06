using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlagger : MonoBehaviour
{
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
        public bool OwneFlag(string keyName)       { return ControllKey(keyName); }
        public bool OwneFlag(int KeyHash)          { return ControllKey(KeyHash); }

        public bool OwneFlagOrFree(string keyName) { return ControllKey() || ControllKey(keyName); }
        public bool OwneFlagOrFree(int KeyHash)    { return ControllKey() || ControllKey(KeyHash); }
        #endregion




        /// <summary>
        /// Locks the flag so only the class with the key can cuse it 
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
        /// <returns>true if the flag could be locked or if it allredy is lockde by the key</returns>
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


    }
    #endregion




    [SerializeField] private Dictionary<string, AnimationFlag> flags;


    public AnimationFlag flag = new AnimationFlag("actionFlag");



    /// <summary>
    /// Used by the animator to trigger the flag
    /// </summary>
    public void TriggerFlag()
    {


    }

}

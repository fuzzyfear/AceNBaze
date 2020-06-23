using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlagger : MonoBehaviour
{
    protected static string LockfreeName = "free";
    protected static int    Lockfreehash = 1294909896;



    //TODO: bygg ut det här senare efter attack och gågn är klart
    [System.Serializable]
    public struct AnimationFlag
    {

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

        private bool ControllKey(string keyName) { return currentLockHash == Animator.StringToHash(keyName); }
        private bool ControllKey(int keyHash)    { return currentLockHash == keyHash;                        }
        private bool ControllKey()               { return currentLockHash == Lockfreehash;                   }

        private int HashKey(string keyName) { return Animator.StringToHash(keyName); }
       
        /// <summary>
        /// Locks the flag so only the class with the key can cuse it 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns>true if the flag could be locked</returns>
        public bool LockFlag(string keyName)
        {
            bool succses = ControllKey(keyName) || ControllKey();
            if (succses)
            {
                _currentLockName = keyName;
                _currentLockHash = HashKey(keyName);
            }
            return succses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyHash"></param>
        /// <returns></returns>
        public bool LockFlag(int keyHash)
        {
            bool succses = ControllKey(keyHash) || ControllKey();
            if (succses)
            {
                _currentLockName = keyHash.ToString();
                _currentLockHash = keyHash;
            }
            return succses;
        }

        public bool LockFlag(string keyName, bool flagValue)
        {
            bool succses = ControllKey(keyName) || ControllKey();
            if (succses)
            {
                _currentLockName = keyName;
                _currentLockHash = HashKey(keyName);
                _flag            = flagValue;
            }
            return succses;
        }
        public bool LockFlag(int keyHash, bool flagValue)
        {
            bool succses = ControllKey(keyHash) || ControllKey();
            if (succses)
            {
                _currentLockName = keyHash.ToString();
                _currentLockHash = keyHash;
                _flag            = flagValue;
            }
            return succses;
        }



    }





    public AnimationFlag flag = new AnimationFlag("actionFlag");




    public void TriggerFlag() { }

}

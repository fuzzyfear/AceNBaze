using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FunctionBase : MonoBehaviour
{
    /// <summary>
    /// Struckt som används av functioner för att
    /// låsa specifika funktioner så som förmågan 
    /// att ändra rörelse destination
    /// </summary>
    public readonly struct LockKey
    {
    
        public LockKey(string key)
        {
            PreatyName = key;
            lockKey    = Animator.StringToHash(key);
        }

        public string PreatyName { get; }
        public int    lockKey { get; }

    }

    private LockKey _lockKey;
    protected FunctionBase() { _lockKey = new LockKey(this.name); }


    public abstract void Tick(CharacterInfo stats, ref FunctionTick.flags locks);




   


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
    public int HP;
    [HideInInspector] public int currentHP; //Decreptent remove later
    public int dmg;//Decreptent remove later (moved to the weapon)
    [Tooltip("mesured in unity meters/second")] public int movementSpeed;
    public int attackSpeed; //Decreptent remove later
    [Tooltip("mesured in seconds")]             public float attackColldown;
    [Tooltip("mesured in unity meters")]        public float attackRange;
	public int dashCooldown;//Decreptent remove later
    [Tooltip("mesured in seconds")]             public float staminaColldown;
                                                public float staminaBaseMax;

    [Tooltip("mesured inseconds")]              public float baseDashTime;
    [Tooltip("mesured in unity meters/second")] public float bashDashSpeed;
}

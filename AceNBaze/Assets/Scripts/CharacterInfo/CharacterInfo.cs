using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
    public int HP;
    [HideInInspector] public int currentHP; //Decreptent remove later

    public int dmg;//Decreptent remove later (moved to the weapon)
    public int attackSpeed; //Decreptent remove later
    public int dashCooldown;//Decreptent remove later

    [Tooltip("mesured in unity meters/second")] public int movementSpeed;

    [Tooltip("mesured inseconds")]              public float baseDashTime;
    [Tooltip("mesured in unity meters/second")] public float bashDashSpeed;




    [Tooltip("mesured in seconds")]             public float attackColldown;
    [Tooltip("mesured in unity meters")]        public float attackRange;

    [Tooltip("mesured in seconds")]             public float staminaColldown;
                                                public float staminaBaseMax;


}

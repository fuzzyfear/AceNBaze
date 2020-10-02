using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
    public int healthPoints;
    
    public int dmg;
    public int attackSpeed;
    public float attackRange;
    
    public int runningSpeed;
    public int walkingSpeed;
    
    public int dashCooldown;
    public float baseDashTime;
    public float bashDashSpeed;

    public float visisonDistNeutral;
    public float visionDistChase;

    public float FOWNeutral;
    public float FOWChase;

    public float stunDuration;

    //Delete?
    public float attackColldown;
    public float staminaColldown;
    public float staminaBaseMax;


}

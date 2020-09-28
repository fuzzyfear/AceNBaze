using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
    public int healthPoints;
    public int dmg;
    public int attackSpeed;
    public int dashCooldown;
    public int runningSpeed;
    public int walkingSpeed;
    public float baseDashTime;
    public float bashDashSpeed;
    public float attackColldown;
    public float attackRange;
    public float staminaColldown;
    public float staminaBaseMax;
}

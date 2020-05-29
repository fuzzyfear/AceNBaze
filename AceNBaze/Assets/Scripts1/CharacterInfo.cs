using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterInfo : ScriptableObject
{
	public int HP;
	[HideInInspector] public int currentHP;
	public int dmg;
	public int movementSpeed;
	public int attackSpeed;
	public float attackRange;
	public int dashCooldown;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple attack, attacks emidiatly then attack button is pressed.
/// </summary>
public class F_DirectAttack : _FunctionBase
{
    public F_DirectAttack() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.attack))
        {

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
           
                Vector3 mouse = Input.mousePosition;
                Ray        castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {
                    float dist = Vector3.Distance(baseAbilitys.agent.transform.position, hit.collider.transform.root.position);

                    if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                    {

               
                        CharacterBaseAbilitys targetAbilitis = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                        if (targetAbilitis == null)
                            Debug.LogError(" the top rot of target dosent have funktion ticker");

                        if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon, _keyHash))
                            Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
                        else
                            Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");

                    }
                    else
                    {
                        Debug.Log("Miss, enemy not in range " + dist);
                    }
                    modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
                    //StartCoroutine(WaitForAttackSpeed(baseAbilitys, modifier));
                }
            }
        }

    }





}
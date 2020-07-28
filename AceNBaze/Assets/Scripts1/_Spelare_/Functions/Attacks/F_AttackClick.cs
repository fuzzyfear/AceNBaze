using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Simple click to attack if in range.
/// </summary>
public class F_AttackClick : _FunctionBase
{
    public F_AttackClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.attack))
        {

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                Vector3    mouse     = Input.mousePosition;
                Ray        castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                CharacterBaseAbilitys targetAbilitis    = null;
                LockManager           targetLockManager = null;
             
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {
                    targetAbilitis    = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                    targetLockManager = hit.transform.root.GetChild(FunctionTick.LockManagerChildIndex).GetComponent<LockManager>();
                }
                

                //Dos the attack, "swings the weapon"
                PreformAttack(baseAbilitys, modifier, targetAbilitis, targetLockManager);

            }
        }
       
    }



    private void PreformAttack(CharacterBaseAbilitys baseAbilitys, Modifier attackerModifier, CharacterBaseAbilitys targetAbilitis = null, LockManager targetLockManager = null)
    {
        attackerModifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
        if(targetAbilitis != null)
        {
            float dist = Vector3.Distance(baseAbilitys.agent.transform.position, targetAbilitis.mainTransform.position);

            if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
            {

                StopMovment(baseAbilitys, attackerModifier.lockManager);

                //  if (targetAbilitis == null) { Debug.LogError(" the top rot of target dosent have funktion ticker"); }


                float damage = CalcDamage(baseAbilitys, attackerModifier, targetAbilitis);

                




               // if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon, _keyHash))
               //     Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
               // else
               //     Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");

            }
            else
            {
                Debug.Log("Miss, enemy not in range " + dist);
            }

        }
    }




    private float CalcDamage(CharacterBaseAbilitys baseAbilitys, Modifier modifier, CharacterBaseAbilitys targetAbilitis)
    {

        float damage = 0;

        Vector3 targetLookingDir = targetAbilitis.mainTransform.forward;
        Vector3 playerLookingDir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);


        float[] targetParryData  = modifier.commonFunctionMethods.GetCharacterDirectionData(targetLookingDir);
        float[] playerAttackData = modifier.commonFunctionMethods.GetCharacterDirectionData(targetLookingDir);

        float[] damageData = new float[8];

        if (targetAbilitis.characterStats.cWstats.parry)
        {
            damageData = modifier.commonFunctionMethods.ParryAttack(targetParryData, targetAbilitis.characterStats.cStats.weapon.parryStrengh,
                                                                      playerAttackData, baseAbilitys.characterStats.cStats.weapon.weaponDamage);
        }
        else
        {
            float wDamage = baseAbilitys.characterStats.cStats.weapon.weaponDamage;
            for (int i = 0; i < 8; ++i)
                damageData[i] = playerAttackData[i] * wDamage;
        }

        for (int i = 0; i < 8; ++i)
            damage += damageData[i];

        return damage;

    }




    /// <summary>
    /// Stops the movment of the player 
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <param name="modifier"></param>
    private void StopMovment(CharacterBaseAbilitys baseAbilitys, LockManager modifier)
    {

#region Lock SetAgentIsStopped
        bool locked;
#if UNITY_EDITOR
        locked = modifier.SetAgentIsStopped.LockAction(_keyName);
#else
        locked = modifier.SetAgentIsStopped.LockAction(_keyHash);
#endif
#endregion
        if (locked)
        {
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, true, _keyHash);
           
#region Lock SetAgentMovingDestination
#if UNITY_EDITOR
            locked = modifier.SetAgentMovingDestination.LockAction(_keyName);
#else
            locked = modifier.SetAgentMovingDestination.LockAction(_keyHash);
#endif
#endregion
            if (locked)
            {
                modifier.SetAgentMovingDestination.UseAction(baseAbilitys, baseAbilitys.mainTransform.position, _keyHash);
                modifier.SetAgentMovingDestination.UnLockAction(_keyHash);
            }
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, false, _keyHash);
            modifier.SetAgentIsStopped.UnLockAction(_keyHash);


        }
    }


}

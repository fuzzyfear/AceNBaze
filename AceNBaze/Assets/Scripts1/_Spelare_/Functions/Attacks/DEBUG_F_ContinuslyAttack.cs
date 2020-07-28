using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_F_ContinuslyAttack : _FunctionBase
{
  public DEBUG_F_ContinuslyAttack() : base() { }

    bool test = false;
    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
  
        //BEFOR_MERGE: remove this 
        baseAbilitys.characterStats.cWstats.DEBUG_attacking = false;
 
        if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {

     
            Ray castPoint = new Ray(baseAbilitys.mainTransform.position, baseAbilitys.transform.forward);

            RaycastHit hit;

            CharacterBaseAbilitys targetAbilitis = null;
            LockManager targetLockManager = null;

            if (Physics.SphereCast(castPoint,1, out hit, baseAbilitys.characterStats.cStats.weapon.weaponRange, baseAbilitys.maskes.EnemyMask))
            {
                targetAbilitis    = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                targetLockManager = hit.transform.root.GetChild(FunctionTick.LockManagerChildIndex).GetComponent<LockManager>();
            }


            //Dos the attack, "swings the weapon"
            PreformAttack(baseAbilitys, modifier, targetAbilitis, targetLockManager);

            //BEFOR_MERGE: remove this 
            baseAbilitys.characterStats.cWstats.DEBUG_attacking = true;

        }

      
    }


    private void PreformAttack(CharacterBaseAbilitys baseAbilitys, Modifier attackerModifier, CharacterBaseAbilitys targetAbilitis = null, LockManager targetLockManager = null)
    {
        attackerModifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);


        if (targetAbilitis != null)
        {

            //StopMovment(baseAbilitys, attackerModifier.lockManager);

            float damage = CalcDamage(baseAbilitys, attackerModifier, targetAbilitis);
            //applays the damage to the target
            targetLockManager.ApplayDamage.UseAction(targetAbilitis, damage, _keyHash);


        }
    }
    


    private float CalcDamage(CharacterBaseAbilitys baseAbilitys, Modifier modifier, CharacterBaseAbilitys targetAbilitis)
    {
        float damage = 0;

        Vector3 targetLookingDir = targetAbilitis.mainTransform.forward;
        Vector3 playerLookingDir = baseAbilitys.mainTransform.forward; //modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);


        float[] targetParryData = modifier.commonFunctionMethods.GetCharacterDirectionData(targetLookingDir);
        #region get the attack data
        float[] tempAttackData = modifier.commonFunctionMethods.GetCharacterDirectionData(playerLookingDir);
        float[] playerAttackData = new float[tempAttackData.Length];
        playerAttackData[(int)tempAttackData[8]] = tempAttackData[(int)tempAttackData[8]];
        #endregion





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
}

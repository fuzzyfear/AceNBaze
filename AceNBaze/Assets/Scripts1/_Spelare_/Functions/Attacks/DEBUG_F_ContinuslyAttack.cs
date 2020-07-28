using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_F_ContinuslyAttack : _FunctionBase
{
  public DEBUG_F_ContinuslyAttack() : base() { }

    bool test = false;
    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
  

 
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

 

        }

      
    }


    private void PreformAttack(CharacterBaseAbilitys baseAbilitys, Modifier attackerModifier, CharacterBaseAbilitys targetAbilitis = null, LockManager targetLockManager = null)
    {
        attackerModifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);

        if (targetAbilitis != null)
        {

            float damage = attackerModifier.commonFunctionMethods.CalcDamage(baseAbilitys, targetAbilitis);
            //applays the damage to the target
            targetLockManager.ApplayDamage.UseAction(targetAbilitis, damage, _keyHash);


        }
    }
    


   
}

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


        //BEFOR_MERGE: remove this 
        baseAbilitys.characterStats.cWstats.DEBUG_attacking = false;

        //if (Input.GetKeyDown(Controlls.instanse.attack))
        //{

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {

                RaycastHit hit;

                Ray castPoint = new Ray(baseAbilitys.mainTransform.position, baseAbilitys.transform.forward);

                CharacterBaseAbilitys targetAbilitis    = null;
                LockManager           targetLockManager = null;
             
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
        //}
       
    }



    private void PreformAttack(CharacterBaseAbilitys baseAbilitys, Modifier attackerModifier, CharacterBaseAbilitys targetAbilitis = null, LockManager targetLockManager = null)
    {
        attackerModifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
        if(targetAbilitis != null)
        {
          
                //StopMovment(baseAbilitys, attackerModifier.lockManager);

                float damage = attackerModifier.commonFunctionMethods.CalcDamage(baseAbilitys, targetAbilitis);
                //applays the damage to the target
                targetLockManager.ApplayDamage.UseAction(targetAbilitis, damage, _keyHash);


        }
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

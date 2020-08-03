using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MoveToTargetAutoAttack : _FunctionBase
{

    [SerializeField] CharacterBaseAbilitys targetAbilitis;

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        #region Get attack Target
        if (modifier.controller.Attack_1_Click())
        {

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                Vector3    mouse     = Input.mousePosition;
                Ray        castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {
                    targetAbilitis = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                }
            }
        }
        else if (modifier.controller.AnyInput_Click())
        {

            targetAbilitis = null;
        }
        #endregion


        if (targetAbilitis != null)
        {
           if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                float dist = Vector3.Distance(baseAbilitys.agent.transform.position, targetAbilitis.transform.root.position);


                #region In Range
                if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                {
                    StopMovment(baseAbilitys, modifier.lockManager);

                    if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon.weaponDamage, _keyHash))
                        Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
                    else
                    {
                        Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");
                 


                        modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
                    }
                        
                }
                #endregion
                #region Not in Range
                else
                {
                    #region Locks SetAgentMovingDestination
                    bool locked;
#if UNITY_EDITOR
                    locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName);
#else
                    locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyHash);
#endif
                    #endregion
                    if (locked)
                        {
                            modifier.lockManager.SetAgentMovingDestination.UseAction(baseAbilitys, targetAbilitis.transform.root.position, _keyHash);
                            modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyName);
                        }
                    
                }
                #endregion
            }
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

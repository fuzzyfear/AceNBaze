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

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {
                    float dist  = Vector3.Distance(baseAbilitys.agent.transform.position, hit.collider.transform.root.position);

                    if(dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                    {

                        StopMovment(baseAbilitys, modifier.lockManager);

                        CharacterBaseAbilitys targetAbilitis = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                        if(targetAbilitis == null)
                            Debug.LogError(" the top rot of target dosent have funktion ticker");

                        if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon , _keyHash))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary>
/// Simple click attack that continus to attack 
/// until other orders is given
/// </summary>
public class F_AutoAttackClick : _FunctionBase
{
    [SerializeField] CharacterBaseAbilitys targetAbilitis;


    public F_AutoAttackClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        #region attack
        if (Input.GetKey(Controlls.instanse.attack))
        {

            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.EnemyMask))
                {
                    float dist = Vector3.Distance(baseAbilitys.agent.transform.position, hit.collider.transform.root.position);

                    if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                    {

                        StopMovment(baseAbilitys, modifier.lockManager);

                        targetAbilitis = hit.transform.root.GetChild(FunctionTick.CharackterAbilityChildIndex).GetComponent<CharacterBaseAbilitys>();
                        //if (targetAbilitis == null)
                        //    Debug.LogError(" the top rot of target dosent have funktion ticker");

                        //if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon, _keyHash))
                        //    Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
                        //else
                        //    Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");
                            
                    }
                    else
                    {
                        Debug.Log("Miss, enemy not in range " + dist);
                    }
                    modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
                    //  StartCoroutine(WaitForAttackSpeed(baseAbilitys, modifier));
                }
				else
				{
					Debug.Log("Miss, no target");
					modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
				}
			}
        }
        #endregion
        #region Cancel attack
        else if (Input.anyKeyDown)
        {
            targetAbilitis = null;
        }
        #endregion
        #region Auto attack
        else if (targetAbilitis != null)
        {
            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                    float dist = Vector3.Distance(baseAbilitys.agent.transform.position, targetAbilitis.transform.root.position);

                    if (dist <= baseAbilitys.characterStats.cStats.weapon.weaponRange)
                    {
                        if (!modifier.lockManager.ApplayDamage.UseAction(targetAbilitis, baseAbilitys.characterStats.cStats.weapon, _keyHash)) { }
                        //    Debug.Log("Could not applay damage, " + modifier.lockManager.ApplayDamage.CurrentLockName + " has locked the action");
                        //else
                        //    Debug.Log(targetAbilitis.transform.root.gameObject.name + " takes " + baseAbilitys.characterStats.cStats.weapon.weaponDamage + " dmg");
                    }
                    else
                    {
                        Debug.Log("Miss, enemy not in range " + dist);
                        targetAbilitis = null;
                    }
                    modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, 0, _keyHash);
                    //StartCoroutine(WaitForAttackSpeed(baseAbilitys, modifier));


            }
        }
        #endregion

    }

    //IEnumerator WaitForAttackSpeed(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    //{
    //    float colldown = 0f;
    //    modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);

    //    float colldownSpeed = baseAbilitys.characterStats.cStats.weapon.collDownSpeed;

    //    while (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
    //    {

    //        yield return new WaitForSeconds(colldownSpeed);
    //        colldown = Mathf.MoveTowards(colldown, 1f, 0.1f);//  Mathf.Clamp01(colldown + colldownSpeed);
    //        Debug.Log(colldown);
    //        modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);


    //    }

    //}


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

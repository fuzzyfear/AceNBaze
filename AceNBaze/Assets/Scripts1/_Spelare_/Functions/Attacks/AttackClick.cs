using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Simple click to move skript
/// </summary>
public class AttackClick : _FunctionBase
{


    // temparary move to setteing script lagter
    public enum attackScheme { HOLD, TOGGLE, CLICK }
    public attackScheme scheme = attackScheme.CLICK;
    [SerializeField] private bool attack = false;


    public AttackClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (shuldAttack())
        {
           
            if (baseAbilitys.characterStats.cStats.weapon.NotColldown)
            {
                Vector3    mouse = Input.mousePosition;
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
                    StartCoroutine(WaitForAttackSpeed(baseAbilitys, modifier));
                }
            }
        }
       
    }

    IEnumerator WaitForAttackSpeed(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        float colldown = 0f;
        modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);
      
        float colldownSpeed = baseAbilitys.characterStats.cStats.weapon.collDownSpeed;

        while (!baseAbilitys.characterStats.cStats.weapon.NotColldown)
        {

            colldown = Mathf.Clamp01(colldown+colldownSpeed);
            modifier.lockManager.SetAttackCollDown.UseAction(baseAbilitys, colldown, _keyHash);

            yield return new WaitForSeconds(1f);
        }

    }

    /// <summary>
    /// Stops the movment of the player 
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <param name="modifier"></param>
    private void StopMovment(CharacterBaseAbilitys baseAbilitys, LockManager modifier)
    {
       
        if (modifier.SetAgentIsStopped.LockAction(_keyName))
        {
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, true, _keyHash);
            if (modifier.SetAgentMovingDestination.LockAction(keyName: _keyName))
            {
                modifier.SetAgentMovingDestination.UseAction(baseAbilitys, baseAbilitys.mainTransform.position, _keyHash);
                modifier.SetAgentMovingDestination.UnLockAction(_keyHash);
            }
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, false, _keyHash);
            modifier.SetAgentIsStopped.UnLockAction(_keyHash);
        }
    }





    private bool shuldAttack()
    {

        switch (scheme)
        {
            case attackScheme.HOLD:
                attack = Input.GetKey(Controlls.instanse.attack);
                break;
            case attackScheme.TOGGLE:
                if (Input.GetKeyDown(Controlls.instanse.attack))
                    attack = !attack;
                break;
            case attackScheme.CLICK:
                attack = Input.GetKeyDown(Controlls.instanse.attack);
                break;
        }
        return attack;


    }
}

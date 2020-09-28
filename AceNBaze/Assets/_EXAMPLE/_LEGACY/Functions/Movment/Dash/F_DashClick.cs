using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that handles a simple dash
/// the dash only works as long ass you hovers above the ground, 
/// that should be fixed for later
/// </summary>
/// 
public class F_DashClick :_FunctionBase
{





    [SerializeField] private bool _Isdashing = false;
    private Coroutine dashing;

    public F_DashClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.dash) && !_Isdashing)
        {
            _Isdashing = true;

            if (dashing != null)
                StopCoroutine(dashing);
            dashing = StartCoroutine(Dashing(baseAbilitys, modifier));
        }
       
    }

    IEnumerator Dashing(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        #region Locks all agent related actions
        bool locked;
        //Locks all the movment actions
        do
        {
#if UNITY_EDITOR
            locked = (modifier.lockManager.SetAgentIsStopped.OwnesOrLock(_keyName)         &&
                      modifier.lockManager.SetAgentMovingDestination.OwnesOrLock(_keyName) &&
                      modifier.lockManager.SetAgentMovingSpeed.OwnesOrLock(_keyName)       &&
                      modifier.lockManager.SetAgentMove.OwnesOrLock(_keyName)              &&
                      modifier.lockManager.SetAgentUppdateRotation.OwnesOrLock(_keyName)   );
#else
            locked = (modifier.lockManager.SetAgentIsStopped.OwnesOrLock(_keyHash)         &&
                      modifier.lockManager.SetAgentMovingDestination.OwnesOrLock(_keyHash) &&
                      modifier.lockManager.SetAgentMovingSpeed.OwnesOrLock(_keyHash)       &&
                      modifier.lockManager.SetAgentMove.OwnesOrLock(_keyHash)              &&
                      modifier.lockManager.SetAgentUppdateRotation.OwnesOrLock(_keyHash)   );
#endif

            yield return locked;
        } while (!locked);
        #endregion

        modifier.lockManager.SetStamina.SoftLock(_keyName);


        #region Sets up agnet for dash
        modifier.lockManager.SetAgentMovingSpeed.UseAction(baseAbilitys, baseAbilitys.characterStats.cStats.dashSpeed, true, _keyHash);
        modifier.lockManager.SetAgentIsStopped.UseAction(baseAbilitys, true, _keyHash);
        modifier.lockManager.SetAgentUppdateRotation.UseAction(baseAbilitys,false, _keyHash);
        #endregion


        #region Get variabler from baseabilitys
        float stamina     = baseAbilitys.characterStats.cStats.staminaCurrent;
        float draineSpeed = baseAbilitys.characterStats.cStats.dashStaminaDraineSpeed;
        float dashspeed   = baseAbilitys.characterStats.cStats.dashSpeed;
        #endregion


        Vector3 dir = modifier.commonFunctionMethods.GetDirAgentToMouse(baseAbilitys);
     
        #region Locks transforms rotation Rotation
        do
        {
#if UNITY_EDITOR
            locked = modifier.lockManager.SetTransformRotationFromQuaternion.OwnesOrLock(_keyName) && 
                     modifier.lockManager.SetTransformRotationFromVector.OwnesOrLock(_keyName)     ;
#else
            locked = modifier.lockManager.SetTransformRotationFromQuaternion.OwnesOrLock(_keyHash) && 
                     modifier.lockManager.SetTransformRotationFromVector.OwnesOrLock(_keyHash)     ;
#endif
            yield return locked;
        } while (!locked);
        #endregion


        modifier.lockManager.SetTransformRotationFromVector.UseAction(baseAbilitys, dir,_keyHash);

        #region the dash

        modifier.lockManager.SetAgentMove.UseAction(baseAbilitys, dir  * Time.smoothDeltaTime, _keyHash);
        while (!Input.GetKeyDown(Controlls.instanse.dash) &&
               baseAbilitys.characterStats.cStats.staminaCurrent > 0)
        {
            stamina = Mathf.MoveTowards(stamina, 0, draineSpeed * Time.deltaTime);

            modifier.lockManager.SetStamina.UseAction(baseAbilitys, stamina, _keyHash);

            modifier.lockManager.SetAgentMove.UseAction(baseAbilitys, dir * dashspeed * Time.smoothDeltaTime, _keyHash);
            

            yield return baseAbilitys.characterStats.cStats.staminaCurrent == 0;
        }
        #endregion
        modifier.lockManager.SetAgentMovingDestination.UseAction(baseAbilitys, baseAbilitys.mainTransform.position, _keyHash);




        #region Unlocks transforms rotation Rotation
        modifier.lockManager.SetTransformRotationFromQuaternion.UnLockAction(_keyHash);
        modifier.lockManager.SetTransformRotationFromVector.UnLockAction(_keyHash);
        #endregion


        #region return agent to walk 
        modifier.lockManager.SetAgentMovingSpeed.UseAction(baseAbilitys, -1, true, _keyHash);
        modifier.lockManager.SetAgentIsStopped.UseAction(baseAbilitys, false, _keyHash);
        modifier.lockManager.SetAgentUppdateRotation.UseAction(baseAbilitys, true, _keyHash);
        #endregion


        #region Unlock all agent related actions
        modifier.lockManager.SetAgentIsStopped.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentMovingSpeed.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentMove.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentUppdateRotation.UnLockAction(_keyHash);
        #endregion

        modifier.lockManager.SetStamina.SofUntLock(_keyName);

        _Isdashing = false;
    }



}

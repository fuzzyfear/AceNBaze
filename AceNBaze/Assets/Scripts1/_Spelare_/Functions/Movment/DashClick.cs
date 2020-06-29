using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashClick :_FunctionBase
{

    [SerializeField] private bool _Isdashing = false;
    private Coroutine dashing;

    public DashClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        är här
        //TODO: fixa dashen
        Vector3 mouse = Input.mousePosition;

        Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);

        Vector3 baser = baseAbilitys.agent.transform.position;
        baser.y = 0;
        Vector3 test = new Vector3(castPoint.origin.x, 0, castPoint.origin.z);

        Vector3 mheading = (test - baser);
        float mdist = mheading.magnitude;



        Debug.DrawRay(castPoint.origin, 10f*(mheading / mdist), Color.red);
        //Debug.DrawRay(castPoint.origin, 2*(castPoint.direction));








        if (Input.GetKeyDown(Controlls.instanse.dash) && !_Isdashing)
        {
            _Isdashing = true;


            //TEMP
            CharackterStats.Stats temp = baseAbilitys.characterStats.cStats;
            temp.staminaCurrent = baseAbilitys.characterStats.cStats.staminMax;
            baseAbilitys.characterStats.cStats = temp;

            if (dashing != null)
                StopCoroutine(dashing);
            dashing = StartCoroutine(Dashing(baseAbilitys, modifier));
        }
       
    }

    IEnumerator Dashing(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        #region Locks SetAgentIsStopped, SetAgentMovingDestination, SetAgentMovingSpeed
        bool locked;
        //Locks all the movment actions
        do
        {
#if UNITY_EDITOR
            locked = (modifier.lockManager.SetAgentIsStopped.OwnesOrLock(_keyName) &&
                      modifier.lockManager.SetAgentMovingDestination.OwnesOrLock(_keyName) &&
                      modifier.lockManager.SetAgentMovingSpeed.OwnesOrLock(_keyName));
#else
            locked = (modifier.lockManager.SetAgentIsStopped.OwnesOrLock(_keyHash)         &&
                      modifier.lockManager.SetAgentMovingDestination.OwnesOrLock(_keyHash) &&
                      modifier.lockManager.SetAgentMovingSpeed.OwnesOrLock(_keyHash)       );
#endif

            yield return locked;
        } while (!locked);
        #endregion



        StopMovment(baseAbilitys, modifier.lockManager);

        modifier.lockManager.SetAgentMovingSpeed.UseAction(baseAbilitys, baseAbilitys.characterStats.cStats.dashSpeed,true, _keyHash);

     

        #region Actual dash
        modifier.lockManager.SetStamina.SoftLock(_keyName);
        Vector3 dir = GetMousDirFromAgent(baseAbilitys);

        float stamina     = baseAbilitys.characterStats.cStats.staminaCurrent;
        float draineSpeed = baseAbilitys.characterStats.cStats.dashStaminaDraineSpeed;
        float dashspeed   = baseAbilitys.characterStats.cStats.dashSpeed;

        while (!Input.GetKeyDown(Controlls.instanse.dash) && 
               baseAbilitys.characterStats.cStats.staminaCurrent > 0)
        {


            #region Lock SetStamina 


#if UNITY_EDITOR
            locked = modifier.lockManager.SetStamina.OwnesOrLock(_keyName);
#else
            locked = modifier.lockManager.SetStamina.OwnesOrLock(_keyHash);
#endif
            #endregion
            if (locked)
            {

                stamina = Mathf.MoveTowards(stamina, 0, draineSpeed * Time.deltaTime);

                modifier.lockManager.SetStamina.UseAction(baseAbilitys, stamina, _keyHash);
                modifier.lockManager.SetStamina.UnLockAction(_keyHash);
            }

            baseAbilitys.agent.Move(dir * dashspeed * Time.deltaTime);

            yield return baseAbilitys.characterStats.cStats.staminaCurrent == 0;//new WaitForSeconds(draineSpeed);
        }

        modifier.lockManager.SetStamina.SofUntLock(_keyName);
        #endregion

        modifier.lockManager.SetAgentMovingSpeed.UseAction(baseAbilitys, -1, true, _keyHash);

        //unLocks all the movment actions
        #region Unlock SetAgentIsStopped, SetAgentMovingDestination, SetAgentMovingSpeed
        modifier.lockManager.SetAgentIsStopped.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyName);
        modifier.lockManager.SetAgentMovingSpeed.UnLockAction(_keyName);
        #endregion

        _Isdashing = false;
    }

    private Vector3 GetMousDirFromAgent(CharacterBaseAbilitys baseAbilitys)
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);

        Vector3 mheading = (castPoint.origin - baseAbilitys.agent.transform.position);
        float mdist = mheading.magnitude;
        return  mheading / mdist;
    }

   
    /// <summary>
    /// Stops the movment of the player 
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <param name="modifier"></param>
    private void StopMovment(CharacterBaseAbilitys baseAbilitys, LockManager modifier)
    {

  
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, true, _keyHash);


                modifier.SetAgentMovingDestination.UseAction(baseAbilitys, baseAbilitys.mainTransform.position, _keyHash);
                modifier.SetAgentMovingDestination.UnLockAction(_keyHash);
            
            modifier.SetAgentIsStopped.UseAction(baseAbilitys, false, _keyHash);
            modifier.SetAgentIsStopped.UnLockAction(_keyHash);

    
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that handles a simple dash
/// the dash only works as long ass you hovers above the ground, 
/// that should be fixed for later
/// </summary>
public class DashClick :_FunctionBase
{[TextArea]
    public string text;

    [SerializeField] private bool _Isdashing = false;
    private Coroutine dashing;

    public DashClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {



        //TODO: fixa dashen
     






       



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




        Vector3 dir;
        bool onGround = GetMousDirFromAgent(baseAbilitys, out dir);



        float stamina     = baseAbilitys.characterStats.cStats.staminaCurrent;
        float draineSpeed = baseAbilitys.characterStats.cStats.dashStaminaDraineSpeed;
        float dashspeed   = baseAbilitys.characterStats.cStats.dashSpeed;
        baseAbilitys.agent.isStopped = true;

        while (!Input.GetKeyDown(Controlls.instanse.dash)            && 
               baseAbilitys.characterStats.cStats.staminaCurrent > 0 &&
                        onGround)
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




    /// <summary>
    /// Finds the point on the ground the mouse is over,
    /// clacks dir from the player to that point.
    /// OBS this only works as long as the mous is over grund.
    /// should figure out a better solution that can handle 
    /// holls but this works for now
    /// </summary>
    /// <param name="baseAbilitys"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    private bool GetMousDirFromAgent(CharacterBaseAbilitys baseAbilitys, out Vector3 dir)
    {
        Ray castPoint = baseAbilitys.camar.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool didHitGorund = Physics.Raycast(castPoint, out hit, baseAbilitys.maskes.GrundMask);
        dir = Vector3.zero;

    
        if (didHitGorund)
        {
            Debug.DrawRay(hit.point, 20 * (Vector3.up));
            Vector3 mheading = (hit.point - baseAbilitys.agent.transform.position);
            float mdist = mheading.magnitude;
            dir = mheading / mdist;
        }
        return didHitGorund;
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

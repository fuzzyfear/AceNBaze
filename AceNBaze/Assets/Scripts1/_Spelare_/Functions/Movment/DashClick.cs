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





        float draineSpeed = baseAbilitys.characterStats.cStats.dashStaminaDraineSpeed;

        //The dashing
        while (!Input.GetKeyDown(Controlls.instanse.dash) && 
               baseAbilitys.characterStats.cStats.staminaCurrent > 0)
        {


            yield return new WaitForSeconds(draineSpeed);

            #region Lock SetStamina 


#if UNITY_EDITOR
            locked = modifier.lockManager.SetStamina.OwnesOrLock(_keyName);
#else
            locked = modifier.lockManager.SetStamina.OwnesOrLock(_keyHash);
#endif
            #endregion
            if (locked)
            {
                float stamina = baseAbilitys.characterStats.cStats.staminaCurrent;
                stamina       = Mathf.MoveTowards(stamina, 0, draineSpeed * Time.deltaTime);

                modifier.lockManager.SetStamina.UseAction(baseAbilitys, stamina, _keyHash);
                modifier.lockManager.SetStamina.UnLockAction(_keyHash);
            }



            baseAbilitys.agent.Move(baseAbilitys.mainTransform.position + mdir * dashDist);

        }


        //unLocks all the movment actions
        modifier.lockManager.SetAgentIsStopped.UnLockAction(_keyHash);
        modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyName);
        modifier.lockManager.SetAgentMovingSpeed.UnLockAction(_keyName);


        _Isdashing = false;
    }

    private Vector3 GetMousDirFromAgent(CharacterBaseAbilitys baseAbilitys)
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);

        Vector3 mheading = (castPoint.origin - baseAbilitys.agent.transform.position);
        float mdist = mheading.magnitude;
        return = mheading / mdist;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHold : _FunctionBase
{

    public MovingHold() : base() { }



    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKey(Controlls.instanse.movment))
        {
            bool locked;
#if UNITY_EDITOR
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName);
#else
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyHash);
#endif

            if (locked)
            {
                //Sets moving speed to walking
                if (modifier.lockManager.SetAgentMovingSpeed.LockAction(_keyName))
                {
                    modifier.lockManager.SetAgentMovingSpeed.UseAction(baseAbilitys, -1.0f, true, _keyHash);
                    modifier.lockManager.SetAgentMovingSpeed.UnLockAction(_keyHash);
                }


                Vector3     mouse    = Input.mousePosition;
                Ray        castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.GrundMask))
                {
                    modifier.lockManager.SetAgentMovingDestination.UseAction(baseAbilitys, hit.point, _keyHash);
                }

                modifier.lockManager.SetAgentMovingDestination.UnLockAction(_keyName);
            }
        }
        else if (Input.GetKeyUp(Controlls.instanse.movment))
        {
            StopMovment(baseAbilitys, modifier.lockManager);
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

}

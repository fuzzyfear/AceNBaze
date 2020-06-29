using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClick : _FunctionBase
{
    public MovingClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.movment))
        {

            bool locked;
#if UNITY_EDITOR
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName);
#else
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyHash);
#endif

            if (locked)
            {

#if UNITY_EDITOR
                locked = modifier.lockManager.SetAgentMovingSpeed.LockAction(_keyName);
#else
                locked = modifier.lockManager.SetAgentMovingSpeed.LockAction(_keyHash);
#endif
                //Sets moving speed to walking
                if (locked)
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
    }

    

}

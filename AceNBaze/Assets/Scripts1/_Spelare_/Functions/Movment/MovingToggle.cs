using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToggle : _FunctionBase
{

    [SerializeField] private bool _uppdateMovmentDestination = false;


    public MovingToggle() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        if (Input.GetKeyDown(Controlls.instanse.movment))
            _uppdateMovmentDestination = !_uppdateMovmentDestination;
        else if (Input.anyKeyDown)
            _uppdateMovmentDestination = false; // to stop movment
        
        if (_uppdateMovmentDestination)
        {


            bool locked;
#if UNITY_EDITOR
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName);
           modifier.lockManager.SetAgentMovingDestination.SoftLock(_keyName);
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

                Vector3    mouse     = Input.mousePosition;
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

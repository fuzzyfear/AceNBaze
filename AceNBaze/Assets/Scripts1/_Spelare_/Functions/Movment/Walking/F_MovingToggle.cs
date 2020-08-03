using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_MovingToggle : _FunctionBase
{

    [SerializeField] private bool _uppdateMovmentDestination = false;


    public F_MovingToggle() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {

        if (modifier.controller.Move_Click())
            _uppdateMovmentDestination = !_uppdateMovmentDestination;
        else if (Input.anyKeyDown)
            _uppdateMovmentDestination = false; // to stop movment
        
        if (_uppdateMovmentDestination)
        {

            #region Lock SetAgentMovingDestination 
            bool locked;
#if UNITY_EDITOR
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName);
#else
            locked = modifier.lockManager.SetAgentMovingDestination.LockAction(_keyHash);
#endif
            #endregion
            if (locked)
            {
                //Sets moving speed to walking
                #region Lock SetAgentMovingSpeed
#if UNITY_EDITOR
                locked = modifier.lockManager.SetAgentMovingSpeed.LockAction(_keyName);
#else
                locked = modifier.lockManager.SetAgentMovingSpeed.LockAction(_keyHash);
#endif
                #endregion   
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

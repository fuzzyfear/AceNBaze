using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Simple click to move skript
/// </summary>
public class MovingClick : _FunctionBase
{
    public MovingClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, LockManager modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.movment))
        {
            if (modifier.SetAgentMovingDestination.LockAction(_keyName))
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, baseAbilitys.maskes.GrundMask))
                {
                    modifier.SetAgentMovingDestination.UseAction(baseAbilitys, hit.point, _keyHash);
                }
               
                modifier.SetAgentMovingDestination.UnLockAction(_keyName);
            }
        }
    }




}

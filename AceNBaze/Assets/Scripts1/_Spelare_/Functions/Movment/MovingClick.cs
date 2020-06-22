using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Simple click to move skript
/// </summary>
public class MovingClick : _FunctionBase
{
    public MovingClick() : base() { }

    public override void Tick(CharacterBaseAbilitys stats, LockManager modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.movment))
        {
            if (modifier.SetMovingDestination.LockAction(_keyName))
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = stats.camar.ScreenPointToRay(mouse);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, stats.maskes.grundMask))
                {
                    modifier.SetMovingDestination.UseAction(stats, hit.point, _keyHash);
                }
               
                modifier.SetMovingDestination.UnLockAction(_keyName);
            }
        }
    }




}

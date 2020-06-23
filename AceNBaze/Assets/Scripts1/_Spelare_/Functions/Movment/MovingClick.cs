﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClick : _FunctionBase
{
    public MovingClick() : base() { }

    public override void Tick(CharacterBaseAbilitys baseAbilitys, Modifier modifier)
    {
        if (Input.GetKeyDown(Controlls.instanse.movment))
        {
            if (modifier.lockManager.SetAgentMovingDestination.LockAction(_keyName))
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = baseAbilitys.camar.ScreenPointToRay(mouse);
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

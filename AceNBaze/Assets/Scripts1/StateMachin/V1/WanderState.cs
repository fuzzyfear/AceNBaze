using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private AI _ai;




    public WanderState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();


        if (chaseTarget != null)
        {
            _ai.Target = chaseTarget.gameObject;
            return typeof(ChaseState);
        }
    

        return null;
    }

    public Transform CheckForAggro()
    {

        return AIsettings.InAttackDist(origin      : transform, 
                                       direcion    : Vector3.forward,
                                       tagTarget   : "Player",
                                       numberOfrays: AIsettings.aiSenseCone, 
                                       distance    :AIsettings.AggroRadius);
    } 
}

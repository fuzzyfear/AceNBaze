using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{

    AI _ai;

    public AttackState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
    }


    public override Type Tick()
    {

        float dist = Vector3.Distance(a: _ai.Target.transform.position, b: transform.position);
        if (dist > AIsettings.AttackRadius)
            return typeof(ChaseState);

        _ai.transform.Rotate(Vector3.up, 360);
        return null;
    }



    private Transform InAttackDist()
    {
        return AIsettings.InAttackDist(origin: transform,
                                       direcion: Vector3.forward,
                                       tagTarget: "Player",
                                       numberOfrays: AIsettings.AiSenseCone,
                                       distance: AIsettings.AttackRadius);
    }

    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{

    AI _ai;

    public ChaseState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
    }


    public override Type Tick()
    {


        Transform target = InAggroDist();


        if (target == null)
        {
            _ai.Target = null;
            return typeof(WanderState);
        }

        _ai.Target = target.gameObject;

        _ai.getAgent.SetDestination(target.position);


        var inAttack = InAttackDist();
        if (inAttack != null) 
            return typeof(AttackState);

  



        return null;
    }



    private Transform InAttackDist()
    {
        return AIsettings.InAttackDist(origin      : transform,
                                       direcion    : Vector3.forward,
                                       tagTarget   : "Player",
                                       numberOfrays: AIsettings.aiSenseCirkle,
                                       distance    : AIsettings.AttackRadius);
    }

    private Transform InAggroDist()
    {
        return AIsettings.InAttackDist(origin: transform,
                                       direcion: Vector3.forward,
                                       tagTarget: "Player",
                                       numberOfrays: AIsettings.AiSenseCone,
                                       distance: AIsettings.AggroRadius);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    private AI _ai;
    private Vector3 startPos;



    public WanderState(AI ai) : base(ai.gameObject)
    {
        _ai = ai;
        startPos = _ai.transform.position;

    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();


        if (chaseTarget != null)
        {
            _ai.Target = chaseTarget.gameObject;
            return typeof(ChaseState);
        }

        Wander();
        return null;
    }



    private void Wander()
    {
        if(_ai.getAgent.remainingDistance == 0)
        {
            _ai.getAgent.SetDestination(GetRandomPoint(startPos, maxDist: AIsettings.WanderRadius));
        }
    }

    public Transform CheckForAggro()
    {

        return AIsettings.InAttackDist(origin      : transform, 
                                       direcion    : Vector3.forward,
                                       tagTarget   : "Player",
                                       numberOfrays: AIsettings.AiSenseCone, 
                                       distance    :AIsettings.AggroRadius);
    } 

    private Vector3 GetRandomPoint(Vector3 center, float maxDist)
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere* maxDist;
        randomPos += center;

        NavMeshHit hit;

        NavMesh.SamplePosition(sourcePosition: randomPos, 
                                                out hit, 
                               maxDistance   :maxDist, 
                               areaMask      :NavMesh.AllAreas);
        return hit.position;
    }
}

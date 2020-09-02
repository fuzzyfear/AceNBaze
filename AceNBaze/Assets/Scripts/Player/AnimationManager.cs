using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationManager : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent agent;
    public float velocityLimit;

    // Update is called once per frame
    void Update()
    {
        Walking();
        Attacking();
    }

    void Walking()
    {
        if (agent.velocity.x >= velocityLimit || agent.velocity.x <= -velocityLimit || agent.velocity.z >= velocityLimit || agent.velocity.z <= -velocityLimit)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void Attacking()
    {

    }
}

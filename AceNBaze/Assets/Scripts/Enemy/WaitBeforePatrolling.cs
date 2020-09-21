using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitBeforePatrolling : StateMachineBehaviour
{
    private NavMeshAgent navMeshAgent;
    private float startWaitTime;
    private float waitTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navMeshAgent = animator.GetComponentInParent<NavMeshAgent>();
        startWaitTime = animator.GetComponentInParent<EnemyBehaviour>().waitTime;
        waitTime = startWaitTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            animator.SetFloat("tempWaitTime", startWaitTime);
            navMeshAgent.isStopped = false;
            animator.SetBool("waitBeforePatrolling", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

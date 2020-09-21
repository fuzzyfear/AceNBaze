using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : StateMachineBehaviour
{
	private NavMeshAgent navMeshAgent;
	public float speed;
	private string spotsName;

	private GameObject moveSpotsHolder;
	private Transform[] moveSpots;
	private float startWaitTime;
	private int nextSpot = 0;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		spotsName = animator.GetComponentInParent<EnemyBehaviour>().personalPath;
		startWaitTime = animator.GetComponentInParent<EnemyBehaviour>().waitTime;
		navMeshAgent = animator.GetComponentInParent<NavMeshAgent>();
		moveSpotsHolder = GameObject.Find(spotsName);
		moveSpots = new Transform[moveSpotsHolder.transform.childCount];

		for(int i = 0; i < moveSpotsHolder.transform.childCount; i++)
		{
			moveSpots[i] = moveSpotsHolder.transform.GetChild(i).transform;
		}

		animator.SetFloat("tempWaitTime", startWaitTime);
		navMeshAgent.autoBraking = false;

		FindClosestSpot(animator);
		navMeshAgent.isStopped = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//Change animation state to guarding
		if (moveSpotsHolder.transform.childCount <= 1)
		{
			nextSpot = 0;
			Patrol(animator);
			if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
			{
				animator.SetBool("isGuarding", true);
			}
		}
		else
		{
			Patrol(animator);
		}
	}

	//// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//navMeshAgent.isStopped = false;
	}

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

	void Patrol(Animator animator)
	{
		if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
		{
			if (startWaitTime > 0)
			{
				if (animator.GetFloat("tempWaitTime") == startWaitTime)
				{
					navMeshAgent.isStopped = true;
					animator.SetBool("waitBeforePatrolling", true);
				}
				else
					animator.SetFloat("tempWaitTime", startWaitTime);
			}
			GotoNextPoint();
		}
	}

	void FindClosestSpot(Animator animator)
	{
		Vector3 origin = animator.transform.position;
		int closestSpot = 0;
		float dist = Vector3.Distance(origin, moveSpots[0].transform.position);
		float tempDist = 0;

		//Get distance between all points. Dist is the closest point.
		for (int i = 1; i < moveSpots.Length; i++)
		{
			tempDist = Vector3.Distance(origin, moveSpots[i].transform.position);
			if (tempDist < dist)
			{
				dist = tempDist;
				closestSpot = i;
			}
		}

		//If the distance betweeen the enemy and the next checkpoint is less than 
		//the distance from the current checkpoint to the next checkpoint, go to the next checkpoint
		dist = Vector3.Distance(moveSpots[closestSpot].position, moveSpots[(closestSpot + 1) % moveSpots.Length].position);
		tempDist = Vector3.Distance(origin, moveSpots[(closestSpot + 1) % moveSpots.Length].position);
		if (dist > tempDist)
			closestSpot = (closestSpot + 1) % moveSpots.Length;
		nextSpot = closestSpot;
	}

	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (moveSpots.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		navMeshAgent.destination = moveSpots[nextSpot].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		nextSpot = (nextSpot + 1) % moveSpots.Length;
	}
}

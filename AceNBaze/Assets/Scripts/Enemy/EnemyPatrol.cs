using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : StateMachineBehaviour
{
	private NavMeshAgent navMeshAgent;
	public float speed;
	public float startWaitTime;
	public string spotsName;
	public bool randomPath = false;

	private GameObject moveSpotsHolder;
	private Transform[] moveSpots;
	private float waitTime;
	private int nextSpot = 0;
	private int lastRandomSpot;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		navMeshAgent = animator.GetComponentInParent<NavMeshAgent>();
		moveSpotsHolder = GameObject.FindGameObjectWithTag(spotsName);
		moveSpots = new Transform[moveSpotsHolder.transform.childCount];
		for(int i = 0; i < moveSpotsHolder.transform.childCount; i++)
		{
			moveSpots[i] = moveSpotsHolder.transform.GetChild(i).transform;
		}
		waitTime = startWaitTime;
		FindClosestSpot(animator);
		if (randomPath)
		{
			nextSpot = Random.Range(0, moveSpots.Length);
			lastRandomSpot = nextSpot;
			navMeshAgent.SetDestination(moveSpots[nextSpot].transform.position);
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Patrol(animator);
		navMeshAgent.SetDestination(moveSpots[nextSpot].transform.position);
	}

	//// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{

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

	void Patrol(Animator animator)
	{
		if (Vector3.Distance(animator.transform.position, moveSpots[nextSpot].transform.position) < 0.2f)
		{
			if (waitTime <= 0)
			{
				if (randomPath)
				{
					nextSpot = Random.Range(0, moveSpots.Length);
					while (nextSpot == lastRandomSpot)
					{
						nextSpot = Random.Range(0, moveSpots.Length);
					}
				}
				else
				{
					nextSpot++;
					nextSpot = nextSpot % moveSpots.Length;
				}
				waitTime = startWaitTime;
				navMeshAgent.SetDestination(moveSpots[nextSpot].transform.position);
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
		}
	}

	void FindClosestSpot(Animator animator)
	{
		int newNextSpot = 0;
		float dist = Vector3.Distance(moveSpots[0].transform.position, animator.transform.position);
		for(int i = 1; i < moveSpots.Length; i++)
		{
			float tempDist = Vector3.Distance(moveSpots[i].transform.position, animator.transform.position);
			if (tempDist < dist)
			{
				dist = tempDist;
				newNextSpot = i;
			}
		}
		if(Vector3.Distance(moveSpots[nextSpot].transform.position, animator.transform.position) > dist)
		{
			nextSpot = newNextSpot;
		}
		navMeshAgent.SetDestination(moveSpots[nextSpot].transform.position);
	}
}

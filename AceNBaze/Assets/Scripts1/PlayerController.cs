using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Camera cam;
	public NavMeshAgent agent;
	public CharacterInfo playerStats;
	public Slider hp;
	public LayerMask enemy;
	public bool moveAndAttack;
	private Collider[] hitColliders;
	private RaycastHit attackTarget;
	float distBetweenStartAndGoal;

	private void Start()
	{
		agent.speed = playerStats.movementSpeed;
		hp.maxValue = playerStats.HP;
		hp.value = hp.maxValue;
	}

	void Update()
    {
		MoveToMouse();
		MoveAndAttack();
		WaitToAttackUntilInRange();
		hitColliders = Physics.OverlapSphere(agent.transform.position, playerStats.attackRange, enemy);
	}

    void MoveToMouse()
    {
		if(Input.GetMouseButtonDown(1))
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
			{
				agent.SetDestination(hit.point);
			}
		}
	}

	void MoveAndAttack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
			{
				agent.SetDestination(hit.point);
				attackTarget = hit;
				moveAndAttack = true;
			}
		}
	}

	void WaitToAttackUntilInRange()
	{
		if (moveAndAttack)
		{
			if (agent.pathPending)
			{
				distBetweenStartAndGoal = Vector3.Distance(transform.position, attackTarget.point);
			}
			else
			{
				distBetweenStartAndGoal = agent.remainingDistance;
			}
			if (distBetweenStartAndGoal <= playerStats.attackRange)
			{
				Attack();
			}
		}
	}

	void Attack()
	{
		Debug.Log(attackTarget.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
		attackTarget.collider.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
		moveAndAttack = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color (1, 1, 1, 0.1f);
		Gizmos.DrawSphere(agent.transform.position, playerStats.attackRange);
	}
}

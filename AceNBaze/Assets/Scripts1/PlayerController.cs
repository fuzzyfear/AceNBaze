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
	private RaycastHit attackTarget;
	float distBetweenStartAndGoal;
	bool attackSpeed = true;
	public bool onlyAttack = false;

	private void Start()
	{
		agent.speed = playerStats.movementSpeed;
		hp.maxValue = playerStats.HP;
		hp.value = hp.maxValue;
	}

	void Update()
    {
		MoveToMouse();
		if (!onlyAttack)
		{
			MoveAndAttack();
		}
		else
		{
			OnlyAttack();
		}
		WaitToAttackUntilInRange();
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

	void OnlyAttack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
			{
				if (attackSpeed)
				{
					if (hit.collider.gameObject.layer == 11)
					{
						attackTarget = hit;
						moveAndAttack = true;
					}
					else
					{
						Debug.Log("Miss, no enemmy selected");
						attackSpeed = false;
						StartCoroutine(WaitForAttackSpeed());
					}
				}
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

	IEnumerator WaitForAttackSpeed()
	{
		yield return new WaitForSeconds(playerStats.attackSpeed);
		attackSpeed = true;
	}

	void WaitToAttackUntilInRange()
	{
		if (moveAndAttack)
		{
			if(onlyAttack && agent.velocity == Vector3.zero)
			{
				distBetweenStartAndGoal = Vector3.Distance(agent.transform.position, attackTarget.point);
			}
			else if (!onlyAttack && agent.pathPending)
			{
				distBetweenStartAndGoal = Vector3.Distance(agent.transform.position, attackTarget.point);
			}
			else if(!onlyAttack)
			{
				distBetweenStartAndGoal = agent.remainingDistance;
			}
			if (distBetweenStartAndGoal <= playerStats.attackRange)
			{
				agent.isStopped = true;
				agent.SetDestination(agent.transform.position);
				agent.isStopped = false;
				if (attackSpeed)
				{
					Attack();
					attackSpeed = false;
					StartCoroutine(WaitForAttackSpeed());
				}
			}
			else if (onlyAttack)
			{
				if (attackSpeed)
				{
					Debug.Log("Miss, enemy not in range");
					moveAndAttack = false;
					attackSpeed = false;
					StartCoroutine(WaitForAttackSpeed());
				}
			}
		}
	}

	void Attack()
	{
		attackTarget.collider.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
		Debug.Log(attackTarget.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
		moveAndAttack = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color (1, 1, 1, 0.1f);
		Gizmos.DrawSphere(agent.transform.position, playerStats.attackRange);
	}
}

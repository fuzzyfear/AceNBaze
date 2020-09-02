using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision : MonoBehaviour
{
	private NavMeshAgent navMeshAgent;
	private SphereCollider sphereCollider;
	public CharacterInfo enemyStats;
	private bool attackReady = true;
	private float attackSpeed;

	public float fieldOfView = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;
	public Animator animator;

	private LastPlayerSighting lastPlayerSighting;
	GameObject player;
	private Animator playerAnimator;
	private float playerHealth;
	//private HashIDs hash;
	private Vector3 previousSighting;

	private void Awake()
	{
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		sphereCollider = gameObject.GetComponent<SphereCollider>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerAnimator = player.GetComponentInChildren<Animator>();
		playerHealth = player.GetComponent<PlayerController>().hp.value;

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;

		attackSpeed = enemyStats.attackSpeed;
	}

	private void Update()
	{
		Detect();
		if (playerInSight)
		{
			PlayerInRange();
		}
	}

	void Detect()
	{
		if (lastPlayerSighting.postition != previousSighting)
		{
			personalLastSighting = lastPlayerSighting.postition;
		}

		previousSighting = lastPlayerSighting.postition;

		if (playerHealth > 0f)
		{
			animator.SetBool("playerInSight", playerInSight);
		}
		else
		{
			animator.SetBool("playerInSight", false);
		}
	}

	void PlayerInRange()
	{
		float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);

		if (dist <= enemyStats.attackRange)
		{
			navMeshAgent.isStopped = true;
			navMeshAgent.SetDestination(navMeshAgent.transform.position);
			navMeshAgent.isStopped = false;
			if (attackReady)
			{
				Attack();
				attackReady = false;
				animator.SetBool("isAttacking", true);
				StartCoroutine(WaitForAttackSpeed());
			}
		}
	}

	void Attack()
	{
		Debug.Log(player.name + " takes " + enemyStats.dmg + " dmg");
	}

	IEnumerator WaitForAttackSpeed()
	{
		attackSpeed = 0;
		while (attackSpeed < enemyStats.attackSpeed)
		{
			attackSpeed += 0.1f;
			yield return new WaitForSeconds(enemyStats.attackSpeed * 0.1f);
		}
		animator.SetBool("isAttacking", false);
		attackReady = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfView * 0.5f)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
				{
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;
						lastPlayerSighting.postition = player.transform.position;
					}
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInSight = false;
		}
	}
}

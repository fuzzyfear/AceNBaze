using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyVision : MonoBehaviour
{
	public CharacterInfo enemyStats;
	private float fieldOfView;
	public bool playerInSight;
	public Vector3 personalLastSighting;
	public float waitTime;
	public string personalPath;
	public Slider healthBar;

	public GameObject testCube;

	private Animator animator;
	private NavMeshAgent navMeshAgent;
	private SphereCollider sphereCollider;
	private bool attackReady = true;
	private float attackSpeed;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private float playerHealth;
	private Vector3 previousSighting;
	private GameObject personalPathHolder;
	private bool showHealthBar = false;
	private bool isStunned;

	private void Awake()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		sphereCollider = gameObject.GetComponent<SphereCollider>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerController>().hp.value;
		personalPathHolder = GameObject.Find(personalPath);

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;

		attackSpeed = enemyStats.attackSpeed;
		healthBar.maxValue = enemyStats.healthPoints;
		healthBar.value = enemyStats.healthPoints;
		sphereCollider.radius = enemyStats.visisonDistNeutral;
		fieldOfView = enemyStats.FOWNeutral;
	}

	private void Start()
	{
		for(int i = 0; i < healthBar.transform.childCount; i++)
		{
			healthBar.transform.GetChild(i).GetComponent<Image>().enabled = false;
		}
		navMeshAgent.speed = enemyStats.walkingSpeed;
	}

	private void Update()
	{
		playerHealth = player.GetComponent<PlayerController>().hp.value;
		if (!isStunned)
		{
			Detect();
			PlayerInRange();
			if (playerInSight)
			{
				float dist = Vector3.Distance(navMeshAgent.transform.localPosition, player.transform.localPosition);
				if (dist > sphereCollider.radius)
				{
					Debug.Log(dist);
					playerInSight = false;
					animator.SetBool("inRange", false);
					animator.SetBool("playerInSight", false);
					playerInSight = false;
					sphereCollider.radius = enemyStats.visisonDistNeutral;
					navMeshAgent.speed = enemyStats.walkingSpeed;
					fieldOfView = enemyStats.FOWNeutral;
				}
			}
		}
	}

	void CommitSuduko()
	{
		if(healthBar.value <= 0)
		{
			Debug.Log("Suduko commited");
			gameObject.SetActive(false);
		}
	}

	public void TakeDmg(int dmg)
	{
		if (!showHealthBar)
		{
			for (int i = 0; i < healthBar.transform.childCount; i++)
			{
				Image image = healthBar.transform.GetChild(i).GetComponent<Image>();
				if (image.enabled == false)
				{
					image.enabled = true;
				}
			}
			showHealthBar = false;
		}
		healthBar.value -= dmg;
		//navMeshAgent.SetDestination(player.transform.position);
		CommitSuduko();
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
			//Debug.Log("Player is dead");
			playerInSight = false;
			animator.SetBool("playerInSight", false);
			animator.SetBool("inRange", false);
		}

		if (playerInSight && attackReady)
		{
			navMeshAgent.SetDestination(player.transform.position);
		}
		else if (!playerInSight && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
		{
			//Instantiate(testCube, navMeshAgent.destination, Quaternion.identity);
			playerInSight = false;
			sphereCollider.radius = enemyStats.visisonDistNeutral;
			navMeshAgent.speed = enemyStats.walkingSpeed;
			fieldOfView = enemyStats.FOWNeutral;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.gameObject == player)
		{
			//playerInSight = false;
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if (angle < fieldOfView * 0.5f)
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, sphereCollider.radius))
				{
					if (hit.collider.gameObject == player)
					{
						playerInSight = true;
						sphereCollider.radius = enemyStats.visionDistChase;
						navMeshAgent.speed = enemyStats.runningSpeed;
						fieldOfView = enemyStats.FOWChase;
						lastPlayerSighting.postition = player.transform.position;
					}
				}
			}
			//else if (dist > sphereCollider.radius)
			//{
			//	playerInSight = false;
			//	animator.SetBool("inRange", false);
			//}
		}
	}

	//private void OnTriggerExit(Collider other)
	//{
	//	float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
	//	if (dist > sphereCollider.radius)
	//	{
	//		playerInSight = false;
	//		animator.SetBool("inRange", false);
	//		animator.SetBool("playerInSight", false);
	//		playerInSight = false;
	//		sphereCollider.radius = enemyStats.visisonDistNeutral;
	//		navMeshAgent.speed = enemyStats.walkingSpeed;
	//	}
	//}

	void PlayerInRange()
	{
		//Check if we can see player
		if (playerInSight)
		{
			float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);

			//Check if we are close enough to attack enemy
			if (dist <= enemyStats.attackRange)
			{
				animator.SetBool("inRange", true);
				//Attack if the attackspeed allows it
				if (attackReady)
				{
					Attack();
				}
			}
			else
			{
				animator.SetBool("inRange", false);
			}
		}
	}

	//TODO: Enemy attack 1 time too many when player is dead
	void Attack()
	{
		//Debug.Log("attack player");
		navMeshAgent.isStopped = true;
		navMeshAgent.ResetPath();
		attackReady = false;
		animator.SetBool("attackReady", false);
		animator.SetBool("isAttacking", true);
		StartCoroutine(WaitForAttackSpeed());
	}

	IEnumerator WaitForAttackSpeed()
	{
		//attackSpeed = 0;
		yield return new WaitForSeconds(enemyStats.attackSpeed);
		animator.SetBool("isAttacking", false);
		animator.SetBool("attackReady", true);
		attackReady = true;
		navMeshAgent.isStopped = false;
		player.GetComponent<PlayerController>().TakeDmg(enemyStats.dmg);
	}

	public void GotParried()
	{
		isStunned = true;
		animator.SetTrigger("isStunnedTrigger");
		animator.SetBool("isStunned", true);
		animator.SetBool("attackReady", false);
		navMeshAgent.isStopped = true;
		attackReady = false;
		navMeshAgent.ResetPath();
		StartCoroutine(WaitForStunDuration());
	}

	IEnumerator WaitForStunDuration()
	{
		yield return new WaitForSeconds(enemyStats.stunDuration);
		isStunned = false;
		animator.SetBool("isStunned", false);
		navMeshAgent.isStopped = false;
		StartCoroutine(WaitForAttackSpeed());
	}
}

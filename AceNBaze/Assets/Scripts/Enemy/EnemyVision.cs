using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyVision : MonoBehaviour
{
	public CharacterInfo enemyStats;
	public float fieldOfView = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;
	public float waitTime;
	public string personalPath;
	public Slider healthBar;

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
	}

	private void Start()
	{
		for(int i = 0; i < healthBar.transform.childCount; i++)
		{
			healthBar.transform.GetChild(i).GetComponent<Image>().enabled = false;
		}
		
	}

	private void Update()
	{
		playerHealth = player.GetComponent<PlayerController>().hp.value;
		Detect();
		PlayerInRange();

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
			animator.SetBool("playerInSight", false);
		}
	}

	void PlayerInRange()
	{
		if (playerInSight)
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
	}

	void Attack()
	{
		Debug.Log(player.name + " takes " + enemyStats.dmg + " dmg");
		player.GetComponent<PlayerController>().TakeDmg(enemyStats.dmg);
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

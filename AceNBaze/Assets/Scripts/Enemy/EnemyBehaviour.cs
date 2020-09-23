using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MasterCombatController
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
	public bool attackReady = true;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private float playerHealth;
	private Vector3 previousSighting;
	[HideInInspector] public bool showHealthBar = false;

	private void Awake()
	{
		animator = gameObject.GetComponentInChildren<Animator>();
		navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
		sphereCollider = gameObject.GetComponent<SphereCollider>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerController>().hp.value;

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;

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
		Detect(animator, lastPlayerSighting, playerInSight, previousSighting, personalLastSighting, playerHealth);
		PlayerInRange(animator, navMeshAgent, player, playerInSight, attackReady, enemyStats.attackRange, enemyStats.attackSpeed, enemyStats.dmg);
		//if (attackReady)
		//{
		//	StartCoroutine(WaitForAttackSpeed(animator, attackReady, enemyStats.attackSpeed));
		//}
		if (!attackReady)
		{
			Debug.Log(attackReady);
		}
	}



	//public override IEnumerator WaitForAttackSpeed(Animator animator, bool attackReady, float attackSpeed)
	//{
	//	yield return base.WaitForAttackSpeed(animator, attackReady, attackSpeed);
	//}

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

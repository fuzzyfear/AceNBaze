using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

 public class MasterCombatController : MonoBehaviour
{
	public IEnumerator WaitForAttackSpeed(Animator animator, bool attackReady, float attackSpeed)
	{
		float tempAttackSpeed = 0;
		while (tempAttackSpeed < attackSpeed)
		{
			attackSpeed += 0.1f;
			yield return new WaitForSeconds(attackSpeed * 0.1f);
			Debug.Log("xd");
		}
		animator.SetBool("isAttacking", false);
		attackReady = true;
	}

	public void EnemyTakeDmg(Slider healthBar, bool showHealthBar, int dmg)
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
		CommitSuduko(healthBar);
	}

	void CommitSuduko(Slider healthBar)
	{
		if (healthBar.value <= 0)
		{
			Debug.Log("Suduko commited");
			gameObject.SetActive(false);
		}
	}

	public void PlayerInRange(Animator animator, NavMeshAgent navMeshAgent, GameObject player, bool playerInSight, bool attackReady, float attackRange, float attackSpeed, int damage)
	{
		if (playerInSight)
		{
			float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);

			if (dist <= attackRange)
			{
				navMeshAgent.isStopped = true;
				navMeshAgent.SetDestination(navMeshAgent.transform.position);
				navMeshAgent.isStopped = false;
				if (attackReady)
				{
					attackReady = false;
					Attack(player, damage);
					animator.SetBool("isAttacking", true);
					StartCoroutine(WaitForAttackSpeed(animator, attackReady, attackSpeed));
				}
			}
		}
	}

	void Attack(GameObject player, int damage)
	{
		player.GetComponent<PlayerController>().TakeDmg(damage);
	}

	public void Detect(Animator animator, LastPlayerSighting lastPlayerSighting, bool playerInSight, Vector3 previousSighting, Vector3 personalLastSighting, float playerHealth)
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
}

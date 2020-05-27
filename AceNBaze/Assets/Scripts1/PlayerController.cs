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

	private void Start()
	{
		agent.speed = playerStats.movementSpeed;
		hp.maxValue = playerStats.HP;
		hp.value = hp.maxValue;
	}

	// Update is called once per frame
	void Update()
    {
		MoveToMouse();
		Attack();
    }

    void MoveToMouse()
    {
		if(Input.GetMouseButtonDown(1) == true)
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

	void Attack()
	{
		if (Input.GetMouseButtonDown(0) == true)
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
			{
				Debug.Log(hit.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
				hit.collider.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
			}
		}
	}
}

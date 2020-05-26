using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	public Camera cam;
	public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
		MoveToMouse();
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
}

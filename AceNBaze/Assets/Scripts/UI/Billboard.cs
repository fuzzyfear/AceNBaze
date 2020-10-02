using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

	private void Awake()
	{
		//TODO: Find a better solution
		cam = GameObject.Find("Player").transform.GetChild(0);
	}

	private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}

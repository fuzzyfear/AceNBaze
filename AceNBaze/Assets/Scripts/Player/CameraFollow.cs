using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 startPos;
	private Vector3 offset;
    private Quaternion startRotation;

    private void Start()
    {
        startPos = transform.position;
		offset = player.transform.position;
        startRotation = transform.rotation;
        transform.parent = null;
    }
    //lagade efter karaktären 
    //void Update()
    //{
    //    transform.position = startPos + player.transform.position - offset;
    //}

    private void Update()
    {
        transform.position = startPos + player.transform.position - offset;
    }
}

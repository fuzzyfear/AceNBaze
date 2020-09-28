using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public int panBorderThickness;
    public int panSpeed;

    private Vector3 startPos;
	private Vector3 offset;
    private bool returnToPlayer = true;
    private bool alwaysFollow = true;

    private void Start()
    {
        startPos = transform.position;
		offset = player.transform.position;
        transform.parent = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            alwaysFollow = !alwaysFollow;
            if (alwaysFollow)
            {
                returnToPlayer = true;
                CenterMouse();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            returnToPlayer = true;
        }
        CenterMouse();
        MoveMouse();
    }

    void CenterMouse()
    {
        if (returnToPlayer)
        {
            transform.position = startPos + player.transform.position - offset;
            if (!alwaysFollow)
                returnToPlayer = false;
        }
    }

    void MoveMouse()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            returnToPlayer = false;
            pos.z += panSpeed * Time.deltaTime;
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            returnToPlayer = false;
            pos.z -= panSpeed * Time.deltaTime;
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            returnToPlayer = false;
            pos.x += panSpeed * Time.deltaTime;
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            returnToPlayer = false;
            pos.x -= panSpeed * Time.deltaTime;
            pos.z -= panSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }
}

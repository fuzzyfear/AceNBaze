using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed;
    public float radius;
    public float swapDirTime;
    private Vector3 direction;
    private Rigidbody rb;
    private Vector3 angle;
    private Vector3 prevLocation;
    private Vector3 newAngle;
    private Vector3 circlePos;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        prevLocation = transform.position;
        StartCoroutine((NewDir()));
    }

    IEnumerator NewDir()
    {
        yield return new WaitForSeconds(swapDirTime);
        NewDirection();
        StartCoroutine((NewDir()));
    }

    private void FixedUpdate()
    {
        angle = ((Vector3)transform.position - prevLocation).normalized;
        circlePos = angle + (Vector3)transform.position;
        //Debug.DrawLine(transform.position, newAngle, Color.red);
        rb.AddForce(direction * speed * Time.deltaTime);
        prevLocation = transform.position;
    }

    void NewDirection()
    {
        newAngle = Random.insideUnitCircle.normalized * radius;
        newAngle = new Vector3(newAngle.x, 0, newAngle.y);
        newAngle += circlePos;
        direction = newAngle.normalized;
        Debug.Log(direction);
    }
}

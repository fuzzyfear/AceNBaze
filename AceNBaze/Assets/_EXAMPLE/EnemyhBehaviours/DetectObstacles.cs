using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObstacles : MonoBehaviour
{
    public float detectLength;
    public int rays;
    public int sideRays;
    public Vector2 offsetAngle;
    public float dodgeSpeed;
    public float sideRyasMult;
    private Vector2 angle;
    private List<RaycastHit2D> hits = new List<RaycastHit2D>();
    private Vector2 prevLocation;
    private Vector2 tempAngle;
    private Rigidbody2D rb;
    private float temp;
    private Vector2 dodgeAngle;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        prevLocation = transform.position;
    }

    void FixedUpdate()
    {
        //For normal uneven rays
        angle = ((Vector2)transform.position - prevLocation).normalized;
        angle -= offsetAngle;
        for (int i = 0; i < rays; i++)
        {
            tempAngle = ((angle + offsetAngle * i).normalized);
            hits.Add(Physics2D.Raycast(transform.position, tempAngle, detectLength, LayerMask.GetMask("Obstacles")));
            //Debug.DrawRay(transform.position, tempAngle * detectLength);
        }

        //For two small rays on the side (bad but works)
        angle = ((Vector2)transform.position - prevLocation).normalized;
        angle -= offsetAngle * sideRyasMult;
        for (int i = 0; i < sideRays; i++)
        {
            if(i == 1)
            {
                i++;
            }
            tempAngle = ((angle + offsetAngle * sideRyasMult * i).normalized);
            hits.Add(Physics2D.Raycast(transform.position, tempAngle, detectLength/2, LayerMask.GetMask("Obstacles")));
            //Debug.DrawRay(transform.position, tempAngle * detectLength/2);
        }

        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].collider != null)
            {
                //Debug.DrawLine(transform.position, hits[i].point, Color.red);
                //Debug.DrawLine(hits[i].normal, hits[i].point, Color.yellow);
                temp = (detectLength - hits[i].distance);
                dodgeAngle = (hits[i].normal);
                rb.AddForce(dodgeAngle * (1 + temp*temp) * dodgeSpeed * Time.deltaTime);
            }
        }
        hits.Clear();
        prevLocation = transform.position;
    }
}

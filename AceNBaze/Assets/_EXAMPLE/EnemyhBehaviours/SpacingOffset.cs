using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacingOffset : MonoBehaviour
{
    public float radius;
    public float speed;
    private Vector2 newPos;
    private float dist;
    private Collider2D[] results;
    private Vector3 offsetDir;
    private LayerMask mask;

    private void Start()
    {
        results = new Collider2D[transform.parent.childCount];
        mask = LayerMask.GetMask("Enemy");
    }

    public void FixedUpdate()
    {
        int result = Physics2D.OverlapCircleNonAlloc(transform.position, radius, results, mask);
        for(int i = 0; i < result; i++)
        {
            Collider2D coll = results[i];
            GameObject objInsideArea = coll.gameObject;
            if(objInsideArea != null && objInsideArea != gameObject)
            {
                //Debug.Log(objInsideArea.name);
                offsetDir += objInsideArea.transform.position - transform.position;
            }
        }
        offsetDir = -offsetDir.normalized;
        //Debug.Log(gameObject.name + ", offsetDir " + offsetDir);
        gameObject.GetComponent<Rigidbody2D>().AddForce(offsetDir * speed * Time.deltaTime);
        offsetDir = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Vector4(255, 255, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Vector2 prevLocation;
    private Vector2 normalizedDir;
    private float angle;
    void Start()
    {
        prevLocation = transform.position;
    }

    void FixedUpdate()
    {
        normalizedDir = (new Vector2(gameObject.GetComponent<Rigidbody2D>().position.x, gameObject.GetComponent<Rigidbody2D>().position.y) - prevLocation).normalized;
        angle =  Mathf.Atan2(normalizedDir.y, normalizedDir.x) * Mathf.Rad2Deg - 90f;
        gameObject.GetComponent<Rigidbody2D>().rotation = angle;
        prevLocation = transform.position;
    }
}

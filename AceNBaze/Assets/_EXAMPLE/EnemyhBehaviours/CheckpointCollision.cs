using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollision : MonoBehaviour
{
    public List<Transform> nearbyCheckpoints = new List<Transform>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            GameObject parent = gameObject.transform.parent.gameObject;
            PassingCheckpoints pc = parent.GetComponent<PassingCheckpoints>();
            pc.NextCheckpoint();
        }
    }
}

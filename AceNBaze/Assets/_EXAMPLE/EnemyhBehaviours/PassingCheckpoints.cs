using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingCheckpoints : MonoBehaviour
{
    public bool nearbyCheckpointsOnly;
    public GameObject enemies;
    public float speed;
    private Transform checkpoint;
    [HideInInspector] public Transform currentCheckpoint;
    private Rigidbody2D rb;
    private Vector2 newPos;
    private List<Transform> checkpointList = new List<Transform>();
    //private GameManager gm;
    private Transform lastCheckpoint;

    private void Awake()
    {
        //gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            checkpoint = transform.GetChild(i).transform;
            checkpointList.Add(checkpoint);
            transform.GetChild(i).gameObject.SetActive(false);
        }
        currentCheckpoint = checkpointList[Random.Range(0, checkpointList.Count)];
        checkpoint = currentCheckpoint;
        lastCheckpoint = checkpointList[Random.Range(0, checkpointList.Count)];
        currentCheckpoint.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        //if(gm.enemyList.Count > 0)
        //{
        //    for (int i = 0; i < gm.enemyList.Count; i++)
        //    {
        //        newPos = (currentCheckpoint.position - gm.enemyList[i].transform.position).normalized;
        //        rb = gm.enemyList[i].GetComponent<Rigidbody2D>();
        //        rb.AddForce(newPos * speed * Time.deltaTime);
        //    }
        //}
    }

    public void NextCheckpoint()
    {
        currentCheckpoint.gameObject.SetActive(false);
        List<Transform> tempList = checkpoint.GetComponent<CheckpointCollision>().nearbyCheckpoints;
        if (nearbyCheckpointsOnly)
        {
            checkpoint = tempList[Random.Range(0, tempList.Count)];
            while (checkpoint == lastCheckpoint)
            {
                checkpoint = tempList[Random.Range(0, tempList.Count)];
            }
        }
        else
        {
            while (checkpoint == currentCheckpoint || checkpoint == lastCheckpoint)
            {
                checkpoint = checkpointList[Random.Range(0, checkpointList.Count)];
            }
        }
        lastCheckpoint = currentCheckpoint;
        currentCheckpoint = checkpoint;
        currentCheckpoint.gameObject.SetActive(true);
    }
}

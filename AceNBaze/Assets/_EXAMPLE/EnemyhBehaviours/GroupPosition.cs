using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupPosition : MonoBehaviour
{
    public GameObject Enemies;
    public float speed;
    private List<Vector2> EnemyPos = new List<Vector2>();
    private float tempX;
    private float tempY;
    private GameObject currentEnemy;
    //private GameManager gm;

    private void Awake()
    {
        //gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        tempX = 0;
        tempY = 0;
    }

    void Update()
    {
        //if(gm.enemyList.Count > 0)
        //{
        //    for (int i = 0; i < gm.enemyList.Count; i++)
        //    {
        //        currentEnemy = gm.enemyList[i].gameObject;
        //        EnemyPos.Add(new Vector2(currentEnemy.transform.position.x, currentEnemy.transform.position.y));
        //        tempX += currentEnemy.transform.position.x;
        //        tempY += currentEnemy.transform.position.y;
        //    }
        //    transform.position = new Vector2(tempX / gm.enemyList.Count, tempY / gm.enemyList.Count);
        //    for (int i = 0; i < EnemyPos.Count; i++)
        //    {
        //        currentEnemy = gm.enemyList[i].gameObject;
        //        Vector2 newPos = (transform.position - currentEnemy.transform.position).normalized;
        //        float dist = Vector2.Distance(currentEnemy.transform.position, transform.position);
        //        currentEnemy.GetComponent<Rigidbody2D>().AddForce(newPos * dist * speed * Time.deltaTime);
        //    }
        //    tempX = 0;
        //    tempY = 0;
        //    EnemyPos.Clear();
        //}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, 1f);
    }
}

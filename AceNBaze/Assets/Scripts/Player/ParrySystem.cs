using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySystem : MonoBehaviour
{
    public GameObject[] shieldBorder;
    public GameObject[] shieldFill;
    public float resizeShield;
    public float repositionShield;

    private PlayerController playerController;
    private Vector3 resizeShieldVector;
    private float blockAngle = 0;
    private Vector2 blockInterval = Vector2.zero;
    private bool parryReady;
    private bool lockRotation;
    private float attackAngle;
    private bool isBlocking;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        for (int i = 0; i < shieldBorder.Length; i++)
        {
            shieldBorder[i].SetActive(false);
            shieldFill[i].SetActive(false);
        }

        resizeShieldVector = new Vector3(resizeShield, resizeShield, resizeShield);
    }

    void Update()
    {
        UseShield();
		//if (lockRotation)
  //          playerController.navMeshAgent.updateRotation = false;
		//else
  //          playerController.navMeshAgent.updateRotation = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //if(!parryReady)
            //    Debug.Log("Enemy nearby!");
            Vector3 direction = other.transform.position - transform.position;
            attackAngle= Vector3.SignedAngle(direction, new Vector3(1, 0, 1), Vector3.up);
            Debug.Log("Parry status: " + parryReady);
            Debug.Log("Attack angle: " + attackAngle);
            Debug.Log("Block interval: " + blockInterval);
            if (parryReady && isBlocking)
            {
                Debug.Log("Enemy attacking from: " + attackAngle);
                EnemyVision enemy = other.gameObject.GetComponent<EnemyVision>();
                if (enemy.GetComponentInChildren<Animator>().GetBool("isAttacking") == true)
                {
                    Debug.Log(enemy.name + " got parried!");
                    playerController.enemyTargetToKill = null;
                    playerController.animator.SetBool("isParrying", true);
                    enemy.GetComponent<EnemyVision>().GotParried();
                    parryReady = false;
                    isBlocking = false;
                    StartCoroutine(WaitForParry());
                }
            }
        }
    }

    float GetSignedAngle(Vector3 startPos)
    {
        Ray castPoint = playerController.cam.ScreenPointToRay(startPos);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            //transform.LookAt(hit.point);
            startPos = hit.point;
            Vector3 playerPos = transform.position;
            Vector3 offset = startPos - playerPos;
            float angle = Vector3.SignedAngle(offset, new Vector3(1, 0, 1), Vector3.up);
            return angle;
        }
        return 0;
    }

    void ResetShield()
    {
        for (int i = 0; i < shieldBorder.Length; i++)
        {
            shieldFill[i].transform.position = Vector3.zero;
            shieldFill[i].transform.localScale = Vector3.one;
            shieldFill[i].SetActive(false);
            shieldBorder[i].SetActive(true);
        }
        isBlocking = false;
    }

    void ActivateShield(int index)
    {
        shieldFill[index].SetActive(true);
        shieldBorder[index].SetActive(false);
        shieldFill[index].transform.localScale = resizeShieldVector;
    }

    void UseShield()
    {
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < shieldBorder.Length; i++)
            {
                shieldBorder[i].SetActive(true);
            }
            parryReady = true;
            lockRotation = true;
            playerController.moveAndAttack = false;
        }

        else if (Input.GetMouseButton(1) && parryReady)
        {
            Vector3 mouse = Input.mousePosition;
            blockAngle = GetSignedAngle(mouse);
            //Left
            if (blockAngle >= 150 || blockAngle < -90)
            {
                ResetShield();
                ActivateShield(0);
                shieldFill[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - repositionShield);
                blockInterval = new Vector2(150, -90);
                if(attackAngle >= 150 || attackAngle < -90)
				{
                    isBlocking = true;
				}
            }
            //Right
            else if (blockAngle >= -90 && blockAngle < 30)
            {
                ResetShield();
                ActivateShield(1);
                shieldFill[1].transform.position = new Vector3(transform.position.x + repositionShield, transform.position.y, transform.position.z);
                blockInterval = new Vector2(-90, 30);
                if (attackAngle >= -90 && attackAngle < 30)
                {
                    isBlocking = true;
                }
            }
            //Up
            else if (blockAngle >= 30 && blockAngle < 150)
            {
                ResetShield();
                ActivateShield(2);
                shieldFill[2].transform.position = new Vector3(transform.position.x - repositionShield, transform.position.y, transform.position.z + repositionShield);
                blockInterval = new Vector2(30, 150);
                if (attackAngle >= 30 && attackAngle < 150)
                {
                    isBlocking = true;
                }
            }
        }

        else if (Input.GetMouseButtonUp(1) || !parryReady)
        {
            for (int i = 0; i < shieldBorder.Length; i++)
            {
                parryReady = false;
                isBlocking = false;
                shieldFill[0].transform.position = Vector3.zero;
                shieldFill[0].transform.localScale = Vector3.one;
                shieldFill[i].SetActive(false);
                shieldBorder[i].SetActive(false);
            }
            lockRotation = false;
        }
    }

    IEnumerator WaitForParry()
    {
        yield return new WaitForSeconds(playerController.playerStats.attackSpeed);
        playerController.animator.SetBool("isParrying", false);
    }
}

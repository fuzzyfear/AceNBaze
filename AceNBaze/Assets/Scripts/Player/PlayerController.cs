using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent navMeshAgent;
    public CharacterInfo playerStats;
    public LayerMask enemy;
    public Animator animator;
    public GameObject enemyUi;
    private SphereCollider sphereCollider;

    [Header("Controlls")]
    [SerializeField] private KeyCode DASH = KeyCode.Space;
    [SerializeField] private KeyCode INTERACT = KeyCode.Mouse0;

    [Header("UI")]
    public Slider hp;

    [Header("Move and attack")]
    [HideInInspector] public GameObject enemyTargetToKill;
    public bool moveAndAttack;
    private bool attackReady = true;

    [Header("Dash")]
    [SerializeField] private float dashDistance = 9f;
    [SerializeField] private float dashTime = 9f;
    [SerializeField] private float dashSpeed = 9f;
    public bool isDashing = false;
    public bool dashAvailable = true;
    private float walkingSpeed;

    [Header("Test")]
    public GameObject testCube;
    public bool doOnce = true;

    private void Awake()
    {
        Debug.Log("");
        sphereCollider = gameObject.GetComponent<SphereCollider>();

        navMeshAgent.speed = playerStats.runningSpeed;
        walkingSpeed = navMeshAgent.speed;

        hp.maxValue = playerStats.healthPoints;
        hp.value = hp.maxValue;
        playerStats.attackRange = sphereCollider.radius;
    }

	void Update()
    {
        sphereCollider.radius = playerStats.attackRange;
        MoveToMousePos();
        HoverOverEnemy();
        if (moveAndAttack)
            Attack();
        NewDash();
        if (Vector3.Distance(navMeshAgent.destination, animator.transform.position) < 0.2f)
		{
            animator.SetBool("isWalking", false);
		}
    }

    void NewDash()
    {
        if (Input.GetKeyDown(DASH) && dashAvailable)
        {
			dashAvailable = false;
            isDashing = true;
			navMeshAgent.speed = dashSpeed;
			Ray destination = cam.ScreenPointToRay(Input.mousePosition * dashDistance);
			navMeshAgent.SetDestination(destination.origin);
            animator.SetBool("isDashing", true);
        }
		if (isDashing)
		{
			isDashing = false;
			Vector3 startPos = transform.position;
			StartCoroutine(DashTime(startPos));
			StartCoroutine(DashCooldown());
		}
    }

	IEnumerator DashTime(Vector3 startPos)
	{
		yield return new WaitForSeconds(dashTime);
        navMeshAgent.speed = walkingSpeed;
        animator.SetBool("isDashing", false);
	}

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(playerStats.dashCooldown);
		dashAvailable = true;
    }

    void MoveToMousePos()
    {
        if (Input.GetKey(INTERACT))
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;

            //Attack enemy
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
            {
                navMeshAgent.SetDestination(hit.point);
                animator.SetBool("isWalking", true);
                enemyTargetToKill = hit.transform.gameObject;
                moveAndAttack = true;
            }
            //Move to position
            else if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                navMeshAgent.SetDestination(hit.point);
                animator.SetBool("isWalking", true);
                moveAndAttack = false;
            }
        }
    }

    void Attack()
    {
        if (enemyTargetToKill != null && attackReady)
        {
            float dist = Vector3.Distance(navMeshAgent.transform.position, enemyTargetToKill.transform.position);
            if (dist <= sphereCollider.radius)
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.SetDestination(navMeshAgent.transform.position);
                navMeshAgent.isStopped = false;
                attackReady = false;
                animator.SetBool("isAttacking", true);
                enemyTargetToKill.GetComponent<EnemyVision>().TakeDmg(playerStats.dmg);
                Debug.Log(enemyTargetToKill.name + " takes " + playerStats.dmg + " dmg");
                StartCoroutine(WaitForAttackSpeed());
            }
        }
    }

	IEnumerator WaitForAttackSpeed()
	{
        yield return new WaitForSeconds(playerStats.attackSpeed);

        animator.SetBool("isAttacking", false);
		attackReady = true;
	}

	//private void OnDrawGizmos()
	//{
	//	Gizmos.color = new Color (1, 1, 1, 0.1f);
	//	Gizmos.DrawSphere(agent.transform.position, playerStats.attackRange);
	//}

    public void TakeDmg(int dmg)
    {
		if (!animator.GetBool("isParrying"))
		{
            hp.value -= dmg;
            if (hp.value <= 0)
            {
               // Debug.Log("Player is dead");
            }
        }
    }

    void HoverOverEnemy()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = cam.ScreenPointToRay(mouse);
        RaycastHit hit;

        //Display enemy name and HP
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
        {
            enemyUi.SetActive(true);
            enemyUi.GetComponentInChildren<Text>().text = hit.transform.name;
            enemyUi.GetComponentInChildren<Slider>().maxValue = hit.transform.GetComponent<EnemyVision>().healthBar.maxValue;
            enemyUi.GetComponentInChildren<Slider>().value = hit.transform.GetComponent<EnemyVision>().healthBar.value;
        }
        else
        {
            enemyUi.SetActive(false);
        }
    }
}
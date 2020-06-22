using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Camera cam;
	public NavMeshAgent agent;
	public CharacterInfo playerStats;

    public Slider hp;
    public Slider dashBar;
    public Slider attackbar;
	public LayerMask enemy;


[Space]
[Header("Kontroller")]
    [SerializeField] private KeyCode MOVMENT_KEY_dash = KeyCode.Space;
    [SerializeField] private KeyCode MOVMENT_KEY      = KeyCode.Mouse1;
    [SerializeField] private KeyCode ATTACK_KEY       = KeyCode.Mouse0;

    [Space]
    [Header("Attack saker")]
    [SerializeField] private GameObject enemyTargetToKill;
	private RaycastHit attackTarget;
	private float      distBetweenStartAndGoal;
	private bool       attackSpeed = true;
    public bool moveAndAttack;

    [Space]
[Header("Sprint saker")]
    [SerializeField] private float dashDistanse = 9f;
    [SerializeField] private float dashTime     = 9f;

    private bool  dashing = false;
    private float walkingSpeedNORMAL;
    private float walkingAccelerationNORMAL;

    [SerializeField] private bool CandDash = true;


    private float stepDashRefil = 0f;
    private float attackRefil   = 0f;
[Space]
    [Header("inställnignar för testning ", order = 0)]
    [Header("attack ", order = 1)]
    public bool onlyAttack = false;

    [Header("Rörelse ",order =1)]
    [Tooltip("falsk: måste klicka för att röra sig, sant: kan hålla nera musen för att röra sig ")]
    public bool ConstatMovment = false;
    [Tooltip("sant: klicka en gång för att börja röra på sig, en gång för att sluta")]
    public bool toogleMovment = false;


    bool uppdatemovementTarget;
    private void Start()
	{
		agent.speed        = playerStats.movementSpeed;

		hp.maxValue        = playerStats.HP;
		hp.value           = hp.maxValue;

        attackbar.maxValue = playerStats.attackSpeed;
        attackbar.value    = playerStats.attackSpeed;

        dashBar.maxValue   = playerStats.dashCooldown;
        dashBar.value      = playerStats.dashCooldown;



 


    }

	void Update()
    {
		MoveToMouse();
        MoveDash();

        if (!onlyAttack){ MoveAndAttack();	}
		else		    { OnlyAttack();		}

		WaitToAttackUntilInRange();
	}

    void MoveToMouse()
    {

        //Temp ändring för att ändra hur du rör dig
        if (toogleMovment)
        {
            ConstatMovment = true;
            if (Input.GetKey(MOVMENT_KEY))
                uppdatemovementTarget = !uppdatemovementTarget;
        }
        else
        {
            uppdatemovementTarget = (ConstatMovment) ? Input.GetKey(MOVMENT_KEY) : Input.GetKeyDown(MOVMENT_KEY);
        }
       

        if (uppdatemovementTarget)
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
			{
				agent.SetDestination(hit.point);
			}
		}
	}


    void MoveDash()
    {

        if (ConstatMovment)
        {
            if (Input.GetKey(MOVMENT_KEY_dash) && CandDash)
                StartCoroutine(Dashing());
        }
        else
        {
            if (dashing)
            {
                if (agent.remainingDistance == 0)
                {
                    agent.speed = walkingSpeedNORMAL;
                    agent.acceleration = walkingAccelerationNORMAL;
                    dashing = false;

                }

            }
            else if (Input.GetKey(MOVMENT_KEY_dash) && CandDash)
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = cam.ScreenPointToRay(mouse);

                Vector3 mheading = (castPoint.origin - agent.transform.position);
                float mdist = mheading.magnitude;
                Vector3 mdir = mheading / mdist;

                Vector3 targetPoint;
                RaycastHit hit;
                dashing = false;
                float tempDist = dashDistanse;
                bool continuSearcingIfFales = false;
                while (!continuSearcingIfFales && castPoint.origin != agent.transform.position)
                {
                    continuSearcingIfFales = Physics.Raycast(castPoint, out hit, Mathf.Infinity);
                    if (continuSearcingIfFales)
                    {
                        Vector3 heading = (hit.point - agent.transform.position);
                        float dist = heading.magnitude;
                        Vector3 dir = heading / dist;
                        targetPoint = tempDist * dir + agent.transform.position;

                        bool notFound = true;

                        while (notFound)
                        {
                            if (!agent.SetDestination(targetPoint))
                            {

                                tempDist -= 0.2f;
                                if (tempDist <= 0.0f)
                                {
                                    notFound = false;
                                    agent.speed = walkingSpeedNORMAL;
                                    agent.acceleration = walkingAccelerationNORMAL;

                                }
                                else
                                    targetPoint = tempDist * dir + agent.transform.position;
                            }
                            else
                            {
                                notFound = false;
                                dashing = true;
                            }
                        }
                    }
                    else
                    {
                        castPoint.origin = Vector3.MoveTowards(castPoint.origin, agent.transform.position, 0.2f);
                        //  castPoint.origin -= mdir * 0.2f;
                    }


                }
                if (dashing)
                {
                    walkingSpeedNORMAL = agent.speed;
                    agent.speed = walkingSpeedNORMAL * 10f;
                    walkingAccelerationNORMAL = agent.acceleration;
                    agent.acceleration = agent.acceleration * 10f;
                    CandDash = false;
  
                    StartCoroutine(WaitForDashSpeed());
                }



            }
        }
    }

    void OnlyAttack()
	{
		if (Input.GetKeyDown(ATTACK_KEY))
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
			{
				if (attackSpeed)
				{
					if (hit.collider.gameObject.layer == 11)
					{
						attackTarget = hit;
						moveAndAttack = true;
					}
					else
					{
						Debug.Log("Miss, no enemmy selected");
						attackSpeed = false;
						StartCoroutine(WaitForAttackSpeed());
					}
				}
			}
		}
	}

	void MoveAndAttack()
	{
		if (Input.GetKeyDown(ATTACK_KEY))
		{
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = cam.ScreenPointToRay(mouse);
			RaycastHit hit;

			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
			{
				agent.SetDestination(hit.point);
				attackTarget      = hit;
                enemyTargetToKill = hit.collider.transform.root.gameObject;// för att ta med rörliga fiender i beräkningen
                moveAndAttack     = true;
			}
		}
	}

	IEnumerator WaitForAttackSpeed()
	{

        attackbar.value = 0;

        while (attackbar.value != attackbar.maxValue)
        {
            attackbar.value += 0.1f;
            yield return new WaitForSeconds(playerStats.attackSpeed / 10f);
        }
		attackSpeed = true;
	}

    IEnumerator WaitForDashSpeed()
    {
        dashBar.value = 0f;

        while (dashBar.value != dashBar.maxValue)
        {
            dashBar.value += 0.1f;
            yield return new WaitForSeconds(playerStats.dashCooldown/10f);
        }

        CandDash = true;
    }
    IEnumerator Dashing()
    {

         walkingSpeedNORMAL        = agent.speed;
         agent.speed               = walkingSpeedNORMAL * 10f;
         walkingAccelerationNORMAL = agent.acceleration;
         agent.acceleration        = agent.acceleration * 10f;
         CandDash                  = false;



        while (dashBar.value != dashBar.minValue)
        {
            dashBar.value -= 0.1f;
            yield return new WaitForSeconds(playerStats.dashCooldown / dashTime);
        }




        agent.speed = walkingSpeedNORMAL;
        agent.acceleration = walkingAccelerationNORMAL;
        dashing = false;

        //laddar dash energin
        while (dashBar.value != dashBar.maxValue)
        {
            dashBar.value += 0.1f;
            yield return new WaitForSeconds(playerStats.dashCooldown / 10f);
        }

        CandDash = true;
    }
    void WaitToAttackUntilInRange()
	{
		if (moveAndAttack)
		{
			if(onlyAttack && agent.velocity == Vector3.zero)
			{
				distBetweenStartAndGoal = Vector3.Distance(agent.transform.position, attackTarget.point);
			}
			else if(!onlyAttack)
			{
                if (enemyTargetToKill == null)
                {
                    moveAndAttack = false;
                    return;
                }
                Vector3 EnemyPos = enemyTargetToKill.transform.position;
                agent.SetDestination(EnemyPos);

                if (agent.pathPending)
                {
                    distBetweenStartAndGoal = Vector3.Distance(agent.transform.position, EnemyPos);
                }
                else
                {
                    distBetweenStartAndGoal = agent.remainingDistance;
                }
                





			
			}
			if (distBetweenStartAndGoal <= playerStats.attackRange)
			{
				agent.isStopped = true;
				agent.SetDestination(agent.transform.position);
				agent.isStopped = false;
				if (attackSpeed)
				{
					Attack();
					attackSpeed = false;
					StartCoroutine(WaitForAttackSpeed());
				}
			}
			else if (onlyAttack)
			{
				if (attackSpeed)
				{
					Debug.Log("Miss, enemy not in range");
					moveAndAttack = false;
					attackSpeed   = false;
					StartCoroutine(WaitForAttackSpeed());
				}
			}
		}
	}

	void Attack()
	{
		attackTarget.collider.transform.root.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
		Debug.Log(attackTarget.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
		moveAndAttack = false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color (1, 1, 1, 0.1f);
		Gizmos.DrawSphere(agent.transform.position, playerStats.attackRange);
	}
}

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
	public TargetDummyBehaviour hp;
    public Slider dashBar;
    public Slider attackbar;
	public LayerMask enemy;

    public TEMP_animator_trigger TEMP_anim;
[Space]
[Header("Kontroller")]
    [SerializeField] private KeyCode MOVMENT_KEY_dash = KeyCode.Space;
    [SerializeField] private KeyCode MOVMENT_KEY      = KeyCode.Mouse1;
    [SerializeField] private KeyCode ATTACK_KEY       = KeyCode.Mouse0;
    [SerializeField] private KeyCode BLOCK_KEY        = KeyCode.B;

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
    [Header("BLOCK")]
    public float blockTime = 2f;
    public bool blocking   = false;
    public Slider blockbar;

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
    bool doingsomthing     = false;
    bool swingingForAttack = false;


    private string moventAnimation = walk_anim_raper_key;
    private const string walk_anim_raper_key = "walk";
    private const string dash_anim_raper_key = "dash";
    private bool doing=false;
    private void Start()
	{
		agent.speed        = playerStats.movementSpeed;


        attackbar.maxValue = playerStats.attackSpeed;
        attackbar.value    = playerStats.attackSpeed;


        blockbar.maxValue = playerStats.attackSpeed;
        blockbar.value    = playerStats.attackSpeed;

        dashBar.maxValue   = playerStats.dashCooldown;
        dashBar.value      = playerStats.dashCooldown;




 


    }

	void Update()
    {

    
        if(agent.velocity.magnitude == 0 && !doing )
        {
            TEMP_anim.setValue("idle");
        }
        else if(!doing)
        {
            if (dashing)
            {
                TEMP_anim.setValue(dash_anim_raper_key);
            }
            else
            {
                TEMP_anim.setValue(walk_anim_raper_key);
            }
      
        }

        MoveToMouse();
        MoveDash();



        


        if (block()) { doingsomthing = true;  return; } //TEMP_anim.setValue("block"); 

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




    bool block()
    {
        if (Input.GetKeyDown(BLOCK_KEY))
        {
            if (!blocking && blockbar.value != blockbar.minValue)
            {
              
                StartCoroutine(Blocking());
                blocking = true;
            }
        }
        return blocking;
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
                    agent.speed        = walkingSpeedNORMAL;
                    agent.acceleration = walkingAccelerationNORMAL;
                    dashing            = false;
                    moventAnimation = walk_anim_raper_key;
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
                    moventAnimation = dash_anim_raper_key;
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

                Debug.Log(hit.transform.gameObject.name + " attackSpeed " + attackSpeed);
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
                        TEMP_anim.setValue("attack");
                        doing = true;
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



    IEnumerator Blocking()
    {
        hp.blocking = true;
        while (blockbar.value != blockbar.minValue)
        {
            blockbar.value -= 0.1f;
            yield return new WaitForSeconds(playerStats.attackSpeed / 10f);
        }
        hp.blocking = false;
        //laddar dash energin
        while (blockbar.value != blockbar.maxValue)
        {
            blockbar.value += 0.1f;
            yield return new WaitForSeconds(playerStats.attackSpeed / 10f);
        }
        blocking = false;
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
					attackSpeed = false;
					StartCoroutine(WaitForAttackSpeed());
				}
			}
		}
	}

	 void Attack()
	{
        swingingForAttack = true;
        doing = true;
        TEMP_anim.setValue("attack");
    }
    public void realAttack()
    {
        if (swingingForAttack)
        {
            attackTarget.collider.transform.root.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
            Debug.Log(attackTarget.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
            moveAndAttack = false;
            swingingForAttack = false;
         
        }
        doing = false;
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color (1, 1, 1, 0.5f);
		Gizmos.DrawSphere(agent.transform.position, playerStats.attackRange);
	}
}

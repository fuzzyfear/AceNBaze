using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerControll_dash : MonoBehaviour
{
    public Camera        cam;
    public NavMeshAgent  agent;
    public CharacterInfo playerStats;
    public Slider        hp;
    public LayerMask     enemy;


    [SerializeField]private KeyCode MOVMENT_KEY_dash = KeyCode.Space;
    [SerializeField]private KeyCode MOVMENT_KEY      = KeyCode.Mouse1;
    [SerializeField]private KeyCode ATTACK_KEY       = KeyCode.Mouse0;


    [SerializeField] private float dashDistanse = 4f;
    private bool dashing = false;
    private float walkingSpeedNORMAL;
    private float walkingAccelerationNORMAL;
   


    public void Start()
    {
        agent.speed = playerStats.movementSpeed;
        hp.maxValue = playerStats.healthPoints;
        hp.value    = hp.maxValue;
   
    }

    // Update is called once per frame
    void Update()
    {



   
        
        

        MoveToMouse();
        MoveDash();
        



        Attack();
    }


    void MoveDash()
    {
        if (dashing)
        {
            if(agent.remainingDistance == 0)
            {
              agent.speed = walkingSpeedNORMAL;
                agent.acceleration = walkingAccelerationNORMAL;
                dashing = false;
            }
        }
        else if (Input.GetKey(MOVMENT_KEY_dash))
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
                        Debug.Log(!agent.SetDestination(targetPoint));
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
                            Debug.Log(targetPoint);
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
            }



        }
    }





    void MoveToMouse()
    {
        if (Input.GetKeyDown(MOVMENT_KEY) == true)
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

    void Attack()
    {
        if (Input.GetKeyDown(ATTACK_KEY) == true)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;

            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, enemy))
            {
                Debug.Log(hit.collider.gameObject.name + " takes " + playerStats.dmg + " dmg");
                hit.collider.gameObject.GetComponent<TargetDummyBehaviour>().TakeDmg(playerStats.dmg);
            }
        }
    }
}
